using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     Coverage-buffer binding info returned by <see cref="ICoverageTracingMetadata.GetBufferInfo" />.
/// </summary>
[NativeMarshalling(typeof(CoverageBufferInfoMarshaller))]
public struct CoverageBufferInfo
{
    /// <summary>
    ///      Register space the coverage buffer is bound to (D3D12
    ///       `space`, Vulkan descriptor set), or -1 if not assigned for
    ///       this target.
    /// </summary>
    public int Space = -1;

    /// <summary>
    ///     Binding index the coverage buffer is bound at (D3D12
    ///     `register`, Vulkan `binding`), or -1 if not assigned for
    ///     this target.
    /// </summary>
    public int Binding = -1;

    /// <summary>
    ///     Byte width of one counter slot in the synthesized buffer:
    ///     `4` for a `RWStructuredBuffer&lt;uint&gt;`, `8` for a
    ///     `RWStructuredBuffer&lt;uint64_t&gt;`. The host reads back
    ///     `getCounterCount() * elementByteWidth` bytes and interprets
    ///     each slot as a little-endian unsigned integer of this width.
    ///     Mirrored on the JSON sidecar as `buffer.element_stride`.
    ///
    ///     A current in-process implementation always writes `4` or `8`;
    ///     the in-class default `4` only appears if the caller forgot to
    ///     pass the field to `getBufferInfo`. A sentinel `0` can only
    ///     arise when reading a metadata object from an older compiler
    ///     that pre-dates this field; both values should be treated as
    ///     the historical uint32 layout.
    /// </summary>
    public uint ElementByteWidth = 4;

    public CoverageBufferInfo()
    { }
}

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct CoverageBufferInfoUnmanaged
{
    public nuint structSize;
    public int Space;
    public int Binding;
    public uint ElementByteWidth;
}

[CustomMarshaller(typeof(CoverageBufferInfo), MarshalMode.Default, typeof(CoverageBufferInfoMarshaller))]
internal static unsafe class CoverageBufferInfoMarshaller
{
    public static CoverageBufferInfoUnmanaged ConvertToUnmanaged(CoverageBufferInfo managed)
    {
        return new CoverageBufferInfoUnmanaged
        {
            // Use the leading `structSize` for ABI-versioned struct growth
            structSize = (nuint)sizeof(CoverageBufferInfoUnmanaged),
            Space = managed.Space,
            Binding = managed.Binding,
            ElementByteWidth = managed.ElementByteWidth,
        };
    }

    public static CoverageBufferInfo ConvertToManaged(CoverageBufferInfoUnmanaged unmanaged)
    {
        return new CoverageBufferInfo
        {
            Space = unmanaged.Space,
            Binding = unmanaged.Binding,
            ElementByteWidth = unmanaged.ElementByteWidth,
        };
    }
}