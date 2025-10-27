using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     Compile result for storing and retrieving multiple output blobs.
///     This is needed for features such as separate debug compilation which
///     output both base and debug spirv.
/// </summary>
[GeneratedComInterface]
[Guid("5fa9380e-b62f-41e5-9f12-4bad4d9eaae4")]
public unsafe partial interface ICompileResult : ISlangCastable
{
    [PreserveSig]
    public uint GetItemCount();

    [PreserveSig]
    public int GetItemData(uint index, out ISlangBlob blob);

    [PreserveSig]
    public int GetMetadata(uint index, out IMetadata metadata);
}
