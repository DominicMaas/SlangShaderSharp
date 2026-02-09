using SlangShaderSharp.Internal;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(FunctionReflectionMarshaller))]
public readonly partial struct FunctionReflection : IEquatable<FunctionReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use FunctionReflection.Null instead.")]
    public FunctionReflection()
    {
        Handle = 0;
    }

    internal FunctionReflection(nint value)
    {
        Handle = value;
    }

    public static FunctionReflection Null => new(0);

    public static bool operator ==(FunctionReflection left, FunctionReflection right) => left.Handle == right.Handle;
    public static bool operator !=(FunctionReflection left, FunctionReflection right) => !(left == right);

    public bool Equals(FunctionReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is FunctionReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // ---------------- Methods ---------------- //

    public string Name
    {
        get
        {
            if (this == Null) return string.Empty;
            return spReflectionFunction_GetName(this);
        }
    }

    public TypeReflection ResultType
    {
        get
        {
            if (this == Null) return TypeReflection.Null;
            return spReflectionFunction_GetResultType(this);
        }
    }

    public uint ParameterCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionFunction_GetParameterCount(this);
        }
    }

    public VariableReflection GetParameter(uint index)
    {
        if (this == Null) return VariableReflection.Null;
        return spReflectionFunction_GetParameter(this, index);
    }

    public uint AttributeCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionFunction_GetUserAttributeCount(this);
        }
    }

    public AttributeReflection GetAttribute(uint index)
    {
        if (this == Null) return AttributeReflection.Null;
        return spReflectionFunction_GetUserAttribute(this, index);
    }

    public AttributeReflection? FindAttributeByName(string name)
    {
        if (this == Null) return null;
        var attr = spReflectionFunction_FindUserAttributeByName(this, name);
        return attr == AttributeReflection.Null ? null : attr;
    }

    // ---------------- Native Imports ---------------- //

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static partial string spReflectionFunction_GetName(FunctionReflection func);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflection spReflectionFunction_GetResultType(FunctionReflection func);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionFunction_GetParameterCount(FunctionReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableReflection spReflectionFunction_GetParameter(FunctionReflection var, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionFunction_GetUserAttributeCount(FunctionReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial AttributeReflection spReflectionFunction_GetUserAttribute(FunctionReflection type, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial AttributeReflection spReflectionFunction_FindUserAttributeByName(FunctionReflection type, string name);

    // spReflectionFunction_FindModifier

    // spReflectionFunction_GetGenericContainer

    // spReflectionFunction_applySpecializations

    // spReflectionFunction_specializeWithArgTypes

    // spReflectionFunction_isOverloaded

    // spReflectionFunction_getOverloadCount

    // spReflectionFunction_getOverload
}

[CustomMarshaller(typeof(FunctionReflection), MarshalMode.Default, typeof(FunctionReflectionMarshaller))]
internal static class FunctionReflectionMarshaller
{
    public static nint ConvertToUnmanaged(FunctionReflection managed) => managed.Handle;
    public static FunctionReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}

