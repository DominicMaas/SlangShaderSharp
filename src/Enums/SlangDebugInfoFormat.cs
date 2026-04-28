namespace SlangShaderSharp;

/// <summary>
///     Describes the debugging information format produced during a compilation.
/// </summary>
public enum SlangDebugInfoFormat : uint
{
    /// <summary>
    ///     Use the default debugging format for the target
    /// </summary>
    Default = 0,

    /// <summary>
    ///     CodeView C7 format (typically means debugging information is embedded in the binary)
    /// </summary>
    C7 = 1,

    /// <summary>
    ///     Program database
    /// </summary>
    PDB = 2,

    /// <summary>
    ///     Stabbs
    /// </summary>
    Stabs = 3,

    /// <summary>
    ///     COFF debug info
    /// </summary>
    COFF = 4,

    /// <summary>
    ///     DWARF debug info (we may want to support specifying the version)
    /// </summary>
    DWARF = 5,

    CountOf
}
