using System.Runtime.CompilerServices;

namespace SlangShaderSharp;

/// <summary>
///     Description of a Slang global session.
/// </summary>
public struct SlangGlobalSessionDesc
{
    /// <summary>
    ///    Size of this struct.
    /// </summary>
    public uint structureSize;

    /// <summary>
    ///     Slang API version.
    /// </summary>
    public uint apiVersion;

    /// <summary>
    ///     Specify the oldest Slang language version that any sessions will use.
    /// </summary>
    public SlangLanguageVersion minLanguageVersion;

    /// <summary>
    ///     Whether to enable GLSL support.
    /// </summary>
    public bool enableGLSL;

    /// <summary>
    ///     Reserved for future use.
    /// </summary>
    public _reserved_e__FixedBuffer reserved;

    [InlineArray(16)]
    public partial struct _reserved_e__FixedBuffer
    {
        public uint e0;
    }
}
