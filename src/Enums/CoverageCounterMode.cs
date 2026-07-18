namespace SlangShaderSharp;

public enum CoverageCounterMode : uint
{
    /// <summary>
    ///     The counter holds the number of times the entry executed
    ///     (atomic add per execution).
    /// </summary>
    Count = 0,

    /// <summary>
    ///     The counter is a boolean flag: `0` if the entry never executed,
    ///     non-zero if it executed at least once. Written with a plain
    ///     (non-atomic) store of `1`, so it carries no execution count but
    ///     avoids all atomic contention. Selected by
    ///     `-trace-coverage-boolean`.
    /// </summary>
    Boolean = 1,
}
