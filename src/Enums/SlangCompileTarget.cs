namespace SlangShaderSharp;

public enum SlangCompileTarget : int
{
    TargetUnknown,
    TargetNone,
    Glsl,

    [Obsolete("just use `SLANG_GLSL`")]
    GlslVulkanDeprecated,

    [Obsolete("No Reason Specified")]
    GlslVulkanOneDescDeprecated,
    Hlsl,
    Spirv,
    SpirvAsm,
    Dxbc,
    DxbcAsm,
    Dxil,
    DxilAsm,

    /// <summary>
    ///     The C language
    /// </summary>
    CSource,

    /// <summary>
    ///     C++ code for shader kernels.
    /// </summary>
    CppSource,

    /// <summary>
    ///     Standalone binary executable (for hosting CPU/OS)
    /// </summary>
    HostExecutable,

    /// <summary>
    ///     A shared library/Dll for shader kernels (for hosting CPU/OS)
    /// </summary>
    ShaderSharedLibrary,

    /// <summary>
    ///     A CPU target that makes the compiled shader code available to be run immediately
    /// </summary>
    ShaderHostCallable,

    /// <summary>
    ///     Cuda source
    /// </summary>
    CudaSource,

    /// <summary>
    ///     PTX
    /// </summary>
    PTX,

    /// <summary>
    ///     Object code that contains CUDA functions.
    /// </summary>
    CudaObjectCode,

    /// <summary>
    ///     Object code that can be used for later linking
    /// </summary>
    ObjectCode,

    /// <summary>
    ///     C++ code for host library or executable.
    /// </summary>
    HostCppSource,

    /// <summary>
    ///     Host callable host code (ie non kernel/shader)
    /// </summary>
    HostHostCallable,

    /// <summary>
    ///      C++ PyTorch binding code.
    /// </summary>
    CppPyTorchBinding,

    /// <summary>
    ///     Metal shading language
    /// </summary>
    Metal,

    /// <summary>
    ///     Metal library
    /// </summary>
    MetalLib,

    /// <summary>
    ///     Metal library assembly
    /// </summary>
    MetalLibAsm,

    /// <summary>
    ///     A shared library/Dll for host code (for hosting CPU/OS)
    /// </summary>
    HostSharedLibrary,

    /// <summary>
    ///     WebGPU shading language
    /// </summary>
    Wgsl,

    /// <summary>
    ///     SPIR-V assembly via WebGPU shading language
    /// </summary>
    WgslSpirvAsm,

    /// <summary>
    ///     SPIR-V via WebGPU shading language
    /// </summary>
    WgslSpirv,

    /// <summary>
    ///     Bytecode that can be interpreted by the Slang VM
    /// </summary>
    HostVM,

    /// <summary>
    ///     C++ header for shader kernels.
    /// </summary>
    CppHeader,

    /// <summary>
    ///     Cuda header
    /// </summary>
    CudaHeader,

    CountOf,
}
