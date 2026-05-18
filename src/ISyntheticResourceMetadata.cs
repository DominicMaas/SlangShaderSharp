using SlangShaderSharp.Structs;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("47a33723-181b-4d2b-b89e-215495bb388b")]
public unsafe partial interface ISyntheticResourceMetadata : ISlangCastable
{
    /// <summary>
    ///     Number of synthetic bindable resources reported by this
    ///     metadata object.
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    uint GetResourceCount();

    /// <summary>
    ///     Populate <paramref name="info"/> with the metadata for synthetic resource <paramref name="index"/>.
    /// </summary>
    /// <returns><see cref="SlangResult.SLANG_OK"/> on success, <see cref="SlangResult.SLANG_E_INVALID_ARG"/> for null <paramref name="info"/>, mismatched `structSize`, or out-of-range <paramref name="index"/>.</returns>
    [PreserveSig]
    SlangResult GetResourceInfo(
        uint index,
        ref SyntheticResourceInfo info);

    /// <summary>
    ///     Look up the resource index for a stable synthetic resource identifier.
    /// </summary>
    /// <returns>
    ///     <see cref="SlangResult.SLANG_OK"/> on success, <see cref="SlangResult.SLANG_E_NOT_FOUND"/> when no resource with that id exists, and <see cref="SlangResult.SLANG_E_INVALID_ARG"/> for null <paramref name="index"/>.
    /// </returns>
    [PreserveSig]
    SlangResult FindResourceIndexByID(
        uint id,
        out uint index);
}
