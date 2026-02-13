using SlangShaderSharp.Internal;
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

    // ---------------- Public Interface ---------------- //

    public SlangTypeKind Kind
    {
        get
        {
            if (this == Null) return SlangTypeKind.None;
            return spReflectionType_GetKind(this);
        }
    }

    public uint FieldCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionType_GetFieldCount(this);
        }
    }

    public uint RowCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionType_GetRowCount(this);
        }
    }

    public uint ColumnCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionType_GetColumnCount(this);
        }
    }

    public SlangScalarType ScalarType
    {
        get
        {
            if (this == Null) return SlangScalarType.None;
            return spReflectionType_GetScalarType(this);
        }
    }

    public TypeReflection ResourceResultType
    {
        get
        {
            if (this == Null) return Null;
            return spReflectionType_GetResourceResultType(this);
        }
    }

    public SlangResourceShape ResourceShape
    {
        get
        {
            if (this == Null) return SlangResourceShape.None;
            return spReflectionType_GetResourceShape(this);
        }
    }

    public SlangResourceAccess ResourceAccess
    {
        get
        {
            if (this == Null) return SlangResourceAccess.None;
            return spReflectionType_GetResourceAccess(this);
        }
    }

    public string Name
    {
        get
        {
            if (this == Null) return string.Empty;
            return spReflectionType_GetName(this);
        }
    }

    public TypeReflection ElementType
    {
        get
        {
            if (this == Null) return Null;
            return spReflectionType_GetElementType(this);
        }
    }

    public nuint ElementCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionType_GetElementCount(this);
        }
    }

    public uint AttributeCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionType_GetUserAttributeCount(this);
        }
    }

    public bool IsArray => Kind == SlangTypeKind.Array;

    public TypeReflection UnwrapArray()
    {
        var type = this;
        while (type.IsArray)
        {
            type = type.ElementType;
        }
        return type;
    }

    public nuint TotalArrayElementCount
    {
        get
        {
            if (!IsArray) return 0;

            nuint result = 1;
            var type = this;

            for (; ; )
            {
                if (!type.IsArray) return result;

                var c = type.ElementCount;

                if (c == Slang.UnknownSize) return Slang.UnknownSize;
                if (c == Slang.UnboundedSize) return Slang.UnboundedSize;

                result *= c;
                type = type.ElementType;
            }
        }
    }

    public VariableReflection GetFieldByIndex(uint index)
    {
        if (this == Null) return VariableReflection.Null;
        return spReflectionType_GetFieldByIndex(this, index);
    }

    public nuint GetSpecializedElementCount(ShaderReflection reflection)
    {
        if (this == Null) return 0;
        return spReflectionType_GetSpecializedElementCount(this, reflection);
    }

    public SlangResult GetFullName(out ISlangBlob? nameBlob)
    {
        if (this == Null)
        {
            nameBlob = null;
            return SlangResult.SLANG_FAIL;
        }
        return spReflectionType_GetFullName(this, out nameBlob);
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

    public AttributeReflection FindUserAttributeByName(string name) => FindAttributeByName(name);

    public TypeReflection ApplySpecializations(GenericReflection generic)
    {
        if (this == Null) return Null;
        return spReflectionType_applySpecializations(this, generic);
    }

    public GenericReflection GenericContainer
    {
        get
        {
            if (this == Null) return GenericReflection.Null;
            return spReflectionType_GetGenericContainer(this);
        }
    }

    // ---------------- Native Imports ---------------- //


    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangTypeKind spReflectionType_GetKind(TypeReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionType_GetFieldCount(TypeReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableReflection spReflectionType_GetFieldByIndex(TypeReflection type, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nuint spReflectionType_GetElementCount(TypeReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nuint spReflectionType_GetSpecializedElementCount(TypeReflection type, ShaderReflection reflection);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflection spReflectionType_GetElementType(TypeReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionType_GetRowCount(TypeReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionType_GetColumnCount(TypeReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangScalarType spReflectionType_GetScalarType(TypeReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflection spReflectionType_GetResourceResultType(TypeReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangResourceShape spReflectionType_GetResourceShape(TypeReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangResourceAccess spReflectionType_GetResourceAccess(TypeReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static partial string spReflectionType_GetName(TypeReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangResult spReflectionType_GetFullName(TypeReflection type, out ISlangBlob? nameBlob);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionType_GetUserAttributeCount(TypeReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial AttributeReflection spReflectionType_GetUserAttribute(TypeReflection type, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial AttributeReflection spReflectionType_FindUserAttributeByName(TypeReflection type, string name);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflection spReflectionType_applySpecializations(TypeReflection type, GenericReflection generic);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial GenericReflection spReflectionType_GetGenericContainer(TypeReflection type);
}

[CustomMarshaller(typeof(TypeReflection), MarshalMode.Default, typeof(TypeReflectionMarshaller))]
internal static class TypeReflectionMarshaller
{
    public static nint ConvertToUnmanaged(TypeReflection managed) => managed.Handle;
    public static TypeReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}