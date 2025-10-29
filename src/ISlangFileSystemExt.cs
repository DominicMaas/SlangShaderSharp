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
public unsafe partial interface ISlangFileSystemExt : ISlangFileSystem
{
    /// <summary>
    ///    Get a uniqueIdentity which uniquely identifies an object of the file system.
    ///
    ///    Given a path, returns a 'uniqueIdentity' which ideally is the same value for the same object
    ///    on the file system.
    ///
    ///    The uniqueIdentity is used to compare if two paths are the same - which amongst other things
    ///    allows Slang to cache source contents internally.It is also used for #pragma once
    ///    functionality.
    ///
    ///    A* requirement* is for any implementation is that two paths can only return the same
    ///    uniqueIdentity if the contents of the two files are* identical*. If an implementation breaks
    ///    this constraint it can produce incorrect compilation.If an implementation cannot* strictly*
    ///    identify* the same* files, this will only have an effect on #pragma once behavior.
    ///
    ///    The string for the uniqueIdentity is held zero terminated in the ISlangBlob of
    ///    outUniqueIdentity.
    ///
    ///    Note that there are many ways a uniqueIdentity may be generated for a file. For example it
    ///    could be the 'canonical path' - assuming it is available and unambiguous for a file system.
    ///    Another possible mechanism could be to store the filename combined with the file date time
    ///    to uniquely identify it.
    ///
    ///    The client must ensure the blob be released when no longer used, otherwise memory will leak.
    ///
    ///    NOTE! Ideally this method would be called 'getPathUniqueIdentity' but for historical reasons
    ///    and backward compatibility it's name remains with 'File' even though an implementation
    ///    should be made to work with directories too.
    /// </summary>
    /// <returns>A `SlangResult` to indicate success or failure getting the uniqueIdentity.</returns>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult GetFileUniqueIdentity(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string path,
        out ISlangBlob uniqueIdentity);

    /// <summary>
    ///     Calculate a path combining the 'fromPath' with 'path'
    ///
    ///     The client must ensure the blob be released when no longer used, otherwise memory will leak.
    /// </summary>
    /// <param name="fromPathType">How to interpret the from path - as a file or a directory.</param>
    /// <param name="fromPath">The from path.</param>
    /// <param name="path">Path to be determined relative to the fromPath</param>
    /// <param name="pathOut">Holds the string which is the relative path. The string is held in the blob zero terminated.</param>
    /// <returns>A `SlangResult` to indicate success or failure in loading the file.</returns>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult CalcCombinedPath(
        SlangPathType fromPathType,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string fromPath,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string path,
        out ISlangBlob pathOut);

    /// <summary>
    ///     Gets the type of path that path is on the file system.
    /// </summary>
    /// <returns>SLANG_OK if located and type is known, else an error. SLANG_E_NOT_FOUND if not found.</returns>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult GetPathType(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string path,
        out SlangPathType pathType);

    /// <summary>
    ///     Get a path based on the kind.
    /// </summary>
    /// <param name="kind">The kind of path wanted</param>
    /// <param name="path">The input path</param>
    /// <param name="pathOut">The output path held in a blob</param>
    /// <returns>SLANG_OK if successfully simplified the path (SLANG_E_NOT_IMPLEMENTED if not implemented, or some other error code)</returns>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult GetPath(
        PathKind kind,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string path,
        out ISlangBlob pathOut);

    /// <summary>
    ///     Clears any cached information
    /// </summary>
    [PreserveSig]
    void ClearCache();

    /// <summary>
    ///     Enumerate the contents of the path
    ///
    ///     Note that for normal Slang operation it isn't necessary to enumerate contents this can
    ///     return SLANG_E_NOT_IMPLEMENTED.
    /// </summary>
    /// <param name="path">The path to enumerate</param>
    /// <param name="callback">This callback is called for each entry in the path.</param>
    /// <param name="userData">This is passed to the callback</param>
    /// <returns>SLANG_OK if successful</returns>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult EnumeratePathContents(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string path,
        delegate* unmanaged[Stdcall]<SlangPathType, char*, void*, void> callback,
        void* userData);

    /// <summary>
    ///     Returns how paths map to the OS file system
    /// </summary>
    /// <returns>OSPathKind that describes how paths map to the Operating System file system</returns>
    [PreserveSig]
    OSPathKind GetOSPathKind();
}
