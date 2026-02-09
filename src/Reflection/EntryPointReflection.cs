using SlangShaderSharp.Internal;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(EntryPointReflectionMarshaller))]
public readonly partial struct EntryPointReflection : IEquatable<EntryPointReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use EntryPointReflection.Null instead.")]
    public EntryPointReflection()
    {
        Handle = 0;
    }

    internal EntryPointReflection(nint value)
    {
        Handle = value;
    }

    public static EntryPointReflection Null => new(0);

    public static bool operator ==(EntryPointReflection left, EntryPointReflection right) => left.Handle == right.Handle;
    public static bool operator !=(EntryPointReflection left, EntryPointReflection right) => !(left == right);

    public bool Equals(EntryPointReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is EntryPointReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // ---------------- Methods ---------------- //

    public string Name
    {
        get
        {
            if (this == Null) return string.Empty;
            return spReflectionEntryPoint_getName(this);
        }
    }

    public string NameOverride
    {
        get
        {
            if (this == Null) return string.Empty;
            return spReflectionEntryPoint_getNameOverride(this);
        }
    }

    public uint ParameterCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionEntryPoint_getParameterCount(this);
        }
    }

    public FunctionReflection Function
    {
        get
        {
            if (this == Null) return FunctionReflection.Null;
            return spReflectionEntryPoint_getFunction(this);
        }
    }

    public SlangStage Stage
    {
        get
        {
            if (this == Null) return SlangStage.None;
            return spReflectionEntryPoint_getStage(this);
        }
    }

    public VariableLayoutReflection VarLayout
    {
        get
        {
            if (this == Null) return VariableLayoutReflection.Null;
            return spReflectionEntryPoint_getVarLayout(this);
        }
    }

    public VariableLayoutReflection ResultVarLayout
    {
        get
        {
            if (this == Null) return VariableLayoutReflection.Null;
            return spReflectionEntryPoint_getResultVarLayout(this);
        }
    }

    //  TODO

    // ---------------- Native Imports ---------------- //

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static partial string spReflectionEntryPoint_getName(EntryPointReflection entryPoint);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static partial string spReflectionEntryPoint_getNameOverride(EntryPointReflection entryPoint);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionEntryPoint_getParameterCount(EntryPointReflection entryPoint);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial FunctionReflection spReflectionEntryPoint_getFunction(EntryPointReflection entryPoint);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableLayoutReflection spReflectionEntryPoint_getParameterByIndex(EntryPointReflection entryPoint, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangStage spReflectionEntryPoint_getStage(EntryPointReflection entryPoint);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial void spReflectionEntryPoint_getComputeThreadGroupSize(EntryPointReflection entryPoint, uint axisCount, out uint sizeAlongAxis);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial void spReflectionEntryPoint_getComputeWaveSize(EntryPointReflection entryPoint, out uint waveSize);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial int spReflectionEntryPoint_usesAnySampleRateInput(EntryPointReflection entryPoint);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableLayoutReflection spReflectionEntryPoint_getVarLayout(EntryPointReflection entryPoint);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableLayoutReflection spReflectionEntryPoint_getResultVarLayout(EntryPointReflection entryPoint);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial int spReflectionEntryPoint_hasDefaultConstantBuffer(EntryPointReflection entryPoint);
}

[CustomMarshaller(typeof(EntryPointReflection), MarshalMode.Default, typeof(EntryPointReflectionMarshaller))]
internal static class EntryPointReflectionMarshaller
{
    public static nint ConvertToUnmanaged(EntryPointReflection managed) => managed.Handle;
    public static EntryPointReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}