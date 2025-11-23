namespace SlangShaderSharp;

/// <summary>
///     Describes the debugging information format produced during a compilation.
/// </summary>
public enum SlangDebugInfoFormat : uint
{
    /// <summary>
    ///     Use the default debugging format for the target
    /// </summary>
    Default,

    /// <summary>
    ///     CodeView C7 format (typically means debugging information is embedded in the binary)
    /// </summary>
    C7,

    /// <summary>
    ///     Program database
    /// </summary>
    PDB,

    /// <summary>
    ///     Stabbs
    /// </summary>
    Stabs,

    /// <summary>
    ///     COFF debug info
    /// </summary>
    COFF,

    /// <summary>
    ///     DWARF debug info (we may want to support specifying the version)
    /// </summary>
    DWARF,

    CountOf
}
