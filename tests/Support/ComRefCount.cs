using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp.Tests.Support;

/// <summary>
///     Reads (and repairs) the native reference count behind an RCW.
///     <para>
///         Slang exposes no way to observe a refcount, but ownership bugs in the generated marshalling
///         are invisible until the count reaches zero and the process dies somewhere unrelated. Reading
///         the count directly turns those bugs into an ordinary assertion.
///     </para>
/// </summary>
internal static class ComRefCount
{
    /// <summary>
    ///     The current reference count of the COM object <paramref name="comObject"/> wraps.
    /// </summary>
    public static int Read(object comObject)
    {
        if (!ComWrappers.TryGetComInstance(comObject, out var unknown))
        {
            throw new InvalidOperationException($"{comObject.GetType()} is not a COM wrapper.");
        }

        // TryGetComInstance hands back a pointer it has already AddRef'd, so everything we observe
        // while holding it is one higher than the count the rest of the process sees.
        try
        {
            Marshal.AddRef(unknown);
            return Marshal.Release(unknown) - 1;
        }
        finally
        {
            Marshal.Release(unknown);
        }
    }

    /// <summary>
    ///     The current reference count of the CCW that represents the managed object <paramref name="managed"/>
    ///     to native code - the counterpart of <see cref="Read"/> for objects implemented in C#.
    ///     <para>
    ///         Handing such an object back from native code unwraps to the original instance rather than an
    ///         RCW, so <see cref="Read"/> has nothing to inspect. The generated marshalling still calls
    ///         <c>Release</c> on the CCW, though, so its count is where an ownership bug shows.
    ///     </para>
    /// </summary>
    public static unsafe int ReadManaged<T>(T managed) where T : class
    {
        var unknown = (nint)ComInterfaceMarshaller<T>.ConvertToUnmanaged(managed);

        // ConvertToUnmanaged hands back an AddRef'd pointer, so discount it just like Read does.
        try
        {
            Marshal.AddRef(unknown);
            return Marshal.Release(unknown) - 1;
        }
        finally
        {
            Marshal.Release(unknown);
        }
    }

    /// <summary>
    ///     <see cref="RestoreTo"/> for the CCW of a managed object; see <see cref="ReadManaged"/>.
    /// </summary>
    public static unsafe void RestoreManagedTo<T>(T managed, int expected) where T : class
    {
        var actual = ReadManaged(managed);
        if (actual >= expected)
        {
            return;
        }

        var unknown = (nint)ComInterfaceMarshaller<T>.ConvertToUnmanaged(managed);

        try
        {
            for (var i = actual; i < expected; i++)
            {
                Marshal.AddRef(unknown);
            }
        }
        finally
        {
            Marshal.Release(unknown);
        }
    }

    /// <summary>
    ///     Puts back references that an over-releasing call consumed.
    ///     <para>
    ///         Without this a failing ownership test leaves a half-destroyed Slang object behind and takes
    ///         the rest of the run - or the whole process - down with it, hiding the assertion that fired.
    ///     </para>
    /// </summary>
    public static void RestoreTo(object comObject, int expected)
    {
        var actual = Read(comObject);
        if (actual >= expected)
        {
            return;
        }

        if (!ComWrappers.TryGetComInstance(comObject, out var unknown))
        {
            return;
        }

        try
        {
            for (var i = actual; i < expected; i++)
            {
                Marshal.AddRef(unknown);
            }
        }
        finally
        {
            Marshal.Release(unknown);
        }
    }
}
