namespace SlangShaderSharp;

public enum SlangLanguageVersion : uint
{
    Unknown = 0,
    Legacy = 2018,
    V2025 = 2025,
    V2026 = 2026,

    Default = Legacy,
    Latest = V2026,
}
