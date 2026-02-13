using SlangShaderSharp.Internal;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(GenericReflectionMarshaller))]
public readonly partial struct GenericReflection : IEquatable<GenericReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use GenericReflection.Null instead.")]
    public GenericReflection()
    {
        Handle = 0;
    }

    internal GenericReflection(nint value)
    {
        Handle = value;
    }

    public static GenericReflection Null => new(0);

    public static bool operator ==(GenericReflection left, GenericReflection right) => left.Handle == right.Handle;
    public static bool operator !=(GenericReflection left, GenericReflection right) => !(left == right);

    public bool Equals(GenericReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is GenericReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // ---------------- Public Interface ---------------- //

    public DeclReflection AsDecl()
    {
        if (this == Null) return DeclReflection.Null;
        return spReflectionGeneric_asDecl(this);
    }

    public string Name
    {
        get
        {
            if (this == Null) return string.Empty;
            return spReflectionGeneric_GetName(this);
        }
    }

    public uint TypeParameterCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionGeneric_GetTypeParameterCount(this);
        }
    }

    public VariableReflection GetTypeParameter(uint index)
    {
        if (this == Null) return VariableReflection.Null;
        return spReflectionGeneric_GetTypeParameter(this, index);
    }

    public uint ValueParameterCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionGeneric_GetValueParameterCount(this);
        }
    }

    public VariableReflection GetValueParameter(uint index)
    {
        if (this == Null) return VariableReflection.Null;
        return spReflectionGeneric_GetValueParameter(this, index);
    }

    public uint GetTypeParameterConstraintCount(VariableReflection typeParam)
    {
        if (this == Null) return 0;
        return spReflectionGeneric_GetTypeParameterConstraintCount(this, typeParam);
    }

    public TypeReflection GetTypeParameterConstraintType(VariableReflection typeParam, uint index)
    {
        if (this == Null) return TypeReflection.Null;
        return spReflectionGeneric_GetTypeParameterConstraintType(this, typeParam, index);
    }

    public DeclReflection InnerDecl
    {
        get
        {
            if (this == Null) return DeclReflection.Null;
            return spReflectionGeneric_GetInnerDecl(this);
        }
    }

    public DeclReflectionKind InnerKind
    {
        get
        {
            if (this == Null) return DeclReflectionKind.Unsupported;
            return spReflectionGeneric_GetInnerKind(this);
        }
    }

    public GenericReflection OuterGenericContainer
    {
        get
        {
            if (this == Null) return GenericReflection.Null;
            return spReflectionGeneric_GetOuterGenericContainer(this);
        }
    }

    public TypeReflection GetConcreteType(VariableReflection typeParam)
    {
        if (this == Null) return TypeReflection.Null;
        return spReflectionGeneric_GetConcreteType(this, typeParam);
    }

    public long GetConcreteIntVal(VariableReflection valueParam)
    {
        if (this == Null) return 0;
        return spReflectionGeneric_GetConcreteIntVal(this, valueParam);
    }

    public GenericReflection ApplySpecializations(GenericReflection generic)
    {
        if (this == Null) return GenericReflection.Null;
        return spReflectionGeneric_applySpecializations(this, generic);
    }

    // ---------------- Native Imports ---------------- //

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial DeclReflection spReflectionGeneric_asDecl(GenericReflection generic);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static partial string spReflectionGeneric_GetName(GenericReflection generic);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionGeneric_GetTypeParameterCount(GenericReflection generic);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableReflection spReflectionGeneric_GetTypeParameter(GenericReflection generic, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionGeneric_GetValueParameterCount(GenericReflection generic);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableReflection spReflectionGeneric_GetValueParameter(GenericReflection generic, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionGeneric_GetTypeParameterConstraintCount(GenericReflection generic, VariableReflection typeParam);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflection spReflectionGeneric_GetTypeParameterConstraintType(GenericReflection generic, VariableReflection typeParam, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial DeclReflection spReflectionGeneric_GetInnerDecl(GenericReflection generic);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial DeclReflectionKind spReflectionGeneric_GetInnerKind(GenericReflection generic);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial GenericReflection spReflectionGeneric_GetOuterGenericContainer(GenericReflection generic);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflection spReflectionGeneric_GetConcreteType(GenericReflection generic, VariableReflection typeParam);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial long spReflectionGeneric_GetConcreteIntVal(GenericReflection generic, VariableReflection valueParam);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial GenericReflection spReflectionGeneric_applySpecializations(GenericReflection generic, GenericReflection specialization);
}

[CustomMarshaller(typeof(GenericReflection), MarshalMode.Default, typeof(GenericReflectionMarshaller))]
internal static class GenericReflectionMarshaller
{
    public static nint ConvertToUnmanaged(GenericReflection managed) => managed.Handle;
    public static GenericReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
