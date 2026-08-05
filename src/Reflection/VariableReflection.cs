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

    public AttributeReflection FindAttributeByName(IGlobalSession session, string name)
    {
        if (this == Null) return AttributeReflection.Null;
        return spReflectionVariable_FindUserAttributeByName(this, session, name);
    }

    /// <summary>
    ///     Deprecated: call <see cref="GetDefaultValueBlob"/> and check for a null blob instead.
    /// </summary>
    [Obsolete("Call GetDefaultValueBlob and check for a null blob instead.")]
    public bool HasDefaultValue
    {
        get
        {
            if (this == Null) return false;
            return spReflectionVariable_HasDefaultValue(this);
        }
    }

    /// <summary>
    ///     Deprecated: use <see cref="GetDefaultValueBlob"/> instead.
    ///
    ///     Gets an integer default value. For specialized generic static constants,
    ///     the semantic value is resolved under the current specialization first;
    ///     literal initializers are used as a fallback when no integer value resolves.
    /// </summary>
    [Obsolete("Use GetDefaultValueBlob instead.")]
    public SlangResult GetDefaultValueInt(out long value)
    {
        value = default;
        if (this == Null) return SlangResult.SLANG_E_INVALID_HANDLE;
        return spReflectionVariable_GetDefaultValueInt(this, out value);
    }

    /// <summary>
    ///     Deprecated: use <see cref="GetDefaultValueBlob"/> instead.
    ///
    ///     Gets a floating-point default value from a literal initializer. Unlike
    ///     GetDefaultValueInt, this API does not currently resolve specialized
    ///     generic semantic values before checking the initializer.
    /// </summary>
    [Obsolete("Use GetDefaultValueBlob instead.")]
    public SlangResult GetDefaultValueFloat(out float value)
    {
        value = default;
        if (this == Null) return SlangResult.SLANG_E_INVALID_HANDLE;
        return spReflectionVariable_GetDefaultValueFloat(this, out value);
    }

    /// <summary>
    ///     Retrieves a variable's default initializer as a packed byte blob.
    ///
    ///     If the variable has no explicit initializer, returns <see cref="SlangResult.SLANG_OK"/> and sets
    ///     <paramref name="blob"/> to <c>null</c>. Otherwise <paramref name="blob"/> receives an
    ///     <see cref="ISlangBlob"/> holding the initializer's bytes; the caller owns that reference. Returns
    ///     <see cref="SlangResult.SLANG_E_NOT_AVAILABLE"/> when the initializer cannot be represented as a
    ///     default-value blob.
    ///
    ///     Scalars, vectors, matrices, fixed-size arrays, structs/aggregates, and enums are supported.
    ///     Values are packed in natural scalar/field order with no aggregate padding: matrices
    ///     row-by-row, base-class fields before derived fields, and a field with no explicit initializer
    ///     as its zero/default representation. Encoding is target-independent: <c>bool</c> occupies 4 bytes
    ///     to match Slang's GPU scalar layout, <c>intptr_t</c>/<c>uintptr_t</c> always occupy 8 bytes
    ///     signed/unsigned (consumers on narrower-pointer targets must narrow explicitly), and enums use
    ///     their underlying tag type.
    ///
    ///     Scalars are stored in host byte order (little-endian on all supported platforms), and the
    ///     buffer is aligned to at least the maximum scalar alignment, which covers every scalar type
    ///     encoded by this API. After checking the blob size, callers may reinterpret
    ///     <see cref="ISlangBlob.GetBufferPointer"/> directly as the payload element type.
    /// </summary>
    public SlangResult GetDefaultValueBlob(out ISlangBlob? blob)
    {
        blob = null;
        if (this == Null) return SlangResult.SLANG_E_INVALID_HANDLE;

        // `slang::VariableReflection::getDefaultValueBlob` is a non-inline C++ member function rather
        // than an `spReflection*` C entry point, so it is exported under a C++-mangled name. The
        // implicit `this` becomes a leading handle argument under both the MSVC and Itanium ABIs.
        return OperatingSystem.IsWindows()
            ? getDefaultValueBlob_Msvc(this, out blob)
            : getDefaultValueBlob_Itanium(this, out blob);
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
    private static partial AttributeReflection spReflectionVariable_FindUserAttributeByName(VariableReflection var, IGlobalSession session, string name);

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

    // slang::VariableReflection::getDefaultValueBlob(ISlangBlob**) — MSVC mangling (win-x64, win-arm64).
    [LibraryImport(Slang.LibraryName, EntryPoint = "?getDefaultValueBlob@VariableReflection@slang@@QEAAHPEAPEAUISlangBlob@@@Z", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangResult getDefaultValueBlob_Msvc(VariableReflection var, out ISlangBlob? blob);

    // slang::VariableReflection::getDefaultValueBlob(ISlangBlob**) — Itanium mangling (linux-*, osx-*).
    [LibraryImport(Slang.LibraryName, EntryPoint = "_ZN5slang18VariableReflection19getDefaultValueBlobEPP10ISlangBlob", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial SlangResult getDefaultValueBlob_Itanium(VariableReflection var, out ISlangBlob? blob);

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