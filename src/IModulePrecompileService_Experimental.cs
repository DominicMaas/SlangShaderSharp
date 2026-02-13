using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     Experimental interface for doing target precompilation of slang modules.
/// </summary>
[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("8e12e8e3-5fcd-433e-afcb-13a088bc5ee5")]
public unsafe partial interface IModulePrecompileService_Experimental
{
    [PreserveSig]
    SlangResult PrecompileForTarget(
        SlangCompileTarget target,
        out ISlangBlob diagnostics);

    [PreserveSig]
    SlangResult GetPrecompiledTargetCode(
        SlangCompileTarget target,
        out ISlangBlob outCode,
        out ISlangBlob? diagnostics);

    [PreserveSig]
    nint GetModuleDependencyCount();

    [PreserveSig]
    SlangResult GetModuleDependency(
        nint dependencyIndex,
        out IModule module,
        out ISlangBlob? diagnostics);
}