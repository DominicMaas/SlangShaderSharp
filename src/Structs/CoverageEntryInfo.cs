using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///      Per-counter-slot attribution returned by <see cref="ICoverageTracingMetadata.GetEntryInfo" />.
/// </summary>
[NativeMarshalling(typeof(CoverageEntryInfoMarshaller))]
public struct CoverageEntryInfo
{
    /// <summary>
    ///     Source file for this counter entry, or null if the entry
    ///     could not be attributed to a real source file.
    /// </summary>
    public string? File;

    /// <summary>
    ///     1-based source line for this coverage entry, or 0 if the entry
    ///     could not be attributed to a real source line. The current
    ///     implementation reports, function, and branch entries;
    ///     future revisions may add source-region entries or additional
    ///     branch forms.
    /// </summary>
    public uint Line = 0;

    /// <summary>
    ///     Counter slot used by this entry, or
    ///     <see cref="Slang.InvalidCoverageCounterIndex"/> when the entry has no runtime
    ///     counter. The current line/function/branch producers use one
    ///     direct counter per entry. Future source-region coverage may use
    ///     <see cref="Slang.InvalidCoverageCounterIndex"/> for entries whose count is
    ///     derived from other counters or represented through tail-extended
    ///     fields.
    /// </summary>
    public uint CounterIndex = Slang.InvalidCoverageCounterIndex;

    /// <summary>
    ///     Semantic kind of this source coverage entry.
    /// </summary>
    public CoverageEntryKind Kind = CoverageEntryKind.Unknown;

    /// <summary>
    ///      Runtime accumulation mode for <see cref="CounterIndex"/>. The current
    ///      implementation only defines <see cref="CoverageCounterMode.Count"/>; future concrete modes can
    ///       be appended when implemented.
    /// </summary>
    public CoverageCounterMode Mode = CoverageCounterMode.Count;

    /// <summary>
    ///     1-based inclusive start column for this entry, or 0 when
    ///     unavailable.
    /// </summary>
    public uint StartColumn = 0;

    /// <summary>
    ///     1-based end line for this entry's half-open end coordinate, or
    ///     0 when the exact range is unavailable. Future source-region
    ///     coverage can use `(line,startColumn)` to `(endLine,endColumn)`
    ///     for a half-open source range.
    /// </summary>
    public uint EndLine = 0;

    /// <summary>
    ///     1-based exclusive end column for this entry, or 0 when
    ///     unavailable.
    /// </summary>
    public uint EndColumn = 0;

    /// <summary>
    ///     Function display name for function coverage entries, or
    ///     null when not applicable or unavailable.
    /// </summary>
    public string? FunctionName = null;

    /// <summary>
    ///     Stable mangled function name for function coverage entries, or
    ///     null when not applicable or unavailable.
    /// </summary>
    public string? FunctionMangledName = null;

    /// <summary>
    ///     Stable branch-site identifier within this metadata object, or 0
    ///     when not applicable.
    /// </summary>
    public uint BranchSiteId = 0;

    /// <summary>
    ///     Stable branch-arm identifier within <see cref="BranchSiteId"/>, or 0 when
    ///     not applicable.
    /// </summary>
    public uint BranchArmId = 0;

    /// <summary>
    ///     Branch arm semantic for branch coverage entries.
    /// </summary>
    public CoverageBranchArmKind BranchArmKind = CoverageBranchArmKind.Unknown;

    public CoverageEntryInfo()
    { }
}

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct CoverageEntryInfoUnmanaged
{
    public nuint structSize;
    public byte* file;
    public uint line;
    public uint counterIndex;
    public CoverageEntryKind kind;
    public CoverageCounterMode mode;
    public uint startColumn;
    public uint endLine;
    public uint endColumn;
    public byte* functionName;
    public byte* functionMangledName;
    public uint branchSiteId;
    public uint branchArmId;
    public CoverageBranchArmKind branchArmKind;
}

[CustomMarshaller(typeof(CoverageEntryInfo), MarshalMode.Default, typeof(CoverageEntryInfoMarshaller))]
internal static unsafe class CoverageEntryInfoMarshaller
{
    public static CoverageEntryInfoUnmanaged ConvertToUnmanaged(CoverageEntryInfo managed)
    {
        return new CoverageEntryInfoUnmanaged
        {
            // Use the leading `structSize` for ABI-versioned struct growth
            structSize = (nuint)sizeof(CoverageEntryInfoUnmanaged),
            // `file` is an output pointer owned by the metadata object; only `structSize` needs initialization.
            file = null,
            line = managed.Line,
            counterIndex = managed.CounterIndex,
            kind = managed.Kind,
            mode = managed.Mode,
            startColumn = managed.StartColumn,
            endLine = managed.EndLine,
            endColumn = managed.EndColumn,
            // `functionName` is an output pointer owned by the metadata object; only `structSize` needs initialization.
            functionName = null,
            // `functionMangledName` is an output pointer owned by the metadata object; only `structSize` needs initialization.
            functionMangledName = null,
            branchSiteId = managed.BranchSiteId,
            branchArmId = managed.BranchArmId,
            branchArmKind = managed.BranchArmKind
        };
    }

    public static CoverageEntryInfo ConvertToManaged(CoverageEntryInfoUnmanaged unmanaged)
    {
        return new CoverageEntryInfo
        {
            File = Utf8StringMarshaller.ConvertToManaged(unmanaged.file),
            Line = unmanaged.line,
            CounterIndex = unmanaged.counterIndex,
            Kind = unmanaged.kind,
            Mode = unmanaged.mode,
            StartColumn = unmanaged.startColumn,
            EndLine = unmanaged.endLine,
            EndColumn = unmanaged.endColumn,
            FunctionName = Utf8StringMarshaller.ConvertToManaged(unmanaged.functionName),
            FunctionMangledName = Utf8StringMarshaller.ConvertToManaged(unmanaged.functionMangledName),
            BranchSiteId = unmanaged.branchSiteId,
            BranchArmId = unmanaged.branchArmId,
            BranchArmKind = unmanaged.branchArmKind
        };
    }

    public static void Free(CoverageEntryInfoUnmanaged unmanaged)
    {
        // `file` is a non-owning pointer returned by Slang and remains valid
        // only for the lifetime of the associated metadata. It must not be
        // freed by this marshaller.
    }
}