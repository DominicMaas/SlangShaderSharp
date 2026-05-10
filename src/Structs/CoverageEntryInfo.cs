using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///      Per-counter-slot attribution returned by <see cref="ICoverageTracingMetadata.GetEntryInfo" />.
/// </summary>
[NativeMarshalling(typeof(CoverageEntryInfoMarshaller))]
public struct CoverageEntryInfo
{
    /// <summary>
    ///     Source file for this counter slot, or null if the slot
    ///     could not be attributed to a real source file.
    /// </summary>
    public string? File;

    /// <summary>
    ///     1-based source line for this counter slot, or 0 if the slot
    ///     could not be attributed to a real source line.
    /// </summary>
    public uint Line;
}

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct CoverageEntryInfoUnmanaged
{
    public nuint structSize;
    public byte* file;
    public uint line;
}

[CustomMarshaller(typeof(CoverageEntryInfo), MarshalMode.Default, typeof(CoverageEntryInfoMarshaller))]
internal static unsafe class CoverageEntryInfoMarshaller
{
    public static CoverageEntryInfoUnmanaged ConvertToUnmanaged(CoverageEntryInfo managed)
    {
        return new CoverageEntryInfoUnmanaged
        {
            // Use the leading `structSize` for ABI-versioned struct growth
            structSize = (nuint)sizeof(CoverageEntryInfoUnmanaged),
            file = Utf8StringMarshaller.ConvertToUnmanaged(managed.File),
            line = managed.Line
        };
    }

    public static CoverageEntryInfo ConvertToManaged(CoverageEntryInfoUnmanaged unmanaged)
    {
        return new CoverageEntryInfo
        {
            File = Utf8StringMarshaller.ConvertToManaged(unmanaged.file),
            Line = unmanaged.line
        };
    }

    public static void Free(CoverageEntryInfoUnmanaged unmanaged)
    {
        // `file` is a non-owning pointer returned by Slang and remains valid
        // only for the lifetime of the associated metadata. It must not be
        // freed by this marshaller.
    }
}