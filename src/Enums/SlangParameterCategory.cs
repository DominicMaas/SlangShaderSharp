namespace SlangShaderSharp;

public enum SlangParameterCategory : uint
{
    None = 0,
    Mixed = 1,
    ConstantBuffer = 2,
    ShaderResource = 3,
    UnorderedAccess = 4,
    VaryingInput = 5,
    VaryingOutput = 6,
    SamplerState = 7,
    Uniform = 8,
    DescriptorTableSlot = 9,
    SpecializationConstant = 10,
    PushConstantBuffer = 11,

    /// <summary>
    ///     HLSL register `space`, Vulkan GLSL `set`
    /// </summary>
    RegisterSpace = 12,
    Generic = 13,
    RayPayload = 14,
    HitAttributes = 15,
    CallablePayload = 16,
    ShaderRecord = 17,
    ExistentialTypeParam = 18,
    ExistentialObjectParam = 19,
    SubElementRegisterSpace = 20,
    Subpass = 21,
    MetalArgumentBufferElement = 22,
    MetalAttribute = 23,
    MetalPayload = 24,

    Count,

    MetalBuffer = ConstantBuffer,
    MetalTexture = ShaderResource,
    MetalSampler = SamplerState,

    [Obsolete("Deprecated in the native API; use VaryingInput instead.")]
    VertexInput = VaryingInput,

    [Obsolete("Deprecated in the native API; use VaryingOutput instead.")]
    FragmentOutput = VaryingOutput,

    [Obsolete("Deprecated in the native API; use Subpass instead.")]
    CountV1 = Subpass,
}
