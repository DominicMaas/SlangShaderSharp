using SlangShaderSharp.Internal;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(VariableLayoutReflectionMarshaller))]
public readonly partial struct VariableLayoutReflection : IEquatable<VariableLayoutReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use VariableLayoutReflection.Null instead.")]
    public VariableLayoutReflection()
    {
        Handle = 0;
    }

    internal VariableLayoutReflection(nint value)
    {
        Handle = value;
    }

    public static VariableLayoutReflection Null => new(0);

    public static bool operator ==(VariableLayoutReflection left, VariableLayoutReflection right) => left.Handle == right.Handle;
    public static bool operator !=(VariableLayoutReflection left, VariableLayoutReflection right) => !(left == right);

    public bool Equals(VariableLayoutReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is VariableLayoutReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // ---------------- Public Interface ---------------- //

    public VariableReflection Variable
    {
        get
        {
            if (this == Null) return VariableReflection.Null;
            return spReflectionVariableLayout_GetVariable(this);
        }
    }

    public string Name
    {
        get
        {
            // Proxy to Variable
            var v = Variable;
            return v == VariableReflection.Null ? string.Empty : v.Name;
        }
    }

    public nint FindModifier(ModifierID id)
    {
        // Proxy to Variable
        return Variable.FindModifier(id);
    }

    public TypeLayoutReflection TypeLayout
    {
        get
        {
            if (this == Null) return TypeLayoutReflection.Null;
            return spReflectionVariableLayout_GetTypeLayout(this);
        }
    }

    public SlangParameterCategory Category
    {
        get
        {
            // Proxy to TypeLayout
            return TypeLayout.ParameterCategory;
        }
    }

    public uint CategoryCount
    {
        get
        {
            // Proxy to TypeLayout
            return TypeLayout.CategoryCount;
        }
    }

    public SlangParameterCategory GetCategoryByIndex(uint index)
    {
        // Proxy to TypeLayout
        return TypeLayout.GetCategoryByIndex(index);
    }

    public nuint GetOffset(SlangParameterCategory category = SlangParameterCategory.Uniform)
    {
        if (this == Null) return 0;
        return spReflectionVariableLayout_GetOffset(this, category);
    }

    public TypeReflection Type
    {
        get
        {
            // Proxy to Variable which proxies to its Type
            return Variable.Type;
        }
    }

    public uint BindingIndex
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionParameter_GetBindingIndex(this);
        }
    }

    public uint BindingSpace
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionParameter_GetBindingSpace(this);
        }
    }

    public nuint GetBindingSpace(SlangParameterCategory category)
    {
        if (this == Null) return 0;
        return spReflectionVariableLayout_GetSpace(this, category);
    }

    public SlangImageFormat ImageFormat
    {
        get
        {
            if (this == Null) return SlangImageFormat.Unknown;
            return spReflectionVariableLayout_GetImageFormat(this);
        }
    }

    public string SemanticName
    {
        get
        {
            if (this == Null) return string.Empty;
            return spReflectionVariableLayout_GetSemanticName(this);
        }
    }

    public nuint SemanticIndex
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionVariableLayout_GetSemanticIndex(this);
        }
    }

    public SlangStage Stage
    {
        get
        {
            if (this == Null) return SlangStage.None;
            return spReflectionVariableLayout_getStage(this);
        }
    }

    // ---------------- Native Imports ---------------- //

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableReflection spReflectionVariableLayout_GetVariable(VariableLayoutReflection vars);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeLayoutReflection spReflectionVariableLayout_GetTypeLayout(VariableLayoutReflection vars);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nuint spReflectionVariableLayout_GetOffset(VariableLayoutReflection vars, SlangParameterCategory category);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionParameter_GetBindingIndex(VariableLayoutReflection vars);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionParameter_GetBindingSpace(VariableLayoutReflection vars);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nuint spReflectionVariableLayout_GetSpace(VariableLayoutReflection vars, SlangParameterCategory category);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangImageFormat spReflectionVariableLayout_GetImageFormat(VariableLayoutReflection vars);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static partial string spReflectionVariableLayout_GetSemanticName(VariableLayoutReflection vars);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nuint spReflectionVariableLayout_GetSemanticIndex(VariableLayoutReflection vars);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangStage spReflectionVariableLayout_getStage(VariableLayoutReflection vars);
}

[CustomMarshaller(typeof(VariableLayoutReflection), MarshalMode.Default, typeof(VariableLayoutReflectionMarshaller))]
internal static class VariableLayoutReflectionMarshaller
{
    public static nint ConvertToUnmanaged(VariableLayoutReflection managed) => managed.Handle;
    public static VariableLayoutReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
