namespace SlangShaderSharp;

public enum SlangCompileTarget : int
{
    TargetUnknown = 0,
    TargetNone = 1,
    Glsl = 2,

    [Obsolete("just use `SLANG_GLSL`")]
    GlslVulkanDeprecated = 3,

    [Obsolete("No Reason Specified")]
    GlslVulkanOneDescDeprecated = 4,
    Hlsl = 5,
    Spirv = 6,
    SpirvAsm = 7,
    Dxbc = 8,
    DxbcAsm = 9,
    Dxil = 10,
    DxilAsm = 11,

    /// <summary>
    ///     The C language
    /// </summary>
    CSource = 12,

    /// <summary>
    ///     C++ code for shader kernels.
    /// </summary>
    CppSource = 13,

    /// <summary>
    ///     Standalone binary executable (for hosting CPU/OS)
    /// </summary>
    HostExecutable = 14,

    /// <summary>
    ///     A shared library/Dll for shader kernels (for hosting CPU/OS)
    /// </summary>
    ShaderSharedLibrary = 15,

    /// <summary>
    ///     A CPU target that makes the compiled shader code available to be run immediately
    /// </summary>
    ShaderHostCallable = 16,

    /// <summary>
    ///     Cuda source
    /// </summary>
    CudaSource = 17,

    /// <summary>
    ///     PTX
    /// </summary>
    PTX = 18,

    /// <summary>
    ///     Object code that contains CUDA functions.
    /// </summary>
    CudaObjectCode = 19,

    /// <summary>
    ///     Object code that can be used for later linking (kernel/shader)
    /// </summary>
    ObjectCode = 20,

    /// <summary>
    ///     C++ code for host library or executable.
    /// </summary>
    HostCppSource = 21,

    /// <summary>
    ///     Host callable host code (ie non kernel/shader)
    /// </summary>
    HostHostCallable = 22,

    /// <summary>
    ///      C++ PyTorch binding code.
    /// </summary>
    CppPyTorchBinding = 23,

    /// <summary>
    ///     Metal shading language
    /// </summary>
    Metal = 24,

    /// <summary>
    ///     Metal library
    /// </summary>
    MetalLib = 25,

    /// <summary>
    ///     Metal library assembly
    /// </summary>
    MetalLibAsm = 26,

    /// <summary>
    ///     A shared library/Dll for host code (for hosting CPU/OS)
    /// </summary>
    HostSharedLibrary = 27,

    /// <summary>
    ///     WebGPU shading language
    /// </summary>
    Wgsl = 28,

    /// <summary>
    ///     SPIR-V assembly via WebGPU shading language
    /// </summary>
    WgslSpirvAsm = 29,

    /// <summary>
    ///     SPIR-V via WebGPU shading language
    /// </summary>
    WgslSpirv = 30,

    /// <summary>
    ///     Bytecode that can be interpreted by the Slang VM
    /// </summary>
    HostVM = 31,

    /// <summary>
    ///     C++ header for shader kernels.
    /// </summary>
    CppHeader = 32,

    /// <summary>
    ///     Cuda header
    /// </summary>
    CudaHeader = 33,

    /// <summary>
    ///     Host object code
    /// </summary>
    HostObjectCode = 34,

    /// <summary>
    ///     Host LLVM IR assembly
    /// </summary>
    HostLlvmIR = 35,

    /// <summary>
    ///     Host LLVM IR assembly (kernel/shader)
    /// </summary>
    ShaderLlvmIR = 36,

    CountOf,
}
