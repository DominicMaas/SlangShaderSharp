namespace SlangShaderSharp.Enums;

/// <summary>
///     Flags to control compilation behavior.
/// </summary>
[Flags]
public enum SlangCompileFlags : uint
{
    /// <summary>
    ///     Do as little mangling of names as possible, to try to preserve original names
    /// </summary>
    NoMangling = 1 << 3,

    /// <summary>
    ///     Skip code generation step, just check the code and generate layout
    /// </summary>
    NoCodeGen = 1 << 4,

    /// <summary>
    ///     Obfuscate shader names on release products
    /// </summary>
    Obfuscate = 1 << 5,
}
