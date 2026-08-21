namespace SlangShaderSharp;

public enum SlangLanguageVersion : uint
{
    Unknown = 0,
    Legacy = 2018,
    V2025 = 2025,
    V2026 = 2026,

    /// <summary>
    ///     Note: the numeric value may change when the language version is given an
    ///     official name. For now, it's one past the latest stable version.
    /// </summary>
    V202C = 2027,

    /// <summary>
    ///     Codename for <see cref="V2025" />.
    /// </summary>
    V202A = V2025,

    /// <summary>
    ///     Codename for <see cref="V2026" />.
    /// </summary>
    V202B = V2026,

    Default = Legacy,

    /// <summary>
    ///     The latest stable version
    /// </summary>
    Latest = V2026,

    /// <summary>
    ///     Development version
    /// </summary>
    Next = V202C,
}
