using SlangShaderSharp.Internal;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("8044a8a3-ddc0-4b7f-af8e-026e905d7332")]
public unsafe partial interface IMetadata : ISlangCastable
{
    /// <summary>
    ///     Returns whether a resource parameter at the specified binding location is actually being used
    ///     in the compiled shader.
    /// </summary>
    [PreserveSig]
    SlangResult IsParameterLocationUsed(
        SlangParameterCategory category,
        uint spaceIndex,
        uint registerIndex,
        [MarshalAs(UnmanagedType.Bool)] out bool used);

    /// <summary>
    ///     Returns the debug build identifier for a base and debug spirv pair.
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    string GetDebugBuildIdentifier();
}
