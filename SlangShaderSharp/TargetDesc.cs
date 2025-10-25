namespace SlangShaderSharp;

public unsafe partial struct TargetDesc
{
    public nuint structureSize;
    public SlangCompileTarget format;
    public SlangProfileID profile;
    public uint flags;
    public SlangFloatingPointMode floatingPointMode;
    public SlangLineDirectiveMode lineDirectiveMode;
    public bool forceGLSLScalarBufferLayout;
    public CompilerOptionEntry* compilerOptionEntries;
    public uint compilerOptionEntryCount;
}
