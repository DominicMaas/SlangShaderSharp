using SlangShaderSharp.Internal;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(VariableReflectionMarshaller))]
public readonly partial struct VariableReflection : IEquatable<VariableReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use VariableReflection.Null instead.")]
    public VariableReflection()
    {
        Handle = 0;
    }

    internal VariableReflection(nint value)
    {
        Handle = value;
    }

    public static VariableReflection Null => new(0);

    public static bool operator ==(VariableReflection left, VariableReflection right) => left.Handle == right.Handle;
    public static bool operator !=(VariableReflection left, VariableReflection right) => !(left == right);

    public bool Equals(VariableReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is VariableReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // ---------------- Methods ---------------- //

    public string Name
    {
        get
        {
            if (this == Null) return string.Empty;
            return spReflectionVariable_GetName(this);
        }
    }

    public TypeReflection Type
    {
        get
        {
            if (this == Null) return TypeReflection.Null;
            return spReflectionVariable_GetType(this);
        }
    }

    public nint FindModifier(ModifierID id)
    {
        if (this == Null) return 0;
        return spReflectionVariable_FindModifier(this, id);
    }

    public uint AttributeCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionVariable_GetUserAttributeCount(this);
        }
    }

    public AttributeReflection GetAttribute(uint index)
    {
        if (this == Null) return AttributeReflection.Null;
        return spReflectionVariable_GetUserAttribute(this, index);
    }

    public AttributeReflection FindAttributeByName(ISession session, string name)
    {
        if (this == Null) return AttributeReflection.Null;
        return spReflectionVariable_FindUserAttributeByName(this, session, name);
    }

    public bool HasDefaultValue
    {
        get
        {
            if (this == Null) return false;
            return spReflectionVariable_HasDefaultValue(this);
        }
    }

    public SlangResult GetDefaultValueInt(out long value)
    {
        value = default;
        if (this == Null) return SlangResult.SLANG_E_INVALID_HANDLE;
        return spReflectionVariable_GetDefaultValueInt(this, out value);
    }

    public SlangResult GetDefaultValueFloat(out float value)
    {
        value = default;
        if (this == Null) return SlangResult.SLANG_E_INVALID_HANDLE;
        return spReflectionVariable_GetDefaultValueFloat(this, out value);
    }

    public GenericReflection GenericContainer
    {
        get
        {
            if (this == Null) return GenericReflection.Null;
            return spReflectionVariable_GetGenericContainer(this);
        }
    }

    public VariableReflection ApplySpecializations(GenericReflection generic)
    {
        if (this == Null) return Null;
        return spReflectionVariable_applySpecializations(this, generic);
    }

    // ---------------- Native Imports ---------------- //

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static partial string spReflectionVariable_GetName(VariableReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflection spReflectionVariable_GetType(VariableReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionVariable_FindModifier(VariableReflection var, ModifierID id);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionVariable_GetUserAttributeCount(VariableReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial AttributeReflection spReflectionVariable_GetUserAttribute(VariableReflection var, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial AttributeReflection spReflectionVariable_FindUserAttributeByName(VariableReflection var, ISession session, string name);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool spReflectionVariable_HasDefaultValue(VariableReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangResult spReflectionVariable_GetDefaultValueInt(VariableReflection var, out long value);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangResult spReflectionVariable_GetDefaultValueFloat(VariableReflection var, out float value);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial GenericReflection spReflectionVariable_GetGenericContainer(VariableReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableReflection spReflectionVariable_applySpecializations(VariableReflection var, GenericReflection generic);
}

[CustomMarshaller(typeof(VariableReflection), MarshalMode.Default, typeof(VariableReflectionMarshaller))]
internal static class VariableReflectionMarshaller
{
    public static nint ConvertToUnmanaged(VariableReflection managed) => managed.Handle;
    public static VariableReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}