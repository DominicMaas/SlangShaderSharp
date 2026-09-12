using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     Coverage tracing metadata produced when any shader coverage mode is active.
///
///     The current implementation reports line, function-entry, and branch-arm
///     hit-count coverage. Each emitted source entry carries the runtime counter
///     slot that backs it. The interface lets hosts read that mapping at compile
///     time so they can attribute counter values back to source locations at
///     runtime without a separate sidecar file. The metadata is retrieved by
///     casting on the artifact-associated <see cref="IMetadata"/> object.
/// </summary>
[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("7c9f1d50-1e4a-4b9c-8e21-3f7b82a3d951")]
public unsafe partial interface ICoverageTracingMetadata : ISlangCastable
{
    /// <summary>
    ///     Number of runtime counter slots in the synthesized coverage
    ///     buffer. Never larger than <see cref="ICoverageTracingMetadata.GetEntryCount"/>, and
    ///     smaller when line coverage shares one counter across the source entries of a
    ///     straight-line region. Function and branch entries always take a dedicated counter,
    ///     so the two counts are equal for a compile that enables only those modes, or for
    ///     line coverage where every marker lands in its own basic block. Size the counter
    ///     readback buffer from this value, never from the entry count.
    /// </summary>
    [PreserveSig]
    uint GetCounterCount();

    /// <summary>
    ///      Populate <paramref name="info"/> with attribution info for source coverage
    ///      entry <paramref name="index"/>. The valid range is
    ///      `[0, <see cref="GetEntryCount"/>)`.
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

    /// <summary>
    ///     Number of source coverage entries available through
    ///     <see cref="ICoverageTracingMetadata.GetEntryInfo"/>. Every coverage marker produces
    ///     one entry, so this is NOT the number of runtime counters: several entries may name
    ///     the same <see cref="CoverageEntryInfo.CounterIndex"/>. Use
    ///     <see cref="ICoverageTracingMetadata.GetCounterCount"/> to size the readback buffer.
    /// </summary>
    [PreserveSig]
    uint GetEntryCount();
}
