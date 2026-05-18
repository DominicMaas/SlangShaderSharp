using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp.Structs;

[NativeMarshalling(typeof(SyntheticResourceInfoMarshaller))]
public struct SyntheticResourceInfo
{
    /// <summary>
    ///     Stable, opaque, non-zero synthetic resource identifier within
    ///     the compiled program. `0` is reserved as the default
    ///     "unassigned" sentinel; `findResourceIndexByID(0, ...)` always
    ///     returns `SLANG_E_NOT_FOUND`. Non-zero ids are assigned from the
    ///     compiler's internal synthetic-resource registry; hosts should
    ///     treat the numeric value as opaque and use it only to correlate
    ///     metadata and runtime binding helpers.
    /// </summary>
    public uint Id = 0;

    /// <summary>
    ///     The Slang binding kind represented by this synthetic
    ///     resource.
    /// </summary>
    public SlangBindingType BindingType = SlangBindingType.Unknown;

    /// <summary>
    ///     Number of logical resources in the synthetic binding. Most
    ///     current instrumentation resources are scalar (`1`).
    /// </summary>
    public uint ArraySize = 1;

    /// <summary>
    ///      Whether the resource is global/root-scoped or attached to a
    ///      specific entry point. Coverage currently reports a global
    ///      resource; entry-point scoped resources are reserved for future
    ///      synthetic-resource producers.
    /// </summary>
    public SyntheticResourceScope Scope = SyntheticResourceScope.Global;

    /// <summary>
    ///     Intended access pattern for the resource.
    /// </summary>
    public SyntheticResourceAccess Access = SyntheticResourceAccess.Read;

    /// <summary>
    ///     Entry point index when `scope == EntryPoint`, else `-1`.
    ///      No current coverage resource uses entry-point scope.
    /// </summary>
    public int EntryPointIndex = -1;

    /// <summary>
    ///     Descriptor-facing location for backends that bind synthetic resources
    ///
    ///     Sentinel conventions:
    ///     - `space == -1` means the target does not report a descriptor
    ///       space for this resource
    ///     - `0` is a valid value
    /// </summary>
    public int Space = -1;

    /// <summary>
    ///     Descriptor-facing location for backends that bind synthetic resources
    ///
    ///     Sentinel conventions:
    ///     - `binding == -1` means descriptor binding is unavailable for
    ///     this target
    ///     - `0` is a valid value
    /// </summary>
    public int Binding = -1;

    /// <summary>
    ///     CPU/CUDA-style marshaling location in generated uniform /
    ///     wrapper parameter data, in bytes.
    /// </summary>
    public int UniformOffset = -1;

    /// <summary>
    ///     Byte stride between adjacent logical elements when
    ///     CPU/CUDA-style marshaling is reported.
    /// </summary>
    public int UniformStride = 0;

    /// <summary>
    ///     Optional stable debug name for the synthetic resource.
    /// </summary>
    public string? DebugName = null;

    public SyntheticResourceInfo()
    { }
}

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SyntheticResourceInfoUnmanaged
{
    public nuint structSize;
    public uint id;
    public SlangBindingType bindingType;
    public uint arraySize;
    public SyntheticResourceScope scope;
    public SyntheticResourceAccess access;
    public int entryPointIndex;
    public int space;
    public int binding;
    public int uniformOffset;
    public int uniformStride;
    public byte* debugName;
}

[CustomMarshaller(typeof(SyntheticResourceInfo), MarshalMode.Default, typeof(SyntheticResourceInfoMarshaller))]
internal static unsafe class SyntheticResourceInfoMarshaller
{
    public static SyntheticResourceInfoUnmanaged ConvertToUnmanaged(SyntheticResourceInfo managed)
    {
        return new SyntheticResourceInfoUnmanaged
        {
            // Use the leading `structSize` for ABI-versioned struct growth
            structSize = (nuint)sizeof(SyntheticResourceInfoUnmanaged),
            id = managed.Id,
            bindingType = managed.BindingType,
            arraySize = managed.ArraySize,
            scope = managed.Scope,
            access = managed.Access,
            entryPointIndex = managed.EntryPointIndex,
            space = managed.Space,
            binding = managed.Binding,
            uniformOffset = managed.UniformOffset,
            uniformStride = managed.UniformStride,
            debugName = Utf8StringMarshaller.ConvertToUnmanaged(managed.DebugName),
        };
    }

    public static SyntheticResourceInfo ConvertToManaged(SyntheticResourceInfoUnmanaged unmanaged)
    {
        return new SyntheticResourceInfo
        {
            Id = unmanaged.id,
            BindingType = unmanaged.bindingType,
            ArraySize = unmanaged.arraySize,
            Scope = unmanaged.scope,
            Access = unmanaged.access,
            EntryPointIndex = unmanaged.entryPointIndex,
            Space = unmanaged.space,
            Binding = unmanaged.binding,
            UniformOffset = unmanaged.uniformOffset,
            UniformStride = unmanaged.uniformStride,
            DebugName = Utf8StringMarshaller.ConvertToManaged(unmanaged.debugName),
        };
    }
}