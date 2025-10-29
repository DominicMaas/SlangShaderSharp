namespace SlangShaderSharp;

public enum SlangPassThrough : int
{
    None,
    Fxc,
    Dxc,
    Glslang,
    SpirvDis,
    /// <summary> Clang C/C++ compiler </summary>
    Clang,
    /// <summary> Visual studio C/C++ compiler </summary>
    VisualStudio,
    /// <summary> GCC C/C++ compiler </summary>
    Gcc,
    /// <summary> Generic C or C++ compiler, which is decided by the source type </summary>
    Generic_C_Cpp,
    /// <summary> NVRTC Cuda compiler </summary>
    Nvrtc,
    /// <summary> LLVM 'compiler' - includes LLVM and Clang </summary>
    Llvm,
    /// <summary> SPIRV-opt </summary>
    SpirvOpt,
    /// <summary> Metal compiler </summary>
    Metal,
    /// <summary> Tint WGSL compiler </summary>
    Tint,
    /// <summary> SPIRV-link </summary>
    SpirvLink,
    CountOf,
}
