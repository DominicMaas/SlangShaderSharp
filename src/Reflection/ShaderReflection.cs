using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(ShaderReflectionMarshaller))]
public readonly partial struct ShaderReflection : IEquatable<ShaderReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use ShaderReflection.Null instead.")]
    public ShaderReflection()
    {
        Handle = 0;
    }

    internal ShaderReflection(nint value)
    {
        Handle = value;
    }

    public static ShaderReflection Null => new(0);

    public static bool operator ==(ShaderReflection left, ShaderReflection right) => left.Handle == right.Handle;
    public static bool operator !=(ShaderReflection left, ShaderReflection right) => !(left == right);

    public bool Equals(ShaderReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is ShaderReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // ---------------- Methods ---------------- //

    //  TODO

    // ---------------- Native Imports ----------------

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflection_GetParameterCount(nint handle);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflection_GetTypeParameterCount(nint handle);


}

[CustomMarshaller(typeof(ShaderReflection), MarshalMode.Default, typeof(ShaderReflectionMarshaller))]
internal static class ShaderReflectionMarshaller
{
    public static nint ConvertToUnmanaged(ShaderReflection managed) => managed.Handle;
    public static ShaderReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
