namespace SlangShaderSharp;

public enum SlangDebugInfoLevel : uint
{
    /// <summary>
    ///     Don't emit debug information at all.
    /// </summary>
    None = 0,

    /// <summary>
    ///     Emit as little debug information as possible, while still supporting stack trackers.
    /// </summary>
    Minimal = 1,

    /// <summary>
    ///     Emit whatever is the standard level of debug information for each target.
    /// </summary>
    Standard = 2,

    /// <summary>
    ///     Emit as much debug information as possible for each target.
    /// </summary>
    Maximal = 3
}
