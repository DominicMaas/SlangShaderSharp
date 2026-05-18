using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     Coverage tracing metadata produced when `-trace-coverage` is active.
///
///     The current implementation reports line-oriented hit-count coverage:
///     each counter slot in the synthesized coverage buffer maps to a source
///     `(file, line)` pair. The interface lets hosts read that mapping at
///     compile time so they can attribute counter values back to source lines
///     at runtime without a separate sidecar file. The metadata is retrieved
///     by casting on the artifact-associated <see cref="IMetadata"/> object.
/// </summary>
[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("7c9f1d50-1e4a-4b9c-8e21-3f7b82a3d951")]
public unsafe partial interface ICoverageTracingMetadata : ISlangCastable
{
    /// <summary>
    ///     Number of counter slots in the synthesized coverage buffer.
    ///     In the current implementation this is the number of line-
    ///     oriented source-location counter slots. Generic specializations
    ///     and other cloned instances of the same source line aggregate
    ///     into that source line's slot. Future revisions may extend the
    ///     entry model without changing the interface shape.
    /// </summary>
    [PreserveSig]
    uint GetCounterCount();

    /// <summary>
    ///      Populate <paramref name="info"/> with attribution info for counter slot <paramref name="index"/>.
    /// </summary>
    [PreserveSig]
    SlangResult GetEntryInfo(
        uint index,
        ref CoverageEntryInfo info);

    /// <summary>
    ///      Populate <paramref name="info"/> with the coverage buffer's binding info.
    /// </summary>
    [PreserveSig]
    SlangResult GetBufferInfo(ref CoverageBufferInfo info);
}
