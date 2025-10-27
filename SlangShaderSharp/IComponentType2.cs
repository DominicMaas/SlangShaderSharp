using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     IComponentType2 is a component type used for getting separate debug data.
///
///     This interface is used for getting separate debug data, introduced here to
///     avoid breaking backwards compatibility of the IComponentType interface.
///
///     The `getTargetCompileResult` and `getEntryPointCompileResult` functions
///     are used to get the base and debug spirv, and metadata containing the
///     debug build identifier.
/// </summary>
[GeneratedComInterface]
[Guid("9c2a4b3d-7f68-4e91-a52c-8b193e457a9f")]
public partial interface IComponentType2
{
    [PreserveSig]
    public int GetTargetCompileResult(int targetIndex, out ICompileResult compileResult, out ISlangBlob diagnostics);

    [PreserveSig]
    public int GetEntryPointCompileResult(int entryPointIndex, int targetIndex, out ICompileResult compileResult, out ISlangBlob diagnostics);
}
