using SlangShaderSharp.Internal;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(DeclReflectionMarshaller))]
public readonly partial struct DeclReflection : IEquatable<DeclReflection>, IReadOnlyList<DeclReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use DeclReflection.Null instead.")]
    public DeclReflection()
    {
        Handle = 0;
    }

    internal DeclReflection(nint value)
    {
        Handle = value;
    }

    public static DeclReflection Null => new(0);

    public static bool operator ==(DeclReflection left, DeclReflection right) => left.Handle == right.Handle;
    public static bool operator !=(DeclReflection left, DeclReflection right) => !(left == right);

    public bool Equals(DeclReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is DeclReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // ---------------- Methods ---------------- //

    public unsafe string? Name => Handle != 0 ? spReflectionDecl_getName(Handle) : null;

    public unsafe DeclReflectionKind Kind => Handle != 0 ? spReflectionDecl_getKind(Handle) : DeclReflectionKind.Unsupported;

    public TypeReflection Type => Handle != 0 ? spReflection_getTypeFromDecl(Handle) : TypeReflection.Null;

    public VariableReflection AsVariable() => Handle != 0 ? spReflectionDecl_castToVariable(Handle) : VariableReflection.Null;

    public FunctionReflection AsFunction() => Handle != 0 ? spReflectionDecl_castToFunction(Handle) : FunctionReflection.Null;

    public GenericReflection AsGeneric() => Handle != 0 ? spReflectionDecl_castToGeneric(Handle) : GenericReflection.Null;

    public DeclReflection Parent => Handle != 0 ? spReflectionDecl_getParent(Handle) : Null;

    public nint FindModifier(ModifierID id) => Handle != 0 ? spReflectionDecl_findModifier(Handle, id) : 0;

    // ---------------- IReadOnlyList Implementation ---------------- //

    public int Count => Handle != 0 ? (int)spReflectionDecl_getChildrenCount(Handle) : 0;

    public DeclReflection this[int index] => Handle != 0 ? spReflectionDecl_getChild(Handle, (uint)index) : Null;

    public IEnumerator<DeclReflection> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // ---------------- Native Imports ----------------

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static unsafe partial string spReflectionDecl_getName(nint decl);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial DeclReflectionKind spReflectionDecl_getKind(nint decl);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionDecl_getChildrenCount(nint decl);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial DeclReflection spReflectionDecl_getChild(nint decl, uint index);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial TypeReflection spReflection_getTypeFromDecl(nint decl);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial VariableReflection spReflectionDecl_castToVariable(nint decl);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial FunctionReflection spReflectionDecl_castToFunction(nint decl);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial GenericReflection spReflectionDecl_castToGeneric(nint decl);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial DeclReflection spReflectionDecl_getParent(nint decl);

    [LibraryImport(Slang.LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial nint spReflectionDecl_findModifier(nint decl, ModifierID id);
}

[CustomMarshaller(typeof(DeclReflection), MarshalMode.Default, typeof(DeclReflectionMarshaller))]
internal static class DeclReflectionMarshaller
{
    public static nint ConvertToUnmanaged(DeclReflection managed) => managed.Handle;
    public static DeclReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
