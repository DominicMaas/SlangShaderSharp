using System.Runtime.InteropServices;

namespace SlangShaderSharp;

[StructLayout(LayoutKind.Sequential)]
public struct CooperativeMatrixCombination
{
    /// <summary>
    ///     Number of rows of matrix A and the result.
    /// </summary>
    public uint M = 0;

    /// <summary>
    ///     Number of columns of matrix B and the result.
    /// </summary>
    public uint N = 0;

    /// <summary>
    ///     Shared inner dimension: columns of A and rows of B.
    /// </summary>
    public uint K = 0;

    public SlangScalarType ComponentTypeA = SlangScalarType.None;
    public SlangScalarType ComponentTypeB = SlangScalarType.None;
    public SlangScalarType ComponentTypeC = SlangScalarType.None;
    public SlangScalarType ComponentTypeResult = SlangScalarType.None;

    [MarshalAs(UnmanagedType.I1)]
    public bool Saturate = false;

    public SlangScope Scope = SlangScope.None;

    public CooperativeMatrixCombination()
    { }
}
