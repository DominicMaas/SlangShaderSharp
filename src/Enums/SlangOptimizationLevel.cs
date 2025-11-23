namespace SlangShaderSharp;

public enum SlangOptimizationLevel : uint
{
    /// <summary>
    ///     Don't optimize at all.
    /// </summary>
    None = 0,

    /// <summary>
    ///      Default optimization level: balance code quality and compilation time.
    /// </summary>
    Default,

    /// <summary>
    ///     Optimize aggressively.
    /// </summary>
    High,

    /// <summary>
    ///     Include optimizations that may take a very long time, or may involve severe space-vs-speed tradeoffs
    /// </summary>
    Maximal
}
