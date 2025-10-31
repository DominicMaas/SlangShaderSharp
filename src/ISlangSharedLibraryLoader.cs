using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("6264ab2b-a3e8-4a06-97f1-49bc2d2ab14d")]
public partial interface ISlangSharedLibraryLoader
{
    /// <summary>
    ///     Load a shared library. In typical usage the library name should *not* contain any
    ///     platform specific elements. For example on windows a dll name should *not* be passed with a
    ///     '.dll' extension, and similarly on linux a shared library should *not* be passed with the
    ///     'lib' prefix and '.so' extension
    /// </summary>
    /// <param name="path">The unadorned filename and/or path for the shared library</param>
    /// <param name="sharedLibrary">Holds the shared library if successfully loaded</param>
    /// <returns></returns>
    [PreserveSig]
    SlangResult LoadSharedLibrary(
        string path,
        out ISlangSharedLibrary sharedLibrary);
}
