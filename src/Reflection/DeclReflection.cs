using SlangShaderSharp.Internal;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{_value}")]
public readonly partial struct DeclReflection(nint value) : IEquatable<DeclReflection>, IReadOnlyList<DeclReflection>
{
    private readonly nint _value = value;

    public static DeclReflection Null => default;

    public static implicit operator nint(DeclReflection value) => value._value;
    public static explicit operator DeclReflection(nint value) => new(value);

    public static bool operator ==(DeclReflection left, DeclReflection right) => left._value == right._value;
    public static bool operator !=(DeclReflection left, DeclReflection right) => !(left == right);

    public bool Equals(DeclReflection other) => _value == other._value;
    public override bool Equals(object? obj) => obj is DeclReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)_value);
    public override string ToString() => $"0x{_value:x}";

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

    public unsafe string? GetName() => _value != Null ? spReflectionDecl_getName(_value) : null;

    public unsafe Kind GetKind() => _value != Null ? spReflectionDecl_getKind(_value) : Kind.Unsupported;

    // getType

    // asVariable

    // asFunction

    public FunctionReflection AsFunction => _value != Null ? spReflectionDecl_castToFunction(_value) : FunctionReflection.Null;

    // asGeneric

    public DeclReflection Parent => _value != Null ? spReflectionDecl_getParent(_value) : Null;

    // findModifier

    // ---------------- IReadOnlyList Implementation ---------------- //

    public int Count => _value != Null ? (int)spReflectionDecl_getChildrenCount(_value) : 0;

    public DeclReflection this[int index] => _value != Null ? spReflectionDecl_getChild(_value, (uint)index) : Null;

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
    public static nint ConvertToUnmanaged(DeclReflection managed) => managed;
    public static DeclReflection ConvertToManaged(nint unmanaged) => (DeclReflection)unmanaged;
}
