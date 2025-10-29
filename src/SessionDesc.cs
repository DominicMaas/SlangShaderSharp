using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

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

public struct SessionDescription
{
    public TargetDesc[] Targets;
    public SessionFlags Flags;
    public SlangMatrixLayoutMode DefaultMatrixLayoutMode;
    public string[] SearchPaths;
    public PreprocessorMacroDesc[] PreprocessorMacros;
    public nint FileSystem;
    public bool EnableEffectAnnotations;
    public bool AllowGLSLSyntax;
    public CompilerOptionEntry[] CompilerOptionEntries;
    public bool SkipSPIRVValidation;
}

[CustomMarshaller(typeof(SessionDescription), MarshalMode.Default, typeof(SessionDescriptionMarshaller))]
internal static unsafe class SessionDescriptionMarshaller
{
    public static SessionDesc ConvertToUnmanaged(SessionDescription managed)
    {
        var unmanaged = new SessionDesc()
        {
            structureSize = (nuint)sizeof(SessionDesc),

            flags = managed.Flags,
            defaultMatrixLayoutMode = managed.DefaultMatrixLayoutMode,
            searchPaths = null, // Note: Handling of SearchPaths array marshalling is not implemented here.
            searchPathCount = managed.SearchPaths?.LongLength ?? 0,
            preprocessorMacros = null, // Note: Handling of PreprocessorMacros array marshalling is not implemented here.
            preprocessorMacroCount = managed.PreprocessorMacros?.LongLength ?? 0,
            fileSystem = managed.FileSystem,
            enableEffectAnnotations = managed.EnableEffectAnnotations,
            allowGLSLSyntax = managed.AllowGLSLSyntax,
            compilerOptionEntries = null, // Note: not Implemented
            compilerOptionEntryCount = (uint)(managed.CompilerOptionEntries?.LongLength ?? 0),
        };

        if (managed.Targets != null && managed.Targets.Length > 0)
        {
            var size = managed.Targets.Length * sizeof(TargetDesc);
            unmanaged.targets = (TargetDesc*)NativeMemory.Alloc((nuint)size);
            unmanaged.targetCount = managed.Targets?.LongLength ?? 0;
            fixed (TargetDesc* source = managed.Targets)
            {
                Buffer.MemoryCopy(source, unmanaged.targets, size, size);
            }
        }

        return unmanaged;
    }

    public static void Free(SessionDesc unmanaged)
    {
        if (unmanaged.targets != null)
        {
            NativeMemory.Free(unmanaged.targets);
        }
    }
}
