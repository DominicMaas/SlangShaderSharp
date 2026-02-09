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

    // ---------------- Properties ---------------- //

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
            if (this == Null) return string.Empty;
            return Variable.Name;
        }
    }

    public TypeReflection Type
    {
        get
        {
            if (this == Null) return TypeReflection.Null;
            return Variable.Type;
        }
    }

    public TypeLayoutReflection TypeLayout
    {
        get
        {
            if (this == Null) return TypeLayoutReflection.Null;
            return spReflectionVariableLayout_GetTypeLayout(this);
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

    public nuint BindingSpace
    {
        get
        {
            if (this == Null) return 0;
            return spReflectionParameter_GetBindingSpace(this);
        }
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

    // ---------------- Methods ---------------- //


    public nuint GetOffset(SlangParameterCategory category = SlangParameterCategory.Uniform)
    {
        if (this == Null) return 0;
        return spReflectionVariableLayout_GetOffset(this, category);
    }

    public nuint GetSpace(SlangParameterCategory category)
    {
        if (this == Null) return 0;
        return spReflectionVariableLayout_GetSpace(this, category);
    }

    // ---------------- Native Imports ---------------- //

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableReflection spReflectionVariableLayout_GetVariable(VariableLayoutReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeLayoutReflection spReflectionVariableLayout_GetTypeLayout(VariableLayoutReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nuint spReflectionVariableLayout_GetOffset(VariableLayoutReflection var, SlangParameterCategory category);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionParameter_GetBindingIndex(VariableLayoutReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nuint spReflectionParameter_GetBindingSpace(VariableLayoutReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nuint spReflectionVariableLayout_GetSpace(VariableLayoutReflection var, SlangParameterCategory category);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangImageFormat spReflectionVariableLayout_GetImageFormat(VariableLayoutReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static partial string spReflectionVariableLayout_GetSemanticName(VariableLayoutReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nuint spReflectionVariableLayout_GetSemanticIndex(VariableLayoutReflection var);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangStage spReflectionVariableLayout_getStage(VariableLayoutReflection var);
}

[CustomMarshaller(typeof(VariableLayoutReflection), MarshalMode.Default, typeof(VariableLayoutReflectionMarshaller))]
internal static class VariableLayoutReflectionMarshaller
{
    public static nint ConvertToUnmanaged(VariableLayoutReflection managed) => managed.Handle;
    public static VariableLayoutReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
