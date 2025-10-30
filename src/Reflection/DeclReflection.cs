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

    // ---------------- Extra ---------------- //

    public enum Kind : uint
    {
        Unsupported = 0,
        Struct = 1,
        Func = 2,
        Module = 3,
        Generic = 4,
        Variable = 5,
        Namespace = 6
    }

    // ---------------- Methods ---------------- //

    public unsafe string? GetName() => Handle != 0 ? spReflectionDecl_getName(Handle) : null;

    public unsafe Kind GetKind() => Handle != 0 ? spReflectionDecl_getKind(Handle) : Kind.Unsupported;

    // getType

    // asVariable

    // asFunction

    public FunctionReflection AsFunction => Handle != 0 ? spReflectionDecl_castToFunction(Handle) : FunctionReflection.Null;

    // asGeneric

    public DeclReflection Parent => Handle != 0 ? spReflectionDecl_getParent(Handle) : Null;

    // findModifier

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

    [LibraryImport("slang")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static unsafe partial string spReflectionDecl_getName(nint decl);

    [LibraryImport("slang")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial Kind spReflectionDecl_getKind(nint decl);

    [LibraryImport("slang")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static partial uint spReflectionDecl_getChildrenCount(nint decl);

    [LibraryImport("slang")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(DeclReflectionMarshaller))]
    private static partial DeclReflection spReflectionDecl_getChild(nint decl, uint index);

    // getType

    // asVariable

    [LibraryImport("slang")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(FunctionReflectionMarshaller))]
    private static partial FunctionReflection spReflectionDecl_castToFunction(nint decl);

    // asFunction

    // asGeneric

    [LibraryImport("slang")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(DeclReflectionMarshaller))]
    private static partial DeclReflection spReflectionDecl_getParent(nint decl);

    // findModifier

}

[CustomMarshaller(typeof(DeclReflection), MarshalMode.Default, typeof(DeclReflectionMarshaller))]
internal static class DeclReflectionMarshaller
{
    public static nint ConvertToUnmanaged(DeclReflection managed) => managed.Handle;
    public static DeclReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
