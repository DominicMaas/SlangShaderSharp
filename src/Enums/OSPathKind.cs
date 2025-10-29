namespace SlangShaderSharp;

/// <summary>
///     Determines how paths map to files on the OS file system
/// </summary>
public enum OSPathKind : byte
{
    /// <summary>
    ///     Paths do not map to the file system
    /// </summary>
    None,

    /// <summary>
    ///     Paths map directly to the file system
    /// </summary>
    Direct,

    /// <summary>
    ///     Only paths gained via PathKind::OperatingSystem map to the operating
    ///     system file system
    /// </summary>
    OperatingSystem
}
