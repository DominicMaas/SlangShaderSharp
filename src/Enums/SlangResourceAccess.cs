namespace SlangShaderSharp;

[Flags]
public enum SlangResourceAccess : uint
{
    None,
    Read,
    ReadWrite,
    RasterOrdered,
    Append,
    Consume,
    Write,
    Feedback,
    Unknown = 0x7FFFFFFF,
}
