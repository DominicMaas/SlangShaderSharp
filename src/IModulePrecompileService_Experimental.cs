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
    /// <summary>
    ///     Precompile this module for a target and embed the resulting target library in the module.
    ///
    ///     This function is experimental and not thread-safe since it mutates the module by adding
    ///     precompiled target IR and temporary export metadata. Callers must externally synchronize
    ///     access to the module and must not use this API concurrently with other operations on the
    ///     same module or session.
    /// </summary>
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