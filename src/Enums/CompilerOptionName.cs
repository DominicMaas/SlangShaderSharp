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
    Profile = 15,
    Stage = 16,
    Target = 17,
    Version = 18,
    WarningsAsErrors = 19,
    DisableWarnings = 20,
    EnableWarning = 21,
    DisableWarning = 22,
    DumpWarningDiagnostics = 23,
    InputFilesRemain = 24,
    EmitIr = 25,
    ReportDownstreamTime = 26,
    ReportPerfBenchmark = 27,
    ReportCheckpointIntermediates = 28,
    SkipSPIRVValidation = 29,
    SourceEmbedStyle = 30,
    SourceEmbedName = 31,
    SourceEmbedLanguage = 32,
    DisableShortCircuit = 33,
    MinimumSlangOptimization = 34,
    DisableNonEssentialValidations = 35,
    DisableSourceMap = 36,
    UnscopedEnum = 37,
    PreserveParameters = 38,
    Capability = 39,
    DefaultImageFormatUnknown = 40,
    DisableDynamicDispatch = 41,
    DisableSpecialization = 42,
    FloatingPointMode = 43,
    DebugInformation = 44,
    LineDirectiveMode = 45,
    Optimization = 46,
    Obfuscate = 47,
    VulkanBindShift = 48,
    VulkanBindGlobals = 49,
    VulkanInvertY = 50,
    VulkanUseDxPositionW = 51,
    VulkanUseEntryPointName = 52,
    VulkanUseGLLayout = 53,
    VulkanEmitReflection = 54,
    GLSLForceScalarLayout = 55,
    EnableEffectAnnotations = 56,
    EmitSpirvViaGLSL = 57,
    EmitSpirvDirectly = 58,
    SPIRVCoreGrammarJSON = 59,
    IncompleteLibrary = 60,
    CompilerPath = 61,
    DefaultDownstreamCompiler = 62,
    DownstreamArgs = 63,
    PassThrough = 64,
    DumpRepro = 65,
    DumpReproOnError = 66,
    ExtractRepro = 67,
    LoadRepro = 68,
    LoadReproDirectory = 69,
    ReproFallbackDirectory = 70,
    DumpAst = 71,
    DumpIntermediatePrefix = 72,
    DumpIntermediates = 73,
    DumpIr = 74,
    DumpIrIds = 75,
    PreprocessorOutput = 76,
    OutputIncludes = 77,
    ReproFileSystem = 78,
    REMOVED_SerialIR = 79,
    SkipCodeGen = 80,
    ValidateIr = 81,
    VerbosePaths = 82,
    VerifyDebugSerialIr = 83,
    NoCodeGen = 84,
    FileSystem = 85,
    Heterogeneous = 86,
    NoMangle = 87,
    NoHLSLBinding = 88,
    NoHLSLPackConstantBufferElements = 89,
    ValidateUniformity = 90,
    AllowGLSL = 91,
    EnableExperimentalPasses = 92,
    BindlessSpaceIndex = 93,
    SpirvResourceHeapStride = 94,
    SpirvSamplerHeapStride = 95,
    ArchiveType = 96,
    CompileCoreModule = 97,
    Doc = 98,
    IrCompression = 99,
    LoadCoreModule = 100,
    ReferenceModule = 101,
    SaveCoreModule = 102,
    SaveCoreModuleBinSource = 103,
    TrackLiveness = 104,
    LoopInversion = 105,
    ParameterBlocksUseRegisterSpaces = 106,
    LanguageVersion = 107,
    TypeConformance = 108,

    /// <summary> bool, experimental </summary>
    EnableExperimentalDynamicDispatch = 109,

    /// <summary> bool </summary>
    EmitReflectionJSON = 110,

    /// <summary> historical sentinel; value must not be reused </summary>
    CountOfParsableOptions = 111,

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

    /// <summary> enum SlangEmitSpirvMethod (derived; no direct CLI flag) </summary>
    EmitSpirvMethod = 118,

    SaveGLSLModuleBinSource = 119,

    /// <summary> bool, experimental (API-only; no direct CLI flag) </summary>
    SkipDownstreamLinking = 120,

    DumpModule = 121,

    /// <summary> Print serialized module version and name </summary>
    GetModuleInfo = 122,

    /// <summary> Print the min and max module versions this compiler supports </summary>
    GetSupportedModuleVersions = 123,

    EmitSeparateDebug = 124,
    DenormalModeFp16 = 125,
    DenormalModeFp32 = 126,
    DenormalModeFp64 = 127,

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

    /// <summary>  enum SlangEmitCPUMethod (derived; no direct CLI flag) </summary>
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

    /// <summary> bool: insert per-statement line coverage counters </summary>
    TraceCoverage = 145,

    /// <summary>
    ///     intValue0: register index; intValue1: register space — explicit
    ///     binding for the synthesized __slang_coverage buffer. Consumed
    ///     only when TraceCoverage is enabled; the slangc CLI spelling also
    ///     enables TraceCoverage.
    /// </summary>
    TraceCoverageBinding = 146,

    /// <summary>
    ///     intValue0: descriptor/register space reserved by the host when
    ///     auto-allocating the synthesized __slang_coverage buffer. This is
    ///     a repeatable hint consumed only when TraceCoverage is enabled.
    /// </summary>
    TraceCoverageReservedSpace = 147,

    /// <summary>
    ///      bool: insert per-function-entry coverage counters
    /// </summary>
    TraceFunctionCoverage = 148,

    /// <summary>
    ///     bool: insert per-branch-arm coverage counters
    /// </summary>
    TraceBranchCoverage = 149,

    /// <summary>
    ///     stringValue0: explicit path for the slangc coverage manifest sidecar.
    ///     When unset, slangc writes <output>.coverage-manifest.json next to
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
    ///     `-trace-coverage-counter-width <bits>` takes a bit count (32/64)
    ///     and stores the matching byte width here.
    /// </summary>
    TraceCoverageCounterByteWidth = 151,

    /// <summary>
    ///     bool: record boolean coverage (CoverageCounterMode::Boolean) instead of exact
    ///     execution counts. Each counter is written with a plain non-atomic store
    ///     of `1`, eliminating atomic contention (much faster, and avoids the GPU
    ///     watchdog timeouts heavy coverage can trigger) at the cost of exact
    ///     counts. Off by default.
    /// </summary>
    TraceCoverageBoolean = 152,

    /// <summary>
    ///     CLI-only query option `-<compiler>-version`: prints the version of the downstream
    ///     <compiler> Slang would actually load for that pass-through (via
    ///     IGlobalSession::getDownstreamCompilerVersion). It takes no value and is never stored on
    ///     an option set; it only drives the print-and-continue handler in the command-line parser.
    /// </summary>
    CompilerVersion = 153,

    /// <summary>
    ///     bool: when set, emit each SPIRV resource descriptor-heap runtime array's
    ///     ArrayStride as the maximum of image and buffer descriptor sizes, so a
    ///     single heap shared by buffers and images is indexed at the device's unified
    ///     stride. Opt-in; mutually exclusive with a non-zero
    ///     `-spirv-resource-heap-stride` (combining the two is an error).
    /// </summary>
    SPIRVUnifiedDescriptorHeapStride = 154,

    /// <summary>
    ///     intValue0: a SlangWarningLevel group to enable (e.g. <see cref="SlangWarningLevel.Pedantic"/>).
    ///     Repeatable: enabling multiple groups is additive, matching how -Wall/-Wextra/-Wpedantic
    ///     combine on the command line. CLI spellings: -Wall, -Wextra, -Wpedantic.
    /// </summary>
    WarningLevel = 155,

    CountOf,
}
