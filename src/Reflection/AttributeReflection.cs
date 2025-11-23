using SlangShaderSharp.Internal;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp.Reflection;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(AttributeReflectionMarshaller))]
public readonly partial struct AttributeReflection : IEquatable<AttributeReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use AttributeReflection.Null instead.")]
    public AttributeReflection()
    {
        Handle = 0;
    }

    internal AttributeReflection(nint value)
    {
        Handle = value;
    }

    public static AttributeReflection Null => new(0);

    public static bool operator ==(AttributeReflection left, AttributeReflection right) => left.Handle == right.Handle;
    public static bool operator !=(AttributeReflection left, AttributeReflection right) => !(left == right);

    public bool Equals(AttributeReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is AttributeReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // ---------------- Methods ---------------- //

    public string? Name => this == Null ? null : spReflectionUserAttribute_GetName(this);

    public uint ArgumentCount => this == Null ? 0 : spReflectionUserAttribute_GetArgumentCount(this);

    public TypeReflection GetArgumentType(uint index)
    {
        if (this == Null)
            return TypeReflection.Null;

        return spReflectionUserAttribute_GetArgumentType(this, index);
    }

    public int? GetArgumentValueInt(uint index)
    {
        if (this == Null) return null;

        if (!spReflectionUserAttribute_GetArgumentValueInt(this, index, out var value).Succeeded)
            return null;

        return value;
    }

    public float? GetArgumentValueFloat(uint index)
    {
        if (this == Null) return null;

        if (!spReflectionUserAttribute_GetArgumentValueFloat(this, index, out var value).Succeeded)
            return null;

        return value;
    }

    // ---------------- Native Imports ---------------- //

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static partial string spReflectionUserAttribute_GetName(AttributeReflection attrib);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionUserAttribute_GetArgumentCount(AttributeReflection attrib);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflection spReflectionUserAttribute_GetArgumentType(AttributeReflection attrib, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangResult spReflectionUserAttribute_GetArgumentValueInt(AttributeReflection attrib, uint index, out int value);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangResult spReflectionUserAttribute_GetArgumentValueFloat(AttributeReflection attrib, uint index, out float value);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionUserAttribute_GetArgumentValueString(AttributeReflection attrib, uint index, out nuint size);
}

[CustomMarshaller(typeof(AttributeReflection), MarshalMode.Default, typeof(AttributeReflectionMarshaller))]
internal static class AttributeReflectionMarshaller
{
    public static nint ConvertToUnmanaged(AttributeReflection managed) => managed.Handle;
    public static AttributeReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
