using SlangShaderSharp.Internal;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(TypeParameterReflectionMarshaller))]
public readonly partial struct TypeParameterReflection : IEquatable<TypeParameterReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use TypeParameterReflection.Null instead.")]
    public TypeParameterReflection()
    {
        Handle = 0;
    }

    internal TypeParameterReflection(nint value)
    {
        Handle = value;
    }

    public static TypeParameterReflection Null => new(0);

    public static bool operator ==(TypeParameterReflection left, TypeParameterReflection right) => left.Handle == right.Handle;
    public static bool operator !=(TypeParameterReflection left, TypeParameterReflection right) => !(left == right);

    public bool Equals(TypeParameterReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is TypeParameterReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // ---------------- Public Interface ---------------- //

    public string Name
    {
        get
        {
            if (this == Null) return string.Empty;
            return spReflectionTypeParameter_GetName(this);
        }
    }

    public uint Index
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionTypeParameter_GetIndex(this);
        }
    }

    public uint ConstraintCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionTypeParameter_GetConstraintCount(this);
        }
    }

    public TypeReflection GetConstraintByIndex(int index)
    {
        if (this == Null) return TypeReflection.Null;
        return spReflectionTypeParameter_GetConstraintByIndex(this, index);
    }

    // ---------------- Native Imports ---------------- //

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static partial string spReflectionTypeParameter_GetName(TypeParameterReflection typeParam);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionTypeParameter_GetIndex(TypeParameterReflection typeParam);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionTypeParameter_GetConstraintCount(TypeParameterReflection typeParam);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflection spReflectionTypeParameter_GetConstraintByIndex(TypeParameterReflection typeParam, int index);
}

[CustomMarshaller(typeof(TypeParameterReflection), MarshalMode.Default, typeof(TypeParameterReflectionMarshaller))]
internal static class TypeParameterReflectionMarshaller
{
    public static nint ConvertToUnmanaged(TypeParameterReflection managed) => managed.Handle;
    public static TypeParameterReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
