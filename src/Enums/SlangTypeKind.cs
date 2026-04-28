namespace SlangShaderSharp;

public enum SlangTypeKind : uint
{
    None = 0,
    Struct = 1,
    Array = 2,
    Matrix = 3,
    Vector = 4,
    Scalar = 5,
    ConstantBuffer = 6,
    Resource = 7,
    SamplerState = 8,
    TextureBuffer = 9,
    ShaderStorageBuffer = 10,
    ParameterBlock = 11,
    GenericTypeParameter = 12,
    Interface = 13,
    OutputStream = 14,
    MeshOutput = 15,
    Specialized = 16,
    Feedback = 17,
    Pointer = 18,
    DynamicResource = 19,
    Enum = 20,
    Count,
}
