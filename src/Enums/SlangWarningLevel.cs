namespace SlangShaderSharp;

/// <summary>
///     A warning "level" (group), modeled on the clang/gcc -Wall/-Wextra/-Wpedantic
///     groups. Each group is enabled independently: a warning tagged with a group is
///     emitted only when that group has been enabled, while warnings in the implicit
///     Default group are always emitted.
/// </summary>
public enum SlangWarningLevel : int
{
    /// <summary>
    ///     Always emitted; this is the baseline group and is not
    ///     something a caller enables explicitly.
    /// </summary>
    Default = 0,

    /// <summary>
    ///     Warnings enabled by -Wall.
    /// </summary>
    All = 1,

    /// <summary>
    ///     Warnings enabled by -Wextra.
    /// </summary>
    Extra = 2,

    /// <summary>
    ///     Warnings enabled by -Wpedantic.
    /// </summary>
    Pedantic = 3,
}
