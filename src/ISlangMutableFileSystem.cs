using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("a058675c-1d65-452a-8458-ccded1427105")]
public unsafe partial interface ISlangMutableFileSystem : ISlangFileSystemExt
{
    /// <summary>
    ///     Write data to the specified path.
    /// </summary>
    /// <param name="path">The path for data to be saved to</param>
    /// <param name="data">The data to be saved</param>
    /// <param name="size">The size of the data in bytes</param>
    /// <returns>SLANG_OK if successful (SLANG_E_NOT_IMPLEMENTED if not implemented, or some other error code)</returns>
    [PreserveSig]
    SlangResult SaveFile(
        string path,
        void* data,
        nuint size);

    /// <summary>
    ///     Write data in the form of a blob to the specified path.
    ///
    ///      Depending on the implementation writing a blob might be faster/use less memory. It is
    ///      assumed the blob is *immutable* and that an implementation can reference count it.
    ///
    ///     It is not guaranteed loading the same file will return the *same* blob - just a blob with
    ///     same contents.
    /// </summary>
    /// <param name="path">The path for data to be saved to</param>
    /// <param name="dataBlob">The data to be saved</param>
    /// <returns>SLANG_OK if successful (SLANG_E_NOT_IMPLEMENTED if not implemented, or some other error code)</returns>
    [PreserveSig]
    SlangResult SaveFileBlob(
        string path,
        nint dataBlob);

    /// <summary>
    ///     Remove the entry in the path (directory of file). Will only delete an empty directory,
    ///     if not empty will return an error.
    /// </summary>
    /// <param name="path">The path to remove</param>
    /// <returns>SLANG_OK if successful</returns>
    [PreserveSig]
    SlangResult Remove(string path);

    /// <summary>
    ///     Create a directory.
    ///
    ///     The path to the directory must exist
    /// </summary>
    /// <param name="path">To the directory to create. The parent path *must* exist otherwise will return an error.</param>
    /// <returns>SLANG_OK if successful</returns>
    [PreserveSig]
    SlangResult CreateDirectory(string path);
}
