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
[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("9c2a4b3d-7f68-4e91-a52c-8b193e457a9f")]
public partial interface IComponentType2
{
    [PreserveSig]
    SlangResult GetTargetCompileResult(
        nint targetIndex,
        out ICompileResult compileResult,
        out ISlangBlob? diagnostics);

    [PreserveSig]
    SlangResult GetEntryPointCompileResult(
        nint entryPointIndex,
        nint targetIndex,
        out ICompileResult compileResult,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///     Get functions accessible through the ISlangSharedLibrary interface.
    ///     The functions remain in scope as long as the ISlangSharedLibrary interface is in scope.
    /// </summary>
    /// <remarks>
    ///     NOTE! Requires a compilation target of SLANG_HOST_CALLABLE.
    /// </remarks>
    /// <param name="targetIndex">The index of the target to get code for (default: zero)</param>
    /// <param name="sharedLibrary">A pointer to a ISharedLibrary interface which functions can be queried on.</param>
    /// <returns>A `SlangResult` to indicate success or failure.</returns>
    [PreserveSig]
    SlangResult GetTargetHostCallable(
        int targetIndex,
        out ISlangSharedLibrary sharedLibrary,
        out ISlangBlob? diagnostics);
}
