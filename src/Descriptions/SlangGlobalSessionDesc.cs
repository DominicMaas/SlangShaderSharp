using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SlangShaderSharp;

/// <summary>
///     Description of a Slang global session.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SlangGlobalSessionDesc
{
    /// <summary>
    ///    Size of this struct.
    /// </summary>
    public uint StructureSize = (uint)Unsafe.SizeOf<SlangGlobalSessionDesc>();

    /// <summary>
    ///     Slang API version.
    /// </summary>
    public uint ApiVersion = (uint)Slang.ApiVersion;

    /// <summary>
    ///     Specify the oldest Slang language version that any sessions will use.
    /// </summary>
    public SlangLanguageVersion MinLanguageVersion = SlangLanguageVersion.V2025;

    /// <summary>
    ///     Whether to enable GLSL support.
    /// </summary>
    public bool EnableGLSL = false;

    /// <summary>
    ///     Reserved for future use.
    /// </summary>
    public ReservedFixedBuffer Reserved;

    [InlineArray(16)]
    public partial struct ReservedFixedBuffer
    {
        public uint e0;
    }

    public SlangGlobalSessionDesc() { }
}
