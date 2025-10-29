namespace SlangShaderSharp;

/// <summary>
///     Type that identifies how a path should be interpreted
/// </summary>
public enum SlangPathType : uint
{
    /// <summary>
    ///     Path specified specifies a directory.
    /// </summary>
    Directory,

    /// <summary>
    ///     Path specified is to a file.
    /// </summary>
    File,
}
