using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     Bindless resource metadata produced for a compiled target.
///     The bindless space index reported through program reflection is a frontend-predicted reserved
///     descriptor space.It remains stable even when later optimization or target lowering removes all
///     descriptor-handle heap use from the emitted shader.This metadata interface reports the
///     post-lowering usage signal instead.
///
///     `usesBindlessResourceHeap()` reports whether the final target IR still contains the
///     descriptor-handle/bindless resource path after target-specific lowering. This is a code-generation
///     signal, not a complete cross-target host binding policy: targets that lower descriptor handles to
///     native resource handles or addresses may not require an explicit descriptor-heap binding even when
///     this returns true. Hosts should combine this query with their target binding model when deciding
///     whether to bind a heap.
///
///     Cast from an artifact-associated `IMetadata*` using `castAs()`.
/// </summary>
[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("eafa96d3-2352-4bf4-8864-3228a4077a83")]
public unsafe partial interface IBindlessResourceMetadata : ISlangCastable
{
    /// <summary>
    ///     Returns true when the compiled target IR still contains a bindless
    ///     descriptor-heap/resource-handle path after target-specific lowering. This is a
    ///     code-generation signal, not a complete cross-target host binding policy; targets
    ///     that lower descriptor handles to native resource handles or addresses may not require
    ///     an explicit descriptor-heap binding even when this returns true.
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    [return: MarshalAs(UnmanagedType.I1)]
    bool UsesBindlessResourceHeap();
}
