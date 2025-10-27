using System.Runtime.CompilerServices;

namespace SlangShaderSharp;

public struct SlangGlobalSessionDesc
{
    public uint structureSize;

    public uint apiVersion;

    public uint minLanguageVersion;

    public bool enableGLSL;

    public _reserved_e__FixedBuffer reserved;

    [InlineArray(16)]
    public partial struct _reserved_e__FixedBuffer
    {
        public uint e0;
    }
}
