using System.Runtime.InteropServices;

namespace SlangShaderSharp;

[StructLayout(LayoutKind.Sequential)]
public struct CooperativeVectorTypeUsageInfo
{
    public SlangScalarType ComponentType = SlangScalarType.None;

    /// <summary>
    ///     Maximum element count used for this component type in cooperative
    ///     operations (e.g. MatMul).
    /// </summary>
    public uint MaxSize = 0;

    /// <summary>
    ///     Whether this component type is used as an accumulation/storage type for
    ///     cooperative training operations (e.g. outer-product accumulation and
    ///     reduce-sum accumulation). This flag is independent of `maxSize`.
    /// </summary>
    [MarshalAs(UnmanagedType.I1)]
    public bool UsedForTrainingOp = false;

    public CooperativeVectorTypeUsageInfo()
    { }
}
