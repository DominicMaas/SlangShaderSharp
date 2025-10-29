using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     An extended file system abstraction.
///
///      Implementing and using this interface over ISlangFileSystem gives much more control over how
///      paths are managed, as well as how it is determined if two files 'are the same'.
///
///     All paths as input char*, or output as ISlangBlobs are always encoded as UTF-8 strings.
///     Blobs that contain strings are always zero terminated.
/// </summary>
[GeneratedComInterface]
[Guid("5fb632d2-979d-4481-9fee-663c3f1449e1")]
public partial interface ISlangFileSystemExt : ISlangFileSystem
{
}
