using SlangShaderSharp.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(TypeReflectionMarshaller))]
public readonly partial struct TypeReflection : IEquatable<TypeReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use TypeReflection.Null instead.")]
    public TypeReflection()
    {
        Handle = 0;
    }

    internal TypeReflection(nint value)
    {
        Handle = value;
    }

    public static TypeReflection Null => new(0);

    public static bool operator ==(TypeReflection left, TypeReflection right) => left.Handle == right.Handle;
    public static bool operator !=(TypeReflection left, TypeReflection right) => !(left == right);

    public bool Equals(TypeReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is TypeReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // ---------------- Methods ---------------- //

    public uint AttributeCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionType_GetUserAttributeCount(this);
        }
    }

    public AttributeReflection GetAttribute(uint index)
    {
        if (this == Null) return AttributeReflection.Null;
        return spReflectionType_GetUserAttribute(this, index);
    }

    public AttributeReflection FindAttributeByName(string name)
    {
        if (this == Null) return AttributeReflection.Null;
        return spReflectionType_FindUserAttributeByName(this, name);
    }

    // ---------------- Native Imports ---------------- //

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionType_GetUserAttributeCount(TypeReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial AttributeReflection spReflectionType_GetUserAttribute(TypeReflection type, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial AttributeReflection spReflectionType_FindUserAttributeByName(TypeReflection type, string name);
}

[CustomMarshaller(typeof(TypeReflection), MarshalMode.Default, typeof(TypeReflectionMarshaller))]
internal static class TypeReflectionMarshaller
{
    public static nint ConvertToUnmanaged(TypeReflection managed) => managed.Handle;
    public static TypeReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}