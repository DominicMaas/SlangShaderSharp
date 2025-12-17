namespace SlangShaderSharp;

public enum SlangParameterCategory : uint
{
    None,
    Mixed,
    ConstantBuffer,
    ShaderResource,
    UnorderedAccess,
    VaryingInput,
    VaryingOutput,
    SamplerState,
    Uniform,
    DescriptorTableSlot,
    SpecializationConstant,
    PushConstantBuffer,

    /// <summary>
    ///     HLSL register `space`, Vulkan GLSL `set`
    /// </summary>
    RegisterSpace,
    Generic,
    RayPayload,
    HitAttributes,
    CallablePayload,
    ShaderRecord,
    ExistentialTypeParam,
    ExistentialObjectParam,
    SubElementRegisterSpace,
    Subpass,
    MetalArgumentBufferElement,
    MetalAttribute,
    MetalPayload,
    Count,
    MetalBuffer = ConstantBuffer,
    MetalTexture = ShaderResource,
    MetalSampler = SamplerState,

    [Obsolete("No Reason Specified")]
    VertexInput = VaryingInput,

    [Obsolete("No Reason Specified")]
    FragmentOutput = VaryingOutput,

    [Obsolete("No Reason Specified")]
    CountV1 = Subpass,
}
