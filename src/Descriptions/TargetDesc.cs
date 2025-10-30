
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     Description of a code generation target.
/// </summary>
[NativeMarshalling(typeof(TargetDescMarshaller))]
public struct TargetDesc
{
    /// <summary>
    ///     The target format to generate code for (e.g., SPIR-V, DXIL, etc.)
    /// </summary>
    public SlangCompileTarget Format = SlangCompileTarget.TargetUnknown;

    /// <summary>
    ///     The compilation profile supported by the target (e.g., "Shader Model 5.1")
    /// </summary>
    public SlangProfileID Profile = SlangProfileID.Unknown;

    /// <summary>
    ///     Flags for the code generation target. Currently unused.
    /// </summary>
    public SlangTargetFlags Flags = SlangTargetFlags.GenerateSpirvDirectly;

    /// <summary>
    ///     Default mode to use for floating-point operations on the target.
    /// </summary>
    public SlangFloatingPointMode FloatingPointMode = SlangFloatingPointMode.Default;

    /// <summary>
    ///     The line directive mode for output source code.
    /// </summary>
    public SlangLineDirectiveMode LineDirectiveMode = SlangLineDirectiveMode.Default;

    /// <summary>
    ///     Whether to force `scalar` layout for glsl shader storage buffers.
    /// </summary>
    public bool ForceGLSLScalarBufferLayout = false;

    /// <summary>
    ///     Pointer to an array of compiler option entries
    /// </summary>
    public CompilerOptionEntry[]? CompilerOptionEntries = null;

    public TargetDesc() { }
}

internal unsafe struct TargetDescUnmanaged
{
    public nuint structureSize;
    public SlangCompileTarget format;
    public SlangProfileID profile;
    public SlangTargetFlags flags;
    public SlangFloatingPointMode floatingPointMode;
    public SlangLineDirectiveMode lineDirectiveMode;
    public bool forceGLSLScalarBufferLayout;
    public CompilerOptionEntryUnmanaged* compilerOptionEntries;
    public uint compilerOptionEntryCount;
}

[CustomMarshaller(typeof(TargetDesc), MarshalMode.Default, typeof(TargetDescMarshaller))]
internal static unsafe class TargetDescMarshaller
{
    public static TargetDescUnmanaged ConvertToUnmanaged(TargetDesc managed)
    {
        return new TargetDescUnmanaged
        {
            structureSize = (nuint)sizeof(TargetDescUnmanaged),
            format = managed.Format,
            profile = managed.Profile,
            flags = managed.Flags,
            floatingPointMode = managed.FloatingPointMode,
            lineDirectiveMode = managed.LineDirectiveMode,
            forceGLSLScalarBufferLayout = managed.ForceGLSLScalarBufferLayout,
            compilerOptionEntries = ArrayMarshaller<CompilerOptionEntry, CompilerOptionEntryUnmanaged>.AllocateContainerForUnmanagedElements(managed.CompilerOptionEntries, out var count),
            compilerOptionEntryCount = (uint)count,
        };
    }

    public static TargetDescUnmanaged* ConvertToUnmanagedArray(TargetDesc[]? managed, out int count)
    {
        var container = ArrayMarshaller<TargetDesc, TargetDescUnmanaged>.AllocateContainerForUnmanagedElements(managed, out count);

        if (managed != null)
        {
            for (var i = 0; i < count; i++)
            {
                container[i] = ConvertToUnmanaged(managed[i]);
            }
        }

        return container;
    }

    public static TargetDesc ConvertToManaged(TargetDescUnmanaged unmanaged)
    {
        return new TargetDesc
        {
            Format = unmanaged.format,
            Profile = unmanaged.profile,
            Flags = unmanaged.flags,
            FloatingPointMode = unmanaged.floatingPointMode,
            LineDirectiveMode = unmanaged.lineDirectiveMode,
            ForceGLSLScalarBufferLayout = unmanaged.forceGLSLScalarBufferLayout,
            CompilerOptionEntries = ArrayMarshaller<CompilerOptionEntry, CompilerOptionEntryUnmanaged>.AllocateContainerForManagedElements(unmanaged.compilerOptionEntries, (int)unmanaged.compilerOptionEntryCount),
        };
    }

    public static TargetDesc[]? ConvertToManagedArray(TargetDescUnmanaged* unmanaged, int count)
    {
        var container = ArrayMarshaller<TargetDesc, TargetDescUnmanaged>.AllocateContainerForManagedElements(unmanaged, count);
        if (container == null)
        {
            return null;
        }

        for (var i = 0; i < count; i++)
        {
            container[i] = ConvertToManaged(unmanaged[i]);
        }

        return container;
    }

    public static void Free(TargetDescUnmanaged unmanaged)
    {
        ArrayMarshaller<CompilerOptionEntry, CompilerOptionEntryUnmanaged>.Free(unmanaged.compilerOptionEntries);
    }

    public static void FreeArray(TargetDescUnmanaged* unmanaged, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Free(unmanaged[i]);
        }

        ArrayMarshaller<TargetDesc, TargetDescUnmanaged>.Free(unmanaged);
    }
}
