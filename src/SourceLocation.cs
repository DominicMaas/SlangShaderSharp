using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;


[NativeMarshalling(typeof(SourceLocationMarshaller))]
public struct SourceLocation
{
    public string? FilePath;
    public nint Line;
    public nint Column;
}

internal unsafe struct SourceLocationUnmanaged
{
    public byte* filePath;
    public nint line;
    public nint column;
}

[CustomMarshaller(typeof(SourceLocation), MarshalMode.ManagedToUnmanagedOut, typeof(SourceLocationMarshaller))]
[CustomMarshaller(typeof(SourceLocation), MarshalMode.Default, typeof(SourceLocationMarshaller))]
internal static unsafe class SourceLocationMarshaller
{
    public static SourceLocation ConvertToManaged(SourceLocationUnmanaged unmanaged)
    {
        return new SourceLocation
        {
            FilePath = Utf8StringMarshaller.ConvertToManaged(unmanaged.filePath),
            Line = unmanaged.line,
            Column = unmanaged.column
        };
    }
}