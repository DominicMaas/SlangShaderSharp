namespace SlangShaderSharp;

[Flags]
public enum SlangBindingType : uint
{
    Unknown = 0,

    Sampler,
    Texture,
    ConstantBuffer,
    ParameterBlock,
    TypedBuffer,
    RawBuffer,
    CombinedTextureSampler,
    InputRenderTarget,
    InlineUniformData,
    RayTracingAccelerationStructure,

    VaryingInput,
    VaryingOutput,
    ExistentialValue,
    PushConstant,

    MutableFlag = 0x100,

    MutableTexture = Texture | MutableFlag,
    MutableTypedBuffer = TypedBuffer | MutableFlag,
    MutableRawBuffer = RawBuffer | MutableFlag,

    BaseMask = 0x00FF,
    ExtMask = 0xFF00,
}
