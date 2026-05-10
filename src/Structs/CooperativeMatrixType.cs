using System.Runtime.InteropServices;

namespace SlangShaderSharp;

[StructLayout(LayoutKind.Sequential)]
public struct CooperativeMatrixType
{
    /// <summary>
    ///     Component type <see cref="SlangScalarType.None"/> means this type is not valid.
    /// </summary>
    public SlangScalarType ComponentType = SlangScalarType.None;
    public SlangScope Scope = SlangScope.None;

    public uint RowCount = 0;
    public uint ColumnCount = 0;

    public SlangCooperativeMatrixUse Use = SlangCooperativeMatrixUse.UseA;

    public CooperativeMatrixType()
    { }
}
