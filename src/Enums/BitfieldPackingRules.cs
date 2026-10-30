namespace SlangShaderSharp;

public enum BitfieldPackingRules
{
    /// <summary>
    ///     Uses LSB-first packing; fields may share storage across underlying type sizes.
    /// </summary>
    Default = 0,

    /// <summary>
    ///     Uses LSB-first packing; a type-size change starts new storage. Rejects zero-width fields.
    /// </summary>
    MSVC = 1,

    /// <summary>
    ///     Uses MSB-first packing and starts new storage on type-size changes. Not recommended.
    /// </summary>
    LegacyMSBFirstMSVC = 2,
}
