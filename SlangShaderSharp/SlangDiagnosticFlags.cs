namespace SlangShaderSharp;

[Flags]
public enum SlangDiagnosticFlags : int
{
    VerbosePaths = 0x01,
    TreatWarningsAsErrors = 0x02,
}
