namespace SlangShaderSharp;

/// <summary>
///     Defines an archive type used to holds a 'file system' type structure.
/// </summary>
public enum SlangArchiveType : int
{
    Undefined = 0,
    Zip = 1,
    /// <summary> Riff container with no compression </summary>
    Riff = 2,
    RiffDeflate = 3,
    RiffLZ4 = 4,
    CountOf
}
