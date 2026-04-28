namespace SlangShaderSharp;

public enum SlangDiagnosticColor
{
    /// <summary>
    ///      Use color if output sink is a tty
    /// </summary>
    Auto = 0,

    /// <summary>
    ///     Always use color
    /// </summary>
    Always = 1,

    /// <summary>
    ///      Never use color
    /// </summary>
    Never = 2,
}
