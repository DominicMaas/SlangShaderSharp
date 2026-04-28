namespace SlangShaderSharp;

/// <summary>
///     Identifies different types of writer target
/// </summary>
public enum SlangWriterChannel : uint
{
    Diagnostic = 0,
    StandardOutput = 1,
    StandardError = 2,
    CountOf
}
