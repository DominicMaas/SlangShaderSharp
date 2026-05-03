using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     Cooperative matrix and vector metadata.
///
///     This interface reports the cooperative matrix/vector type information that a compiled target uses,
///     including cooperative matrix types, cooperative vector type-usage records, and certain type
///     combinations required to execute some operations (like matrix multiplication).
///
///     Applications can use this metadata to compare shader requirements against the capabilities exposed
///     by the target API/driver (for example Vulkan cooperative matrix/vector property queries, or
///     analogous APIs on other backends).
///
///     Metadata is collected from the IR after target-specific lowering, so it only reflects cooperative
///     types that survive as native constructs in the final output. Targets that lower cooperative types
///     into ordinary arrays will report empty lists.
///
///     Lists are exposed using `get*Count()` plus `get*ByIndex()` methods, where the count returns the
///     number of elements currently available and valid indices are in the range `[0, count)`.
///
///     Cast from an `IMetadata*` using `castAs()`.
/// </summary>
[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("64c4d536-d949-49c3-9fde-3f0f9c6f0131")]
public unsafe partial interface ICooperativeTypesMetadata : ISlangCastable
{
    [PreserveSig]
    nuint GetCooperativeMatrixTypeCount();

    [PreserveSig]
    SlangResult GetCooperativeMatrixTypeByIndex(
        nuint index,
        out CooperativeMatrixType type);

    [PreserveSig]
    nuint GetCooperativeMatrixCombinationCount();

    [PreserveSig]
    SlangResult GetCooperativeMatrixCombinationByIndex(
        nuint index,
        out CooperativeMatrixCombination combination);

    [PreserveSig]
    nuint GetCooperativeVectorTypeCount();

    [PreserveSig]
    SlangResult GetCooperativeVectorTypeByIndex(
        nuint index,
        out CooperativeVectorTypeUsageInfo type);

    [PreserveSig]
    nuint GetCooperativeVectorCombinationCount();

    [PreserveSig]
    SlangResult GetCooperativeVectorCombinationByIndex(
        nuint index,
        out CooperativeVectorCombination combination);
}
