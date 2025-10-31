using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     A (real or virtual) file system.
///
///     Slang can make use of this interface whenever it would otherwise try to load files
///     from disk, allowing applications to hook and/or override filesystem access from
///     the compiler.
///
///     It is the responsibility of
///     the caller of any method that returns a ISlangBlob to release the blob when it is no
///     longer used (using 'release').
/// </summary>
[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("003a09fc-3a4d-4ba0-ad60-1fd863a915ab")]
public partial interface ISlangFileSystem
{
    /// <summary>
    ///     Load a file from `path` and return a blob of its contents
    ///
    ///     NOTE! This is a *binary* load - the blob should contain the exact same bytes
    ///     as are found in the backing file.
    ///
    ///     If load is successful, the implementation should create a blob to hold
    ///     the file's content, store it to `outBlob`, and return 0.
    ///     If the load fails, the implementation should return a failure status
    ///     (any negative value will do).
    /// </summary>
    /// <param name="path">The path to load from, as a null-terminated UTF-8 string.</param>
    /// <param name="outBlob">A destination pointer to receive the blob of the file contents.</param>
    /// <returns>A `SlangResult` to indicate success or failure in loading the file.</returns>
    [PreserveSig]
    SlangResult LoadFile(
        string path,
        out ISlangBlob outBlob);
}
