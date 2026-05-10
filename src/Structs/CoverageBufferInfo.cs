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

    public CoverageBufferInfo()
    { }
}

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct CoverageBufferInfoUnmanaged
{
    public nuint structSize;
    public int Space;
    public int Binding;
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
            Binding = managed.Binding
        };
    }

    public static CoverageBufferInfo ConvertToManaged(CoverageBufferInfoUnmanaged unmanaged)
    {
        return new CoverageBufferInfo
        {
            Space = unmanaged.Space,
            Binding = unmanaged.Binding
        };
    }
}