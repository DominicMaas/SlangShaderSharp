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

    // ---------------- Properties ---------------- //

    public TypeReflection Type
    {
        get
        {
            if (this == Null) return TypeReflection.Null;
            return spReflectionTypeLayout_GetType(this);
        }
    }

    public SlangTypeKind Kind
    {
        get
        {
            if (this == Null) return SlangTypeKind.None;
            return spReflectionTypeLayout_getKind(this);
        }
    }

    public uint FieldCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionTypeLayout_GetFieldCount(this);
        }
    }

    public TypeLayoutReflection ElementTypeLayout
    {
        get
        {
            if (this == Null) return Null;
            return spReflectionTypeLayout_GetElementTypeLayout(this);
        }
    }

    public VariableLayoutReflection ElementVarLayout
    {
        get
        {
            if (this == Null) return VariableLayoutReflection.Null;
            return spReflectionTypeLayout_GetElementVarLayout(this);
        }
    }

    public VariableLayoutReflection ContainerVarLayout
    {
        get
        {
            if (this == Null) return VariableLayoutReflection.Null;
            return spReflectionTypeLayout_getContainerVarLayout(this);
        }
    }

    public SlangParameterCategory ParameterCategory
    {
        get
        {
            if (this == Null) return SlangParameterCategory.None;
            return spReflectionTypeLayout_GetParameterCategory(this);
        }
    }

    public uint CategoryCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionTypeLayout_GetCategoryCount(this);
        }
    }

    public SlangMatrixLayoutMode MatrixLayoutMode
    {
        get
        {
            if (this == Null) return SlangMatrixLayoutMode.Unknown;
            return spReflectionTypeLayout_GetMatrixLayoutMode(this);
        }
    }

    public int GenericParamIndex
    {
        get
        {
            if (this == Null) return -1;
            return spReflectionTypeLayout_getGenericParamIndex(this);
        }
    }

    public nint BindingRangeCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionTypeLayout_getBindingRangeCount(this);
        }
    }

    public nint SubObjectRangeCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionTypeLayout_getSubObjectRangeCount(this);
        }
    }

    public nint DescriptorSetCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionTypeLayout_getDescriptorSetCount(this);
        }
    }

    public VariableLayoutReflection ExplicitCounter
    {
        get
        {
            if (this == Null) return VariableLayoutReflection.Null;
            return spReflectionTypeLayout_GetExplicitCounter(this);
        }
    }

    // ---------------- Methods ---------------- //

    public nuint GetSize(SlangParameterCategory category)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_GetSize(this, category);
    }

    public nuint GetStride(SlangParameterCategory category)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_GetStride(this, category);
    }

    public int GetAlignment(SlangParameterCategory category)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_getAlignment(this, category);
    }

    public VariableLayoutReflection GetFieldByIndex(uint index)
    {
        if (this == Null) return VariableLayoutReflection.Null;
        return spReflectionTypeLayout_GetFieldByIndex(this, index);
    }

    public nint FindFieldIndexByName(string nameBegin, string? nameEnd = null)
    {
        if (this == Null) return -1;
        return spReflectionTypeLayout_findFieldIndexByName(this, nameBegin, nameEnd);
    }

    public nuint GetElementStride(SlangParameterCategory category)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_GetElementStride(this, category);
    }

    public SlangParameterCategory GetCategoryByIndex(uint index)
    {
        if (this == Null) return SlangParameterCategory.None;
        return spReflectionTypeLayout_GetCategoryByIndex(this, index);
    }

    public SlangBindingType GetBindingRangeType(nint index)
    {
        if (this == Null) return SlangBindingType.Unknown;
        return spReflectionTypeLayout_getBindingRangeType(this, index);
    }

    public bool IsBindingRangeSpecializable(nint index)
    {
        if (this == Null) return false;
        return spReflectionTypeLayout_isBindingRangeSpecializable(this, index);
    }

    public nint GetBindingRangeBindingCount(nint index)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_getBindingRangeBindingCount(this, index);
    }

    public nint GetFieldBindingRangeOffset(nint fieldIndex)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_getFieldBindingRangeOffset(this, fieldIndex);
    }

    public nint GetExplicitCounterBindingRangeOffset()
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_getExplicitCounterBindingRangeOffset(this);
    }

    public TypeLayoutReflection GetBindingRangeLeafTypeLayout(nint index)
    {
        if (this == Null) return Null;
        return spReflectionTypeLayout_getBindingRangeLeafTypeLayout(this, index);
    }

    public VariableReflection GetBindingRangeLeafVariable(nint index)
    {
        if (this == Null) return VariableReflection.Null;
        return spReflectionTypeLayout_getBindingRangeLeafVariable(this, index);
    }

    public SlangImageFormat GetBindingRangeImageFormat(nint index)
    {
        if (this == Null) return SlangImageFormat.Unknown;
        return spReflectionTypeLayout_getBindingRangeImageFormat(this, index);
    }

    public nint GetBindingRangeDescriptorSetIndex(nint index)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_getBindingRangeDescriptorSetIndex(this, index);
    }

    public nint GetBindingRangeFirstDescriptorRangeIndex(nint index)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_getBindingRangeFirstDescriptorRangeIndex(this, index);
    }

    public nint GetBindingRangeDescriptorRangeCount(nint index)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_getBindingRangeDescriptorRangeCount(this, index);
    }

    public nint GetDescriptorSetSpaceOffset(nint setIndex)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_getDescriptorSetSpaceOffset(this, setIndex);
    }

    public nint GetDescriptorSetDescriptorRangeCount(nint setIndex)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_getDescriptorSetDescriptorRangeCount(this, setIndex);
    }

    public nint GetDescriptorSetDescriptorRangeIndexOffset(nint setIndex, nint rangeIndex)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_getDescriptorSetDescriptorRangeIndexOffset(this, setIndex, rangeIndex);
    }

    public nint GetDescriptorSetDescriptorRangeDescriptorCount(nint setIndex, nint rangeIndex)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_getDescriptorSetDescriptorRangeDescriptorCount(this, setIndex, rangeIndex);
    }

    public SlangBindingType GetDescriptorSetDescriptorRangeType(nint setIndex, nint rangeIndex)
    {
        if (this == Null) return SlangBindingType.Unknown;
        return spReflectionTypeLayout_getDescriptorSetDescriptorRangeType(this, setIndex, rangeIndex);
    }

    public SlangParameterCategory GetDescriptorSetDescriptorRangeCategory(nint setIndex, nint rangeIndex)
    {
        if (this == Null) return SlangParameterCategory.None;
        return spReflectionTypeLayout_getDescriptorSetDescriptorRangeCategory(this, setIndex, rangeIndex);
    }

    public nint GetSubObjectRangeBindingRangeIndex(nint subObjectRangeIndex)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_getSubObjectRangeBindingRangeIndex(this, subObjectRangeIndex);
    }

    public nint GetSubObjectRangeSpaceOffset(nint subObjectRangeIndex)
    {
        if (this == Null) return 0;
        return spReflectionTypeLayout_getSubObjectRangeSpaceOffset(this, subObjectRangeIndex);
    }

    public VariableLayoutReflection GetSubObjectRangeOffset(nint subObjectRangeIndex)
    {
        if (this == Null) return VariableLayoutReflection.Null;
        return spReflectionTypeLayout_getSubObjectRangeOffset(this, subObjectRangeIndex);
    }

    // ---------------- Native Imports ---------------- //

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflection spReflectionTypeLayout_GetType(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangTypeKind spReflectionTypeLayout_getKind(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nuint spReflectionTypeLayout_GetSize(TypeLayoutReflection type, SlangParameterCategory category);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nuint spReflectionTypeLayout_GetStride(TypeLayoutReflection type, SlangParameterCategory category);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial int spReflectionTypeLayout_getAlignment(TypeLayoutReflection type, SlangParameterCategory category);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionTypeLayout_GetFieldCount(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableLayoutReflection spReflectionTypeLayout_GetFieldByIndex(TypeLayoutReflection type, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_findFieldIndexByName(TypeLayoutReflection type, string nameBegin, string? nameEnd);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableLayoutReflection spReflectionTypeLayout_GetExplicitCounter(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nuint spReflectionTypeLayout_GetElementStride(TypeLayoutReflection type, SlangParameterCategory category);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeLayoutReflection spReflectionTypeLayout_GetElementTypeLayout(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableLayoutReflection spReflectionTypeLayout_GetElementVarLayout(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableLayoutReflection spReflectionTypeLayout_getContainerVarLayout(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangParameterCategory spReflectionTypeLayout_GetParameterCategory(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionTypeLayout_GetCategoryCount(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangParameterCategory spReflectionTypeLayout_GetCategoryByIndex(TypeLayoutReflection type, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangMatrixLayoutMode spReflectionTypeLayout_GetMatrixLayoutMode(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial int spReflectionTypeLayout_getGenericParamIndex(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getBindingRangeCount(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangBindingType spReflectionTypeLayout_getBindingRangeType(TypeLayoutReflection type, nint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool spReflectionTypeLayout_isBindingRangeSpecializable(TypeLayoutReflection type, nint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getBindingRangeBindingCount(TypeLayoutReflection type, nint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getFieldBindingRangeOffset(TypeLayoutReflection type, nint fieldIndex);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getExplicitCounterBindingRangeOffset(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeLayoutReflection spReflectionTypeLayout_getBindingRangeLeafTypeLayout(TypeLayoutReflection type, nint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableReflection spReflectionTypeLayout_getBindingRangeLeafVariable(TypeLayoutReflection type, nint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangImageFormat spReflectionTypeLayout_getBindingRangeImageFormat(TypeLayoutReflection type, nint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getBindingRangeDescriptorSetIndex(TypeLayoutReflection type, nint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getBindingRangeFirstDescriptorRangeIndex(TypeLayoutReflection type, nint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getBindingRangeDescriptorRangeCount(TypeLayoutReflection type, nint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getDescriptorSetCount(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getDescriptorSetSpaceOffset(TypeLayoutReflection type, nint setIndex);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getDescriptorSetDescriptorRangeCount(TypeLayoutReflection type, nint setIndex);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getDescriptorSetDescriptorRangeIndexOffset(TypeLayoutReflection type, nint setIndex, nint rangeIndex);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getDescriptorSetDescriptorRangeDescriptorCount(TypeLayoutReflection type, nint setIndex, nint rangeIndex);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangBindingType spReflectionTypeLayout_getDescriptorSetDescriptorRangeType(TypeLayoutReflection type, nint setIndex, nint rangeIndex);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangParameterCategory spReflectionTypeLayout_getDescriptorSetDescriptorRangeCategory(TypeLayoutReflection type, nint setIndex, nint rangeIndex);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getSubObjectRangeCount(TypeLayoutReflection type);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getSubObjectRangeBindingRangeIndex(TypeLayoutReflection type, nint subObjectRangeIndex);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionTypeLayout_getSubObjectRangeSpaceOffset(TypeLayoutReflection type, nint subObjectRangeIndex);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableLayoutReflection spReflectionTypeLayout_getSubObjectRangeOffset(TypeLayoutReflection type, nint subObjectRangeIndex);
}

[CustomMarshaller(typeof(TypeLayoutReflection), MarshalMode.Default, typeof(TypeLayoutReflectionMarshaller))]
internal static class TypeLayoutReflectionMarshaller
{
    public static nint ConvertToUnmanaged(TypeLayoutReflection managed) => managed.Handle;
    public static TypeLayoutReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
