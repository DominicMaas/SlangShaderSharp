using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp.Internal;

[CustomMarshaller(typeof(string), MarshalMode.Default, typeof(NoFreeUtf8StringMarshaller))]
internal static unsafe class NoFreeUtf8StringMarshaller
{
    /// <summary>
    /// Converts a string to an unmanaged version.
    /// </summary>
    /// <param name="managed">The managed string to convert.</param>
    /// <returns>An unmanaged string.</returns>
    public static byte* ConvertToUnmanaged(string? managed) => Utf8StringMarshaller.ConvertToUnmanaged(managed);

    /// <summary>
    /// Converts an unmanaged string to a managed version.
    /// </summary>
    /// <param name="unmanaged">The unmanaged string to convert.</param>
    /// <returns>A managed string.</returns>
    public static string? ConvertToManaged(byte* unmanaged) => Utf8StringMarshaller.ConvertToManaged(unmanaged);
}
