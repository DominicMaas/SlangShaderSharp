namespace SlangShaderSharp;

public enum SlangCompileTarget : int
{
    SLANG_TARGET_UNKNOWN,
    SLANG_TARGET_NONE,
    SLANG_GLSL,

    [Obsolete("just use `SLANG_GLSL`")]
    SLANG_GLSL_VULKAN_DEPRECATED,

    [Obsolete]
    SLANG_GLSL_VULKAN_ONE_DESC_DEPRECATED,
    SLANG_HLSL,
    SLANG_SPIRV,
    SLANG_SPIRV_ASM,
    SLANG_DXBC,
    SLANG_DXBC_ASM,
    SLANG_DXIL,
    SLANG_DXIL_ASM,

    /// <summary>
    ///     The C language
    /// </summary>
    SLANG_C_SOURCE,

    /// <summary>
    ///     C++ code for shader kernels.
    /// </summary>
    SLANG_CPP_SOURCE,

    /// <summary>
    ///     Standalone binary executable (for hosting CPU/OS)
    /// </summary>
    SLANG_HOST_EXECUTABLE,

    /// <summary>
    ///     A shared library/Dll for shader kernels (for hosting CPU/OS)
    /// </summary>
    SLANG_SHADER_SHARED_LIBRARY,

    /// <summary>
    ///     A CPU target that makes the compiled shader code available to be run immediately
    /// </summary>
    SLANG_SHADER_HOST_CALLABLE,

    /// <summary>
    ///     Cuda source
    /// </summary>
    SLANG_CUDA_SOURCE,

    /// <summary>
    ///     PTX
    /// </summary>
    SLANG_PTX,

    /// <summary>
    ///     Object code that contains CUDA functions.
    /// </summary>
    SLANG_CUDA_OBJECT_CODE,

    /// <summary>
    ///     Object code that can be used for later linking
    /// </summary>
    SLANG_OBJECT_CODE,

    /// <summary>
    ///     C++ code for host library or executable.
    /// </summary>
    SLANG_HOST_CPP_SOURCE,

    /// <summary>
    ///     Host callable host code (ie non kernel/shader)
    /// </summary>
    SLANG_HOST_HOST_CALLABLE,

    /// <summary>
    ///      C++ PyTorch binding code.
    /// </summary>
    SLANG_CPP_PYTORCH_BINDING,

    /// <summary>
    ///     Metal shading language
    /// </summary>
    SLANG_METAL,

    /// <summary>
    ///     Metal library
    /// </summary>
    SLANG_METAL_LIB,

    /// <summary>
    ///     Metal library assembly
    /// </summary>
    SLANG_METAL_LIB_ASM,

    /// <summary>
    ///     A shared library/Dll for host code (for hosting CPU/OS)
    /// </summary>
    SLANG_HOST_SHARED_LIBRARY,

    /// <summary>
    ///     WebGPU shading language
    /// </summary>
    SLANG_WGSL,

    /// <summary>
    ///     SPIR-V assembly via WebGPU shading language
    /// </summary>
    SLANG_WGSL_SPIRV_ASM,

    /// <summary>
    ///     SPIR-V via WebGPU shading language
    /// </summary>
    SLANG_WGSL_SPIRV,

    /// <summary>
    ///     Bytecode that can be interpreted by the Slang VM
    /// </summary>
    SLANG_HOST_VM,

    SLANG_TARGET_COUNT_OF,
}
