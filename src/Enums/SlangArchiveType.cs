namespace SlangShaderSharp.Enums;

/// <summary>
///     Defines an archive type used to holds a 'file system' type structure.
/// </summary>
public enum SlangArchiveType : int
{
    Undefined,
    Zip,
    /// <summary> Riff container with no compression </summary>
    Riff,
    RiffDeflate,
    RiffLZ4,
    CountOf
}
