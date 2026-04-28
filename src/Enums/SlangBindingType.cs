namespace SlangShaderSharp;

[Flags]
public enum SlangBindingType : uint
{
    Unknown = 0,

    Sampler = 1,
    Texture = 2,
    ConstantBuffer = 3,
    ParameterBlock = 4,
    TypedBuffer = 5,
    RawBuffer = 6,
    CombinedTextureSampler = 7,
    InputRenderTarget = 8,
    InlineUniformData = 9,
    RayTracingAccelerationStructure = 10,

    VaryingInput = 11,
    VaryingOutput = 12,
    ExistentialValue = 13,
    PushConstant = 14,

    MutableFlag = 0x100,

    MutableTexture = Texture | MutableFlag,
    MutableTypedBuffer = TypedBuffer | MutableFlag,
    MutableRawBuffer = RawBuffer | MutableFlag,

    BaseMask = 0x00FF,
    ExtMask = 0xFF00,
}
