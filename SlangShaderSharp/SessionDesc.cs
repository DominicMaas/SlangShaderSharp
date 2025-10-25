using System.Runtime.InteropServices;

namespace SlangShaderSharp;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct SessionDesc
{
    /// <summary>
    ///     The size of this structure, in bytes.
    /// </summary>
    public nuint structureSize;

    /// <summary>
    ///      Code generation targets to include in the session.
    /// </summary>
    public TargetDesc* targets;

    /// <summary>
    ///      Code generation targets to include in the session.
    /// </summary>
    public long targetCount;

    /// <summary>
    ///     Flags to configure the session.
    /// </summary>
    public SessionFlags flags;

    /// <summary>
    ///     Default layout to assume for variables with matrix types.
    /// </summary>
    public SlangMatrixLayoutMode defaultMatrixLayoutMode;

    /// <summary>
    ///     Paths to use when searching for `#include`d or `import`ed files.
    /// </summary>
    public sbyte** searchPaths;

    /// <summary>
    ///     Paths to use when searching for `#include`d or `import`ed files.
    /// </summary>
    public long searchPathCount;

    public PreprocessorMacroDesc* preprocessorMacros;
    public long preprocessorMacroCount;
    public nint fileSystem;
    public bool enableEffectAnnotations;
    public bool allowGLSLSyntax;

    /// <summary>
    ///     Pointer to an array of compiler option entries, whose size is compilerOptionEntryCount.
    /// </summary>
    public CompilerOptionEntry* compilerOptionEntries;

    /// <summary>
    ///     Number of additional compiler option entries.
    /// </summary>
    public uint compilerOptionEntryCount;

    /// <summary>
    ///     Whether to skip SPIRV validation.
    /// </summary>
    public bool skipSPIRVValidation;
}
