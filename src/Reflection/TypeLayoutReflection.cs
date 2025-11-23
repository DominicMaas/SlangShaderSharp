using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(TypeLayoutReflectionMarshaller))]
public readonly partial struct TypeLayoutReflection : IEquatable<TypeLayoutReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use TypeLayoutReflection.Null instead.")]
    public TypeLayoutReflection()
    {
        Handle = 0;
    }

    internal TypeLayoutReflection(nint value)
    {
        Handle = value;
    }

    public static TypeLayoutReflection Null => new(0);

    public static bool operator ==(TypeLayoutReflection left, TypeLayoutReflection right) => left.Handle == right.Handle;
    public static bool operator !=(TypeLayoutReflection left, TypeLayoutReflection right) => !(left == right);

    public bool Equals(TypeLayoutReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is TypeLayoutReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // ---------------- Methods ---------------- //

    public TypeReflection Type
    {
        get
        {
            if (this == Null) return TypeReflection.Null;
            return spReflectionTypeLayout_GetType(this);
        }
    }

    public TypeReflectionKind Kind
    {
        get
        {
            if (this == Null) return TypeReflectionKind.None;
            return spReflectionTypeLayout_getKind(this);
        }
    }

    // ---------------- Native Imports ----------------

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflection spReflectionTypeLayout_GetType(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflectionKind spReflectionTypeLayout_getKind(TypeLayoutReflection type);
}

[CustomMarshaller(typeof(TypeLayoutReflection), MarshalMode.Default, typeof(TypeLayoutReflectionMarshaller))]
internal static class TypeLayoutReflectionMarshaller
{
    public static nint ConvertToUnmanaged(TypeLayoutReflection managed) => managed.Handle;
    public static TypeLayoutReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
