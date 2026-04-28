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
    Include = 6,
    Language = 7,
    MatrixLayoutColumn = 8,
    MatrixLayoutRow = 9,
    ZeroInitialize = 10,
    IgnoreCapabilities = 11,
    RestrictiveCapabilityCheck = 12,
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
    EnableExperimentalDynamicDispatch = 109,
    EmitReflectionJSON = 110,
    CountOfParsableOptions = 111,
    DebugInformationFormat = 112,
    VulkanBindShiftAll = 113,
    GenerateWholeProgram = 114,
    UseUpToDateBinaryModule = 115,
    EmbedDownstreamIR = 116,
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

    CountOf,
}
