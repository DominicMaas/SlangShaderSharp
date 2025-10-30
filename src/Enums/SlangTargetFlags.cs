namespace SlangShaderSharp;

/// <summary>
///     Flags to control code generation behavior of a compilation target
/// </summary>
public enum SlangTargetFlags : uint
{
    /// <summary>
    ///     When compiling for a D3D Shader Model 5.1 or higher target, allocate
    ///     distinct register spaces for parameter blocks.
    /// </summary>
    [Obsolete("This behavior is now enabled unconditionally.")]
    ParameterBlocksUseRegisterSpaces = 1 << 4,

    /// <summary>
    ///     When set, will generate target code that contains all entrypoints defined
    ///     in the input source or specified via the `spAddEntryPoint` function in a
    ///     single output module (library/source file).
    /// </summary>
    GenerateWholeProgram = 1 << 8,

    /// <summary>
    ///     When set, will dump out the IR between intermediate compilation steps.
    /// </summary>
    DumpIr = 1 << 9,

    /// <summary>
    ///     When set, will generate SPIRV directly rather than via glslang.
    ///     This flag will be deprecated, use CompilerOption instead.
    /// </summary>
    GenerateSpirvDirectly = 1 << 10,
}
