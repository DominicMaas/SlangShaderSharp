namespace SlangShaderSharp;

/// <summary>
///     Identifies different types of writer target
/// </summary>
public enum SlangWriterChannel : uint
{
    Diagnostic,
    StandardOutput,
    StandardError,
    CountOf
}
