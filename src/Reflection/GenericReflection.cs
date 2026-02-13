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
    private static partial SlangDeclKind spReflectionGeneric_GetInnerKind(GenericReflection generic);

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
