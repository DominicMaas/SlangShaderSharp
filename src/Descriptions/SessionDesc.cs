// Ignore Spelling: Preprocessor

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[NativeMarshalling(typeof(SessionDescMarshaller))]
public unsafe struct SessionDesc
{
    /// <summary>
    ///      Code generation targets to include in the session.
    /// </summary>
    public TargetDesc[]? Targets = null;

    /// <summary>
    ///     Flags to configure the session.
    /// </summary>
    public SessionFlags Flags = SessionFlags.None;

    /// <summary>
    ///     Default layout to assume for variables with matrix types.
    /// </summary>
    public SlangMatrixLayoutMode DefaultMatrixLayoutMode = SlangMatrixLayoutMode.RowMajor;

    /// <summary>
    ///     Paths to use when searching for `#include`d or `import`ed files.
    /// </summary>
    public string[]? SearchPaths = null;

    public PreprocessorMacroDesc[]? PreprocessorMacros = null;

    public ISlangFileSystem? FileSystem = null;

    public bool EnableEffectAnnotations = false;

    public bool AllowGLSLSyntax = false;

    /// <summary>
    ///     Pointer to an array of compiler option entries, whose size is compilerOptionEntryCount.
    /// </summary>
    public CompilerOptionEntry[]? CompilerOptionEntries = null;

    /// <summary>
    ///     Whether to skip SPIRV validation.
    /// </summary>
    public bool SkipSPIRVValidation = false;

    public SessionDesc() { }
}

internal unsafe struct SessionDescUnmanaged
{
    public nuint structureSize;
    public TargetDescUnmanaged* targets;
    public long targetCount;
    public SessionFlags flags;
    public SlangMatrixLayoutMode defaultMatrixLayoutMode;
    public byte** searchPaths;
    public long searchPathCount;
    public PreprocessorMacroDescUnmanaged* preprocessorMacros;
    public long preprocessorMacroCount;
    public nint fileSystem;
    public bool enableEffectAnnotations;
    public bool allowGLSLSyntax;
    public CompilerOptionEntryUnmanaged* compilerOptionEntries;
    public uint compilerOptionEntryCount;
    public bool skipSPIRVValidation;
}

[CustomMarshaller(typeof(SessionDesc), MarshalMode.Default, typeof(SessionDescMarshaller))]
internal static unsafe class SessionDescMarshaller
{
    public static SessionDescUnmanaged ConvertToUnmanaged(SessionDesc managed)
    {
        return new SessionDescUnmanaged
        {
            structureSize = (nuint)sizeof(SessionDescUnmanaged),
            targets = TargetDescMarshaller.ConvertToUnmanagedArray(managed.Targets, out var targetCount),
            targetCount = targetCount,
            flags = managed.Flags,
            defaultMatrixLayoutMode = managed.DefaultMatrixLayoutMode,
            searchPaths = ConvertStringArrayToUnmanaged(managed.SearchPaths),
            searchPathCount = managed.SearchPaths?.Length ?? 0,
            preprocessorMacros = PreprocessorMacroDescMarshaller.ConvertToUnmanagedArray(managed.PreprocessorMacros, out var preprocessorMacroCount),
            preprocessorMacroCount = preprocessorMacroCount,
            //FileSystem = managed.FileSystem != null ? ISlangFileSystemMarshaller,
            enableEffectAnnotations = managed.EnableEffectAnnotations,
            allowGLSLSyntax = managed.AllowGLSLSyntax,
            compilerOptionEntries = CompilerOptionEntryMarshaller.ConvertToUnmanagedArray(managed.CompilerOptionEntries, out var compilerOptionEntryCount),
            compilerOptionEntryCount = (uint)compilerOptionEntryCount,
            skipSPIRVValidation = managed.SkipSPIRVValidation
        };
    }

    public static SessionDesc ConvertToManaged(SessionDescUnmanaged unmanaged)
    {
        return new SessionDesc
        {
            Targets = TargetDescMarshaller.ConvertToManagedArray(unmanaged.targets, (int)unmanaged.targetCount),
            Flags = unmanaged.flags,
            DefaultMatrixLayoutMode = unmanaged.defaultMatrixLayoutMode,
            SearchPaths = ConvertStringArrayToManaged(unmanaged.searchPaths, (int)unmanaged.searchPathCount),
            PreprocessorMacros = PreprocessorMacroDescMarshaller.ConvertToManagedArray(unmanaged.preprocessorMacros, (int)unmanaged.preprocessorMacroCount),
            //FileSystem = unmanaged.FileSystem,
            EnableEffectAnnotations = unmanaged.enableEffectAnnotations,
            AllowGLSLSyntax = unmanaged.allowGLSLSyntax,
            CompilerOptionEntries = CompilerOptionEntryMarshaller.ConvertToManagedArray(unmanaged.compilerOptionEntries, (int)unmanaged.compilerOptionEntryCount),
            SkipSPIRVValidation = unmanaged.skipSPIRVValidation
        };
    }

    public static void Free(SessionDescUnmanaged unmanaged)
    {
        TargetDescMarshaller.FreeArray(unmanaged.targets, (int)unmanaged.targetCount);
        FreeStringArray(unmanaged.searchPaths, (int)unmanaged.searchPathCount);
        PreprocessorMacroDescMarshaller.FreeArray(unmanaged.preprocessorMacros, (int)unmanaged.preprocessorMacroCount);
        CompilerOptionEntryMarshaller.FreeArray(unmanaged.compilerOptionEntries, (int)unmanaged.compilerOptionEntryCount);
    }

    private static byte** ConvertStringArrayToUnmanaged(string[]? strings)
    {
        if (strings == null || strings.Length == 0)
            return null;

        byte** result = (byte**)NativeMemory.Alloc((nuint)(strings.Length * sizeof(byte*)));
        for (var i = 0; i < strings.Length; i++)
        {
            result[i] = Utf8StringMarshaller.ConvertToUnmanaged(strings[i]);
        }

        return result;
    }

    private static string[]? ConvertStringArrayToManaged(byte** strings, int count)
    {
        if (strings == null || count == 0)
            return null;

        var result = new string[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = Utf8StringMarshaller.ConvertToManaged(strings[i]) ?? string.Empty;
        }

        return result;
    }

    private static void FreeStringArray(byte** strings, int count)
    {
        if (strings == null)
            return;

        for (var i = 0; i < count; i++)
        {
            Utf8StringMarshaller.Free(strings[i]);
        }

        NativeMemory.Free(strings);
    }
}
