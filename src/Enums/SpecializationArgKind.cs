namespace SlangShaderSharp;

public enum SpecializationArgKind : int
{
    /// <summary>
    ///     An invalid specialization argument.
    /// </summary>
    Unknown,

    /// <summary>
    ///     Specialize to a type.
    /// </summary>
    Type,

    /// <summary>
    ///     An expression representing a type or value
    /// </summary>
    Expr,
}
