namespace SlangShaderSharp;

public enum SyntheticResourceScope : uint
{
    /// <summary>
    ///     One shared resource bound at program/global scope.
    /// </summary>
    Global = 0,

    /// <summary>
    ///     A resource scoped to one linked entry point.
    /// </summary>
    EntryPoint = 1,
}
