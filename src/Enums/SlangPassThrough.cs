namespace SlangShaderSharp;

public enum SlangPassThrough : int
{
    None = 0,
    Fxc = 1,
    Dxc = 2,
    Glslang = 3,
    SpirvDis = 4,
    /// <summary> Clang C/C++ compiler </summary>
    Clang = 5,
    /// <summary> Visual studio C/C++ compiler </summary>
    VisualStudio = 6,
    /// <summary> GCC C/C++ compiler </summary>
    Gcc = 7,
    /// <summary> Generic C or C++ compiler, which is decided by the source type </summary>
    Generic_C_Cpp = 8,
    /// <summary> NVRTC Cuda compiler </summary>
    Nvrtc = 9,
    /// <summary> LLVM 'compiler' - includes LLVM and Clang </summary>
    Llvm = 10,
    /// <summary> SPIRV-opt </summary>
    SpirvOpt = 11,
    /// <summary> Metal compiler </summary>
    Metal = 12,
    /// <summary> Tint WGSL compiler </summary>
    Tint = 13,
    /// <summary> SPIRV-link </summary>
    SpirvLink = 14,
    CountOf,
}
