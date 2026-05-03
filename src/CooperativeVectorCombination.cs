using System.Runtime.InteropServices;

namespace SlangShaderSharp;

[StructLayout(LayoutKind.Sequential)]
public struct CooperativeVectorCombination
{
    public SlangScalarType InputType = SlangScalarType.None;
    public SlangScalarType InputInterpretation = SlangScalarType.None;

    /// <summary>
    ///     Number of logical elements packed into each physical input element.
    ///     For example, this is 4 when four int8 values are packed into one uint32 input element.
    /// </summary>
    public uint InputPackingFactor = 1;

    public SlangScalarType MatrixInterpretation = SlangScalarType.None;

    /// <summary>
    ///     <see cref="SlangScalarType.None"/> means the operation has no bias operand/matrix.
    /// </summary>
    public SlangScalarType BiasInterpretation = SlangScalarType.None;

    public SlangScalarType ResultType = SlangScalarType.None;

    [MarshalAs(UnmanagedType.I1)]
    public bool Transpose = false;

    public CooperativeVectorCombination()
    { }
}
