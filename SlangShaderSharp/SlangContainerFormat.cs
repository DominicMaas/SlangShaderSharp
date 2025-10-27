namespace SlangShaderSharp;

/// <summary>
///     A "container format" describes the way that the outputs
///     for multiple files, entry points, targets, etc. should be
///     combined into a single artifact for output.
/// </summary>
public enum SlangContainerFormat : int
{
    /// <summary>
    ///     Don't generate a container.
    /// </summary>
    None,

    /// <summary>
    ///     Generate a container in the `.slang-module` format,
    ///     which includes reflection information, compiled kernels, etc.
    /// </summary>
    SlangModule
}
