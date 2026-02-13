using SlangShaderSharp.Internal;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(ShaderReflectionMarshaller))]
public readonly partial struct ShaderReflection : IEquatable<ShaderReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use ShaderReflection.Null instead.")]
    public ShaderReflection()
    {
        Handle = 0;
    }

    internal ShaderReflection(nint value)
    {
        Handle = value;
    }

    public static ShaderReflection Null => new(0);

    public static bool operator ==(ShaderReflection left, ShaderReflection right) => left.Handle == right.Handle;
    public static bool operator !=(ShaderReflection left, ShaderReflection right) => !(left == right);

    public bool Equals(ShaderReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is ShaderReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // ---------------- Methods ---------------- //

    public uint ParameterCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflection_GetParameterCount(this);
        }
    }

    public uint TypeParameterCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflection_GetTypeParameterCount(this);
        }
    }

    public ISession? GetSession()
    {
        if (this == Null) return null;
        return spReflection_GetSession(this);
    }

    public TypeParameterReflection GetTypeParameterByIndex(uint index)
    {
        if (this == Null) return TypeParameterReflection.Null;
        return spReflection_GetTypeParameterByIndex(this, index);
    }

    public TypeParameterReflection? FindTypeParameter(string name)
    {
        if (this == Null) return null;
        var result = spReflection_FindTypeParameter(this, name);
        return result == TypeParameterReflection.Null ? null : result;
    }

    public VariableLayoutReflection GetParameterByIndex(uint index)
    {
        if (this == Null) return VariableLayoutReflection.Null;
        return spReflection_GetParameterByIndex(this, index);
    }

    public static ShaderReflection GetReflection(ICompileRequest request)
    {
        return spGetReflection(request);
    }

    public uint EntryPointCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflection_getEntryPointCount(this);
        }
    }

    public EntryPointReflection GetEntryPointByIndex(uint index)
    {
        if (this == Null) return EntryPointReflection.Null;
        return spReflection_getEntryPointByIndex(this, index);
    }

    /// <summary>
    ///     Get the binding index for the global constant buffer.
    ///
    ///     Returns `SLANG_UNKNOWN_SIZE` when the binding depends on unresolved generic parameters or link-time constants.
    /// </summary>
    public nuint GlobalConstantBufferBinding
    {
        get
        {
            if (this == Null) return 0;
            return spReflection_getGlobalConstantBufferBinding(this);
        }
    }

    /// <summary>
    ///     Get the size of the global constant buffer.
    ///
    ///     Returns `SLANG_UNBOUNDED_SIZE` for unbounded resources.
    ///     Returns `SLANG_UNKNOWN_SIZE` when the size depends on unresolved generic parameters or link-time constants.
    /// </summary>
    public nuint GlobalConstantBufferSize
    {
        get
        {
            if (this == Null) return 0;
            return spReflection_getGlobalConstantBufferSize(this);
        }
    }

    public TypeReflection? FindTypeByName(string name)
    {
        if (this == Null) return null;
        var result = spReflection_FindTypeByName(this, name);
        return result == TypeReflection.Null ? null : result;
    }

    public FunctionReflection? FindFunctionByName(string name)
    {
        if (this == Null) return null;
        var result = spReflection_FindFunctionByName(this, name);
        return result == FunctionReflection.Null ? null : result;
    }

    public FunctionReflection? FindFunctionByNameInType(TypeReflection reflectionType, string name)
    {
        if (this == Null) return null;
        var result = spReflection_FindFunctionByNameInType(this, reflectionType, name);
        return result == FunctionReflection.Null ? null : result;
    }

    public unsafe FunctionReflection? TryResolveOverloadedFunction(ReadOnlySpan<FunctionReflection> candidates)
    {
        if (this == Null) return null;

        fixed (FunctionReflection* pCandidates = candidates)
        {
            var result = spReflection_TryResolveOverloadedFunction(this, candidates.Length, pCandidates);
            return result == FunctionReflection.Null ? null : result;
        }
    }

    public VariableReflection? FindVarByNameInType(TypeReflection reflectionType, string name)
    {
        if (this == Null) return null;
        var result = spReflection_FindVarByNameInType(this, reflectionType, name);
        return result == VariableReflection.Null ? null : result;
    }

    public TypeLayoutReflection? GetTypeLayout(TypeReflection reflectionType, LayoutRules rules)
    {
        if (this == Null) return null;
        var result = spReflection_GetTypeLayout(this, reflectionType, rules);
        return result == TypeLayoutReflection.Null ? null : result;
    }

    public EntryPointReflection? FindEntryPointByName(string name)
    {
        if (this == Null) return null;
        var result = spReflection_findEntryPointByName(this, name);
        return result == EntryPointReflection.Null ? null : result;
    }

    public unsafe TypeReflection? SpecializeType(TypeReflection type, ReadOnlySpan<TypeReflection> args, out ISlangBlob diagnostics)
    {
        if (this == Null)
        {
            diagnostics = null!;
            return null;
        }

        fixed (TypeReflection* pArgs = args)
        {
            var result = spReflection_specializeType(this, type, args.Length, pArgs, out diagnostics);
            return result == TypeReflection.Null ? null : result;
        }
    }

    public unsafe GenericReflection SpecializeGeneric(GenericReflection generic, ReadOnlySpan<SlangReflectionGenericArgType> argTypes, ReadOnlySpan<SlangReflectionGenericArg> argVals, out ISlangBlob diagnostics)
    {
        if (this == Null)
        {
            diagnostics = null!;
            return GenericReflection.Null;
        }

        fixed (SlangReflectionGenericArgType* pArgTypes = argTypes)
        fixed (SlangReflectionGenericArg* pArgVals = argVals)
        {
            return spReflection_specializeGeneric(this, generic, argTypes.Length, pArgTypes, pArgVals, out diagnostics);
        }
    }

    public bool IsSubType(TypeReflection subType, TypeReflection superType)
    {
        if (this == Null) return false;
        return spReflection_isSubType(this, subType, superType);
    }

    public uint HashedStringCount
    {
        get
        {
            if (this == Null) return 0;
            return spReflection_getHashedStringCount(this);
        }
    }

    public string? GetHashedString(uint index)
    {
        if (this == Null) return null;
        var result = spReflection_getHashedString(this, index, out var count);
        return count == 0 ? null : result;
    }

    public TypeLayoutReflection? GetGlobalParamsTypeLayout()
    {
        if (this == Null) return null;
        var result = spReflection_getGlobalParamsTypeLayout(this);
        return result == TypeLayoutReflection.Null ? null : result;
    }

    public VariableLayoutReflection? GetGlobalParamsVarLayout()
    {
        if (this == Null) return null;
        var result = spReflection_getGlobalParamsVarLayout(this);
        return result == VariableLayoutReflection.Null ? null : result;
    }

    public SlangResult ToJson(out ISlangBlob blob)
    {
        if (this == Null)
        {
            blob = null!;
            return SlangResult.SLANG_E_INVALID_HANDLE;
        }

        return spReflection_ToJson(this, null!, out blob);
    }

    /// <summary>
    ///     Get the descriptor set/space index allocated for the bindless resource heap.
    ///     Returns -1 if the program does not use bindless resource heap.
    /// </summary>
    public nint BindlessSpaceIndex
    {
        get
        {
            if (this == Null) return -1;
            return spReflection_getBindlessSpaceIndex(this);
        }
    }

    // ---------------- Native Imports ----------------

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflection_GetParameterCount(ShaderReflection reflection);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflection_GetTypeParameterCount(ShaderReflection reflection);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial ISession? spReflection_GetSession(ShaderReflection reflection);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeParameterReflection spReflection_GetTypeParameterByIndex(ShaderReflection reflection, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeParameterReflection spReflection_FindTypeParameter(ShaderReflection reflection, string name);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableLayoutReflection spReflection_GetParameterByIndex(ShaderReflection reflection, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial ShaderReflection spGetReflection(ICompileRequest request);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflection_getEntryPointCount(ShaderReflection reflection);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial EntryPointReflection spReflection_getEntryPointByIndex(ShaderReflection reflection, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflection_getGlobalConstantBufferBinding(ShaderReflection reflection);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nuint spReflection_getGlobalConstantBufferSize(ShaderReflection reflection);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflection spReflection_FindTypeByName(ShaderReflection reflection, string name);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial FunctionReflection spReflection_FindFunctionByName(ShaderReflection reflection, string name);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial FunctionReflection spReflection_FindFunctionByNameInType(ShaderReflection reflection, TypeReflection reflectionType, string name);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static unsafe partial FunctionReflection spReflection_TryResolveOverloadedFunction(ShaderReflection reflection, int candidateCount, FunctionReflection* candidates);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableReflection spReflection_FindVarByNameInType(ShaderReflection reflection, TypeReflection reflectionType, string name);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeLayoutReflection spReflection_GetTypeLayout(ShaderReflection reflection, TypeReflection reflectionType, LayoutRules rules);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial EntryPointReflection spReflection_findEntryPointByName(ShaderReflection reflection, string name);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static unsafe partial TypeReflection spReflection_specializeType(ShaderReflection reflection, TypeReflection type, nint specializationArgCount, TypeReflection* specializationArgs, out ISlangBlob diagnostics);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static unsafe partial GenericReflection spReflection_specializeGeneric(ShaderReflection reflection, GenericReflection generic, nint specializationArgCount, SlangReflectionGenericArgType* specializationArgTypes, SlangReflectionGenericArg* specializationArgVals, out ISlangBlob diagnostics);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool spReflection_isSubType(ShaderReflection reflection, TypeReflection subType, TypeReflection superType);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflection_getHashedStringCount(ShaderReflection reflection);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static partial string spReflection_getHashedString(ShaderReflection reflection, uint index, out nuint count);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeLayoutReflection spReflection_getGlobalParamsTypeLayout(ShaderReflection reflection);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableLayoutReflection spReflection_getGlobalParamsVarLayout(ShaderReflection reflection);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangResult spReflection_ToJson(ShaderReflection reflection, ICompileRequest compileRequest, out ISlangBlob blob);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflection_getBindlessSpaceIndex(ShaderReflection reflection);

}

[CustomMarshaller(typeof(ShaderReflection), MarshalMode.Default, typeof(ShaderReflectionMarshaller))]
internal static class ShaderReflectionMarshaller
{
    public static nint ConvertToUnmanaged(ShaderReflection managed) => managed.Handle;
    public static ShaderReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
