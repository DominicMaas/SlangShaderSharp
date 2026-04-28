namespace SlangShaderSharp;

public enum SlangResourceAccess : uint
{
    None = 0,
    Read = 1,
    ReadWrite = 2,
    RasterOrdered = 3,
    Append = 4,
    Consume = 5,
    Write = 6,
    Feedback = 7,
    Unknown = 0x7FFFFFFF,
}
