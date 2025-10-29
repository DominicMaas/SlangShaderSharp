using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[GeneratedComInterface]
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
    public int SaveFile([MarshalAs(UnmanagedType.LPUTF8Str)] string path, void* data, nuint size);

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
    public int SaveFileBlob([MarshalAs(UnmanagedType.LPUTF8Str)] string path, nint dataBlob);
}
