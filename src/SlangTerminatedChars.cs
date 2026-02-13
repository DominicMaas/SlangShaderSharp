using System.Runtime.InteropServices;

namespace SlangShaderSharp;

/// <summary>
///     Can be requested from <see cref="ISlangCastable" /> cast to indicate the contained chars are null terminated.
///     <para>
///         Use <see cref="ISlangCastable.CastAs"/> with <see cref="typeof(SlangTerminatedChars).GUID"/> to obtain a pointer to a UTF-8 string.
///     </para>
/// </summary>
[Guid("be0db1a8-3594-4603-a78b-c4868430dfbb")]
public struct SlangTerminatedChars
{
}