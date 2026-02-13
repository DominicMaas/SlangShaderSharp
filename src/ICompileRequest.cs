using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     A request for one or more compilation actions to be performed.
/// </summary>
[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("96d33993-317c-4db5-afd8-666ee77248e2")]
public partial interface ICompileRequest
{
    /// <summary>
    ///     Set the filesystem hook to use for a compile request
    ///
    ///     The provided `fileSystem` will be used to load any files that
    ///     need to be loaded during processing of the compile `request`.
    ///     This includes:
    ///
    ///     - Source files loaded via `spAddTranslationUnitSourceFile`
    ///     - Files referenced via `#include`
    ///     - Files loaded to resolve `#import` operations
    /// </summary>
    [PreserveSig]
    void SetFileSystem(ISlangFileSystem fileSystem);

    /// <summary>
    ///     Set flags to be used for compilation.
    /// </summary>
    [PreserveSig]
    void SetCompileFlags(SlangCompileFlags flags);

    /// <summary>
    ///     Returns the compilation flags previously set with `setCompileFlags`
    /// </summary>
    [PreserveSig]
    SlangCompileFlags GetCompileFlags();

    /// <summary>
    ///     Set whether to dump intermediate results (for debugging) or not.
    /// </summary>
    [PreserveSig]
    void SetDumpIntermediates(int enable);

    [PreserveSig]
    void SetDumpIntermediatePrefix(string path);

    /// <summary>
    ///     Set whether (and how) `#line` directives should be output.
    /// </summary>
    [PreserveSig]
    void SetLineDirectiveMode(SlangLineDirectiveMode mode);

    /// <summary>
    ///     Sets the target for code generation.
    /// </summary>
    /// <param name="target"></param>
    [PreserveSig]
    void SetCodeGenTarget(SlangCompileTarget target);

    /// <summary>
    ///     Add a code-generation target to be used.
    /// </summary>
    [PreserveSig]
    int AddCodeGenTarget(SlangCompileTarget target);

    [PreserveSig]
    void SetTargetProfile(
        int targetIndex,
        SlangProfileID profile);

    [PreserveSig]
    void SetTargetFlags(
        int targetIndex,
        SlangTargetFlags flags);

    /// <summary>
    ///     Set the floating point mode (e.g., precise or fast) to use a target.
    /// </summary>
    [PreserveSig]
    void SetTargetFloatingPointMode(
        int targetIndex,
        SlangFloatingPointMode mode);

    /// <summary>
    ///     DEPRECATED: use `spSetMatrixLayoutMode` instead.
    /// </summary>
    [PreserveSig]
    void SetTargetMatrixLayoutMode(
        int targetIndex,
        SlangMatrixLayoutMode mode);

    [PreserveSig]
    void SetMatrixLayoutMode(SlangMatrixLayoutMode mode);

    /// <summary>
    ///     Set the level of debug information to produce.
    /// </summary>
    /// <param name="level"></param>
    [PreserveSig]
    void SetDebugInfoLevel(SlangDebugInfoLevel level);

    /// <summary>
    ///     Set the level of optimization to perform.
    /// </summary>
    /// <param name="level"></param>
    [PreserveSig]
    void SetOptimizationLevel(SlangOptimizationLevel level);

    /// <summary>
    ///     Set the container format to be used for binary output.
    /// </summary>
    [PreserveSig]
    void SetOutputContainerFormat(SlangContainerFormat format);

    [PreserveSig]
    void SetPassThrough(SlangPassThrough passThrough);

    [PreserveSig]
    unsafe void SetDiagnosticCallback(
        delegate* unmanaged[Cdecl]<nint, void*, void> callback,
        void* userData);

    [PreserveSig]
    void SetWriter(
        SlangWriterChannel channel,
        out ISlangWriter writer);

    [PreserveSig]
    ISlangWriter GetWriter(SlangWriterChannel channel);

    /// <summary>
    ///     Add a path to use when searching for referenced files.
    ///     This will be used for both `#include` directives and also for explicit `__import` declarations.
    /// </summary>
    /// <param name="searchDir">The additional search directory.</param>
    [PreserveSig]
    void AddSearchPath(string searchDir);

    /// <summary>
    ///     Add a macro definition to be used during preprocessing.
    /// </summary>
    /// <param name="key">The name of the macro to define.</param>
    /// <param name="value">The value of the macro to define.</param>
    [PreserveSig]
    void AddPreprocessorDefine(
        string key,
        string value);

    /// <summary>
    ///     Set options using arguments as if specified via command line.
    /// </summary>
    /// <returns> Returns SlangResult. On success SLANG_SUCCEEDED(result) is true.</returns>
    [PreserveSig]
    SlangResult ProcessCommandLineArguments(
        [In]
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPUTF8Str)]
        string[] args,
        int argCount);

    /// <summary>
    ///     Add a distinct translation unit to the compilation request
    ///     `name` is optional.
    /// </summary>
    /// <returns>Returns the zero-based index of the translation unit created.</returns>
    [PreserveSig]
    int AddTranslationUnit(
        SlangSourceLanguage language,
        string? name);

    /// <summary>
    ///     Set a default module name. Translation units will default to this module name if one is not
    ///     passed. If not set each translation unit will get a unique name.
    /// </summary>
    [PreserveSig]
    void SetDefaultModuleName(string defaultModuleName);

    /// <summary>
    ///     Add a preprocessor definition that is scoped to a single translation unit.
    /// </summary>
    /// <param name="translationUnitIndex">The index of the translation unit to get the definition.</param>
    /// <param name="key">The name of the macro to define.</param>
    /// <param name="value">The value of the macro to define.</param>
    [PreserveSig]
    void AddTranslationUnitPreprocessorDefine(
        int translationUnitIndex,
        string key,
        string value);

    /// <summary>
    ///     Add a source file to the given translation unit.
    ///
    ///     If a user-defined file system has been specified via
    ///     `spSetFileSystem`, then it will be used to load the
    ///     file at `path`. Otherwise, Slang will use the OS
    ///     file system.
    ///
    ///     This function does *not* search for a file using
    ///     the registered search paths (`spAddSearchPath`),
    ///     and instead using the given `path` as-is.
    /// </summary>
    [PreserveSig]
    void AddTranslationUnitSourceFile(
        int translationUnitIndex,
        string path);

    [PreserveSig]
    void AddTranslationUnitSourceString(
        int translationUnitIndex,
        string path,
        string source);

    [PreserveSig]
    unsafe SlangResult AddLibraryReference(
        string basePath,
        void* libData,
        nuint libDataSize);

    // TODO: The rest of this interface

    /// <summary>
    ///     Enable repro capture.
    ///
    ///     Should be set after any ISlangFileSystem has been set, but before any compilation. It ensures
    ///     that everything that the ISlangFileSystem accesses will be correctly recorded. Note that if a
    ///     ISlangFileSystem/ISlangFileSystemExt isn't explicitly set (ie the default is used), then the
    ///     request will automatically be set up to record everything appropriate.
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    SlangResult EnableReproCapture();

    /// <summary>
    ///     Get the (linked) program for a compile request.
    ///
    ///     The linked program will include all of the global-scope modules for the
    ///     translation units in the program, plus any modules that they `import`
    ///     (transitively), specialized to any global specialization arguments that
    ///     were provided via the API.
    /// </summary>
    [PreserveSig]
    SlangResult GetProgram(out IComponentType program);

    /// <summary>
    ///     Get the (partially linked) component type for an entry point.
    ///
    ///     The returned component type will include the entry point at the
    ///     given index, and will be specialized using any specialization arguments
    ///     that were provided for it via the API.
    ///
    ///     The returned component will* not* include the modules representing
    ///     the global scope and its dependencies/specialization, so a client
    ///     program will typically want to compose this component type with
    ///     the one returned by `spCompileRequest_getProgram` to get a complete
    ///     and usable component type from which kernel code can be requested.
    /// </summary>
    [PreserveSig]
    SlangResult GetEntryPoint(
        nint entryPointIndex,
        out IComponentType entryPoint);

    /// <summary>
    ///     Get the (un-linked) module for a translation unit.
    ///
    ///     The returned module will not be linked against any dependencies,
    ///     nor against any entry points (even entry points declared inside
    ///     the module). Similarly, the module will not be specialized
    ///     to the arguments that might have been provided via the API.
    ///
    ///     This function provides an atomic unit of loaded code that
    ///     is suitable for looking up types and entry points in the
    ///     given module, and for linking together to produce a composite
    ///     program that matches the needs of an application.
    /// </summary>
    [PreserveSig]
    SlangResult GetModule(
        nint translationUnitIndex,
        out IModule module);

    /// <summary>
    ///     Get the `ISession` handle behind the `SlangCompileRequest`.
    /// </summary>
    [PreserveSig]
    SlangResult GetSession(out ISession session);

    /// <summary>
    ///     Get reflection data from a compilation request
    /// </summary>
    [PreserveSig]
    ShaderReflection GetReflection();

    /// <summary>
    ///     Make output specially handled for command line output
    /// </summary>
    [PreserveSig]
    void SetCommandLineCompilerMode();

    /// <summary>
    ///     Add a defined capability that should be assumed available on the target
    /// </summary>
    [PreserveSig]
    SlangResult AddTargetCapability(
        nint targetIndex,
        SlangCapabilityID capability);

    /// <summary>
    ///     Get the (linked) program for a compile request, including all entry points.
    ///
    ///     The resulting program will include all of the global-scope modules for the
    ///     translation units in the program, plus any modules that they `import`
    ///     (transitively), specialized to any global specialization arguments that
    ///     were provided via the API, as well as all entry points specified for compilation,
    ///     specialized to their entry-point specialization arguments.
    /// </summary>
    [PreserveSig]
    SlangResult GetProgramWithEntryPoints(out IComponentType program);

    [PreserveSig]
    SlangResult IsParameterLocationUsed(
        nint entryPointIndex,
        nint targetIndex,
        SlangParameterCategory category,
        nuint spaceIndex,
        nuint registerIndex,
        [MarshalAs(UnmanagedType.I1)] out bool used);

    /// <summary>
    ///     Set the line directive mode for a target.
    /// </summary>
    [PreserveSig]
    void SetTargetLineDirectiveMode(
        nint targetIndex,
        SlangLineDirectiveMode value);

    /// <summary>
    ///     Set whether to use scalar buffer layouts for GLSL/Vulkan targets.
    ///     If true, the generated GLSL/Vulkan code will use `scalar` layout for storage buffers.
    ///     If false, the resulting code will std430 for storage buffers.
    /// </summary>
    [PreserveSig]
    void SetTargetForceGLSLScalarBufferLayout(
        int targetIndex,
        [MarshalAs(UnmanagedType.I1)] bool value);

    /// <summary>
    ///     Overrides the severity of a specific diagnostic message.
    /// </summary>
    /// <param name="messageId">Numeric identifier of the message to override, as defined in the 1st parameter of the DIAGNOSTIC macro.</param>
    /// <param name="overrideSeverity">New severity of the message. If the message is originally Error or Fatal, the new severity cannot be lower than that.</param>
    [PreserveSig]
    void OverrideDiagnosticSeverity(
        nint messageId,
        SlangSeverity overrideSeverity);

    /// <summary>
    ///     Sets the flags of the request's diagnostic sink.
    ///     The previously specified flags are discarded.
    /// </summary>
    [PreserveSig]
    void SetDiagnosticFlags(SlangDiagnosticFlags flags);

    /// <summary>
    ///     Set the debug format to be used for debugging information
    /// </summary>
    [PreserveSig]
    void SetDebugInfoFormat(SlangDebugInfoFormat debugFormat);

    [PreserveSig]
    void SetEnableEffectAnnotations([MarshalAs(UnmanagedType.I1)] bool value);

    [PreserveSig]
    void SetReportDownstreamTime([MarshalAs(UnmanagedType.I1)] bool value);

    [PreserveSig]
    void SetReportPerfBenchmark([MarshalAs(UnmanagedType.I1)] bool value);

    [PreserveSig]
    void SetSkipSPIRVValidation([MarshalAs(UnmanagedType.I1)] bool value);

    [PreserveSig]
    SlangResult SetTargetUseMinimumSlangOptimization(
        int targetIndex,
        [MarshalAs(UnmanagedType.I1)] bool value);

    [PreserveSig]
    void SetIgnoreCapabilityCheck([MarshalAs(UnmanagedType.I1)] bool value);

    /// <summary>
    ///     Return a copy of internal profiling results, and if `shouldClear` is true, clear the internal
    ///     profiling results before returning.
    /// </summary>
    [PreserveSig]
    SlangResult GetCompileTimeProfile(
        out ISlangProfiler compileTimeProfile,
        [MarshalAs(UnmanagedType.I1)] bool shouldClear);

    [PreserveSig]
    void SetTargetGenerateWholeProgram(
        int targetIndex,
        [MarshalAs(UnmanagedType.I1)] bool value);

    [PreserveSig]
    void SetTargetForceDXLayout(
        int targetIndex,
        [MarshalAs(UnmanagedType.I1)] bool value);

    [PreserveSig]
    void SetTargetEmbedDownstreamIR(
        int targetIndex,
        [MarshalAs(UnmanagedType.I1)] bool value);

    [PreserveSig]
    void SetTargetForceCLayout(
        int targetIndex,
        [MarshalAs(UnmanagedType.I1)] bool value);
}
