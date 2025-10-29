namespace SlangShaderSharp;

public enum SlangPassThrough : int
{
    SLANG_PASS_THROUGH_NONE,
    SLANG_PASS_THROUGH_FXC,
    SLANG_PASS_THROUGH_DXC,
    SLANG_PASS_THROUGH_GLSLANG,
    SLANG_PASS_THROUGH_SPIRV_DIS,
    SLANG_PASS_THROUGH_CLANG,         ///< Clang C/C++ compiler
    SLANG_PASS_THROUGH_VISUAL_STUDIO, ///< Visual studio C/C++ compiler
    SLANG_PASS_THROUGH_GCC,           ///< GCC C/C++ compiler
    SLANG_PASS_THROUGH_GENERIC_C_CPP, ///< Generic C or C++ compiler, which is decided by the
                                      ///< source type
    SLANG_PASS_THROUGH_NVRTC,         ///< NVRTC Cuda compiler
    SLANG_PASS_THROUGH_LLVM,          ///< LLVM 'compiler' - includes LLVM and Clang
    SLANG_PASS_THROUGH_SPIRV_OPT,     ///< SPIRV-opt
    SLANG_PASS_THROUGH_METAL,         ///< Metal compiler
    SLANG_PASS_THROUGH_TINT,          ///< Tint WGSL compiler
    SLANG_PASS_THROUGH_SPIRV_LINK,    ///< SPIRV-link
    SLANG_PASS_THROUGH_COUNT_OF,
}
