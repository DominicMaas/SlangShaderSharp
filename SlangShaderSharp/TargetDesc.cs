using System.Runtime.InteropServices.Marshalling;

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

public struct TargetDescription
{
    public SlangCompileTarget Format;
    public SlangProfileID Profile;
    public uint Flags;
    public SlangFloatingPointMode FloatingPointMode;
    public SlangLineDirectiveMode LineDirectiveMode;
    public bool ForceGLSLScalarBufferLayout;
    public CompilerOptionEntry[] CompilerOptionEntries;
}

[CustomMarshaller(typeof(TargetDescription), MarshalMode.Default, typeof(TargetDescriptionMarshaller))]
internal static unsafe class TargetDescriptionMarshaller
{
    public static TargetDescriptionUnmanaged ConvertToUnmanaged(TargetDescription managed)
    {
        fixed (CompilerOptionEntry* ptr = managed.CompilerOptionEntries)
        {
            return new TargetDescriptionUnmanaged()
            {
                structureSize = (nuint)sizeof(TargetDescriptionUnmanaged),
                format = managed.Format,
                profile = managed.Profile,
                flags = managed.Flags,
                floatingPointMode = managed.FloatingPointMode,
                lineDirectiveMode = managed.LineDirectiveMode,
                forceGLSLScalarBufferLayout = managed.ForceGLSLScalarBufferLayout,
                compilerOptionEntries = ptr,
                compilerOptionEntryCount = (uint)(managed.CompilerOptionEntries?.Length ?? 0),
            };
        }
    }

    internal unsafe partial struct TargetDescriptionUnmanaged
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
}