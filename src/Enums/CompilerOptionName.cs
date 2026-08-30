namespace SlangShaderSharp;

/// <summary>
///     All compiler option names supported by Slang.
/// </summary>
public enum CompilerOptionName
{
    /// <summary> stringValue0: macro name; stringValue1: macro value </summary>
    MacroDefine = 0,

    DepFile = 1,
    EntryPointName = 2,
    Specialize = 3,
    Help = 4,
    HelpStyle = 5,

    /// <summary> stringValue: additional include path. </summary>
    Include = 6,

    Language = 7,

    /// <summary> bool </summary>
    MatrixLayoutColumn = 8,

    /// <summary> bool </summary>
    MatrixLayoutRow = 9,

    /// <summary> bool </summary>
    ZeroInitialize = 10,

    /// <summary> bool </summary>
    IgnoreCapabilities = 11,

    /// <summary> bool </summary>
    RestrictiveCapabilityCheck = 12,

    /// <summary> stringValue0: module name. </summary>
    ModuleName = 13,

    Output = 14,

    /// <summary> intValue0: profile </summary>
    Profile = 15,

    /// <summary> intValue0: stage </summary>
    Stage = 16,

    /// <summary> intValue0: CodeGenTarget </summary>
    Target = 17,

    Version = 18,

    /// <summary> stringValue0: "all" or comma-separated list of warning codes or names. </summary>
    WarningsAsErrors = 19,

    /// <summary> stringValue0: comma separated list of warning codes or names. </summary>
    DisableWarnings = 20,

    /// <summary> stringValue0: warning code or name. </summary>
    EnableWarning = 21,

    /// <summary> stringValue0: warning code or name. </summary>
    DisableWarning = 22,

    DumpWarningDiagnostics = 23,
    InputFilesRemain = 24,

    /// <summary> bool </summary>
    EmitIr = 25,

    /// <summary> bool </summary>
    ReportDownstreamTime = 26,

    /// <summary> bool </summary>
    ReportPerfBenchmark = 27,

    /// <summary> bool </summary>
    ReportCheckpointIntermediates = 28,

    /// <summary> bool </summary>
    SkipSPIRVValidation = 29,

    SourceEmbedStyle = 30,
    SourceEmbedName = 31,
    SourceEmbedLanguage = 32,

    /// <summary> bool </summary>
    DisableShortCircuit = 33,

    /// <summary> bool </summary>
    MinimumSlangOptimization = 34,

    /// <summary> bool </summary>
    DisableNonEssentialValidations = 35,

    /// <summary> bool </summary>
    DisableSourceMap = 36,

    /// <summary> bool </summary>
    UnscopedEnum = 37,

    /// <summary> bool: preserve all resource parameters in the output code. </summary>
    PreserveParameters = 38,

    // Target

    /// <summary> intValue0: CapabilityName </summary>
    Capability = 39,

    /// <summary> bool </summary>
    DefaultImageFormatUnknown = 40,

    /// <summary> bool </summary>
    DisableDynamicDispatch = 41,

    /// <summary> bool </summary>
    DisableSpecialization = 42,

    /// <summary> intValue0: FloatingPointMode </summary>
    FloatingPointMode = 43,

    /// <summary> intValue0: DebugInfoLevel </summary>
    DebugInformation = 44,

    LineDirectiveMode = 45,

    /// <summary> intValue0: OptimizationLevel </summary>
    Optimization = 46,

    /// <summary> bool </summary>
    Obfuscate = 47,

    /// <summary> intValue0 (higher 8 bits): kind; intValue0 (lower bits): set; intValue1: shift </summary>
    VulkanBindShift = 48,

    /// <summary> intValue0: index; intValue1: set </summary>
    VulkanBindGlobals = 49,

    /// <summary> bool </summary>
    VulkanInvertY = 50,

    /// <summary> bool </summary>
    VulkanUseDxPositionW = 51,

    /// <summary> bool </summary>
    VulkanUseEntryPointName = 52,

    /// <summary> bool </summary>
    VulkanUseGLLayout = 53,

    /// <summary> bool </summary>
    VulkanEmitReflection = 54,

    /// <summary> bool </summary>
    GLSLForceScalarLayout = 55,

    /// <summary> bool </summary>
    EnableEffectAnnotations = 56,

    /// <summary> bool (will be deprecated) </summary>
    EmitSpirvViaGLSL = 57,

    /// <summary> bool (will be deprecated) </summary>
    EmitSpirvDirectly = 58,

    /// <summary> stringValue0: json path </summary>
    SPIRVCoreGrammarJSON = 59,

    /// <summary>
    ///     bool, when set, will not issue an error when the linked program
    ///     has unresolved extern function symbols.
    /// </summary>
    IncompleteLibrary = 60,

    // Downstream

    CompilerPath = 61,
    DefaultDownstreamCompiler = 62,

    /// <summary>
    ///     stringValue0: downstream compiler name. stringValue1: argument list,
    ///     one per line.
    /// </summary>
    DownstreamArgs = 63,

    PassThrough = 64,

    // Repro

    DumpRepro = 65,
    DumpReproOnError = 66,
    ExtractRepro = 67,
    LoadRepro = 68,
    LoadReproDirectory = 69,
    ReproFallbackDirectory = 70,

    // Debugging

    DumpAst = 71,
    DumpIntermediatePrefix = 72,

    /// <summary> bool </summary>
    DumpIntermediates = 73,

    /// <summary> bool </summary>
    DumpIr = 74,

    DumpIrIds = 75,
    PreprocessorOutput = 76,
    OutputIncludes = 77,
    ReproFileSystem = 78,

    /// <summary> deprecated and removed; value must never be reused </summary>
    [Obsolete("Deprecated and removed in the native API. Retained only so the value is never reused.")]
    REMOVED_SerialIR = 79,

    /// <summary> bool </summary>
    SkipCodeGen = 80,

    /// <summary> bool </summary>
    ValidateIr = 81,

    VerbosePaths = 82,
    VerifyDebugSerialIr = 83,

    /// <summary> Not used. </summary>
    NoCodeGen = 84,

    // Experimental

    FileSystem = 85,
    Heterogeneous = 86,
    NoMangle = 87,
    NoHLSLBinding = 88,
    NoHLSLPackConstantBufferElements = 89,
    ValidateUniformity = 90,
    AllowGLSL = 91,
    EnableExperimentalPasses = 92,

    /// <summary> int </summary>
    BindlessSpaceIndex = 93,

    /// <summary> int: byte stride for SPIRV resource descriptor heap </summary>
    SPIRVResourceHeapStride = 94,

    /// <summary> int: byte stride for SPIRV sampler descriptor heap </summary>
    SPIRVSamplerHeapStride = 95,

    // Internal

    ArchiveType = 96,
    CompileCoreModule = 97,
    Doc = 98,

    /// <summary> deprecated; value must never be reused </summary>
    [Obsolete("Deprecated in the native API. Retained only so the value is never reused.")]
    IrCompression = 99,

    LoadCoreModule = 100,
    ReferenceModule = 101,
    SaveCoreModule = 102,
    SaveCoreModuleBinSource = 103,
    TrackLiveness = 104,

    /// <summary> bool, enable loop inversion optimization </summary>
    LoopInversion = 105,

    /// <summary> Deprecated; value must never be reused </summary>
    [Obsolete("Deprecated in the native API; this behavior is now enabled unconditionally. Retained only so the value is never reused.")]
    ParameterBlocksUseRegisterSpaces = 106,

    /// <summary> intValue0: SlangLanguageVersion </summary>
    LanguageVersion = 107,

    /// <summary>
    ///     stringValue0: type conformance to link; format:
    ///     <c>"&lt;TypeName&gt;:&lt;IInterfaceName&gt;[=&lt;sequentialId&gt;]"</c>,
    ///     e.g. <c>"Impl:IFoo=3"</c> or <c>"Impl:IFoo"</c>.
    /// </summary>
    TypeConformance = 108,

    /// <summary> bool, experimental </summary>
    EnableExperimentalDynamicDispatch = 109,

    /// <summary> bool </summary>
    EmitReflectionJSON = 110,

    /// <summary> historical sentinel; value must not be reused </summary>
    CountOfParsableOptions = 111,

    // Options added after the original set. Most have CLI flags; a few are
    // API-only (marked below). All future additions belong after DiagnosticColor,
    // immediately before CountOf.

    /// <summary> intValue0: DebugInfoFormat (derived from -g; no direct CLI flag) </summary>
    DebugInformationFormat = 112,

    /// <summary> intValue0: kind; intValue1: shift (derived from -fvk-x-shift; no direct CLI flag) </summary>
    VulkanBindShiftAll = 113,

    /// <summary> bool </summary>
    GenerateWholeProgram = 114,

    /// <summary> bool, when set, will only load precompiled modules if up-to-date with source. (API-only; no direct CLI flag) </summary>
    UseUpToDateBinaryModule = 115,

    /// <summary> bool </summary>
    EmbedDownstreamIR = 116,

    /// <summary> bool </summary>
    ForceDXLayout = 117,

    /// <summary>
    ///     enum SlangEmitSpirvMethod (derived; no direct CLI flag).
    ///
    ///     Setting of <see cref="EmitSpirvDirectly"/> or <see cref="EmitSpirvViaGLSL"/> will turn
    ///     into this option internally.
    /// </summary>
    EmitSpirvMethod = 118,

    SaveGLSLModuleBinSource = 119,

    /// <summary> bool, experimental (API-only; no direct CLI flag) </summary>
    SkipDownstreamLinking = 120,

    DumpModule = 121,

    /// <summary> Print serialized module version and name </summary>
    GetModuleInfo = 122,

    /// <summary> Print the min and max module versions this compiler supports </summary>
    GetSupportedModuleVersions = 123,

    /// <summary> bool </summary>
    EmitSeparateDebug = 124,

    // Floating point denormal handling modes

    DenormalModeFp16 = 125,
    DenormalModeFp32 = 126,
    DenormalModeFp64 = 127,

    // Bitfield options

    /// <summary> bool </summary>
    UseMSVCStyleBitfieldPacking = 128,

    /// <summary> bool </summary>
    ForceCLayout = 129,

    /// <summary> bool, enable experimental features </summary>
    ExperimentalFeature = 130,

    /// <summary> bool, reports detailed compiler performance benchmark results </summary>
    ReportDetailedPerfBenchmark = 131,

    /// <summary> bool, enable detailed IR validation </summary>
    ValidateIRDetailed = 132,

    /// <summary> string, pass name to dump IR before </summary>
    DumpIRBefore = 133,

    /// <summary> string, pass name to dump IR after </summary>
    DumpIRAfter = 134,

    /// <summary> enum SlangEmitCPUMethod (derived; no direct CLI flag) </summary>
    EmitCPUMethod = 135,

    /// <summary> bool </summary>
    EmitCPUViaCPP = 136,

    /// <summary> bool </summary>
    EmitCPUViaLLVM = 137,

    /// <summary> string </summary>
    LLVMTargetTriple = 138,

    /// <summary> string </summary>
    LLVMCPU = 139,

    /// <summary> string </summary>
    LLVMFeatures = 140,

    /// <summary> bool, enable the experimental rich diagnostics </summary>
    EnableRichDiagnostics = 141,

    /// <summary> bool </summary>
    ReportDynamicDispatchSites = 142,

    /// <summary> bool, enable machine-readable diagnostic output (implies EnableRichDiagnostics) </summary>
    EnableMachineReadableDiagnostics = 143,

    /// <summary> intValue0: SlangDiagnosticColor (always, never, auto) </summary>
    DiagnosticColor = 144,

    // Add new options HERE, immediately before CountOf.

    /// <summary> bool: insert per-statement line coverage counters </summary>
    TraceCoverage = 145,

    /// <summary>
    ///     intValue0: register index; intValue1: register space - explicit
    ///     binding for the synthesized __slang_coverage buffer. Consumed
    ///     only when any coverage mode is enabled; the slangc CLI spelling
    ///     also enables TraceCoverage.
    /// </summary>
    TraceCoverageBinding = 146,

    /// <summary>
    ///     intValue0: descriptor/register space reserved by the host when
    ///     auto-allocating the synthesized __slang_coverage buffer. This is
    ///     a repeatable hint consumed only when any coverage mode is enabled.
    /// </summary>
    TraceCoverageReservedSpace = 147,

    /// <summary> bool: insert per-function-entry coverage counters </summary>
    TraceFunctionCoverage = 148,

    /// <summary> bool: insert per-branch-arm coverage counters </summary>
    TraceBranchCoverage = 149,

    /// <summary>
    ///     stringValue0: explicit path for the slangc coverage manifest sidecar.
    ///     When unset, slangc writes <c>&lt;output&gt;.coverage-manifest.json</c> next to
    ///     file outputs that carry coverage metadata. This option is output
    ///     policy only and is excluded from compiler cache keys. It requires
    ///     at least one coverage tracing mode, is rejected for container
    ///     outputs, and errors if the selected outputs produce no coverage
    ///     metadata. Explicit paths are valid only when exactly one compiled
    ///     artifact carries coverage metadata and must not overlap any emitted
    ///     artifact path. Query/set with the string option APIs.
    /// </summary>
    CoverageManifestOutput = 150,

    /// <summary>
    ///     intValue0: per-slot byte width of the synthesized __slang_coverage
    ///     buffer. Accepts 4 (uint32) or 8 (uint64). Omitting the option
    ///     yields 8 when any coverage mode is enabled. Use 4 to opt down to
    ///     uint32 when the runtime driver lacks 64-bit shader atomic support
    ///     (notably MoltenVK on Apple Silicon, where Vulkan exposes
    ///     shaderBufferInt64Atomics = false). uint32 counters wrap silently
    ///     at 2^32 hits per slot; uint64 counters effectively do not wrap
    ///     within any practical run. The corresponding CLI flag
    ///     <c>-trace-coverage-counter-width &lt;bits&gt;</c> takes a bit count (32/64)
    ///     and stores the matching byte width here.
    /// </summary>
    TraceCoverageCounterByteWidth = 151,

    /// <summary>
    ///     bool: record boolean coverage (CoverageCounterMode::Boolean) instead of exact
    ///     execution counts. Each counter is written with a plain non-atomic store
    ///     of <c>1</c>, eliminating atomic contention (much faster, and avoids the GPU
    ///     watchdog timeouts heavy coverage can trigger) at the cost of exact
    ///     counts. Off by default.
    /// </summary>
    TraceCoverageBoolean = 152,

    /// <summary>
    ///     CLI-only query option <c>-&lt;compiler&gt;-version</c>: prints the version of the downstream
    ///     <c>&lt;compiler&gt;</c> Slang would actually load for that pass-through (via
    ///     IGlobalSession::getDownstreamCompilerVersion). It takes no value and is never stored on
    ///     an option set; it only drives the print-and-continue handler in the command-line parser.
    /// </summary>
    CompilerVersion = 153,

    /// <summary>
    ///     bool: when set, emit each SPIRV resource descriptor-heap runtime array's
    ///     ArrayStride as the maximum of image and buffer descriptor sizes, so a
    ///     single heap shared by buffers and images is indexed at the device's unified
    ///     stride. Opt-in; mutually exclusive with a non-zero
    ///     <c>-spirv-resource-heap-stride</c> (combining the two is an error).
    /// </summary>
    SPIRVUnifiedDescriptorHeapStride = 154,

    /// <summary>
    ///     intValue0: a SlangWarningLevel group to enable (e.g. <see cref="SlangWarningLevel.Pedantic"/>).
    ///     Repeatable: enabling multiple groups is additive, matching how -Wall/-Wextra/-Wpedantic
    ///     combine on the command line. CLI spellings: -Wall, -Wextra, -Wpedantic.
    /// </summary>
    WarningLevel = 155,

    /// <summary>
    ///     stringValue0: explicit path for the slangc separate-debug-info sidecar.
    ///     When unset, slangc derives the sidecar path from the main artifact path.
    ///     This option is output policy only and is excluded from compiler cache keys.
    ///     It requires <see cref="EmitSeparateDebug"/> and permits the main artifact to be written to
    ///     stdout. A value of <c>"-"</c> writes the separate debug information to stdout when
    ///     the main artifact is written to a file. Query/set with the string option APIs.
    /// </summary>
    SeparateDebugInfoOutput = 156,

    /// <summary>
    ///     bool: embed the shader source text into the debug information independently of
    ///     the overall <c>-g</c> debug level. At <c>-g1</c> (<see cref="SlangDebugInfoLevel.Minimal"/>)
    ///     the source is embedded via the core <c>OpSource</c> File+Source operands (no NonSemantic
    ///     extension); at <c>-g2</c>/<c>-g3</c> source is already embedded so the option is a no-op.
    ///     Requires debug information: using it with <c>-g0</c>, or without any <c>-g</c> option (both
    ///     resolve to no debug info), is an error. Only affects SPIR-V output.
    /// </summary>
    DebugInfoIncludeSource = 157,

    /// <summary>
    ///     int: Synthesize <c>__slang_coverage</c> as an unbounded descriptor array of
    ///     structured buffers rather than a single buffer, and index it with this value:
    ///     <c>__slang_coverage[N][slot]</c>. Many separately compiled shaders sharing one
    ///     pipeline then occupy a single descriptor binding instead of one binding each,
    ///     and each shader's buffer is sized independently by the host.
    ///     <para>
    ///     Where the array itself lives is a separate decision, made with
    ///     <see cref="TraceCoverageBinding"/> (or left to auto-allocation). Note for hosts:
    ///     if the descriptor array is declared with
    ///     <c>VK_DESCRIPTOR_BINDING_VARIABLE_DESCRIPTOR_COUNT_BIT</c>, Vulkan requires it to
    ///     be the highest-numbered binding in its set. A fixed <c>descriptorCount</c> carries
    ///     no such restriction. Either way it is the host's layout to satisfy and the
    ///     compiler cannot see it.
    ///     </para>
    ///     <para>
    ///     The index is a compile-time constant and therefore part of the compiled artifact:
    ///     a host that keys a shader cache on the compiled output must derive it from a
    ///     stable shader identity rather than from load order, or an unchanged shader
    ///     recompiles whenever that order shifts. SPIR-V and GLSL only.
    ///     </para>
    /// </summary>
    TraceCoverageBindlessIndex = 158,

    // Do not assign an explicit value to CountOf. It must remain one past the last option,
    // which it derives implicitly from the preceding (highest-valued) enumerator.
    CountOf,
}
