using SlangShaderSharp.Internal;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{_value}")]
public readonly partial struct DeclReflection(nint value) : IEquatable<DeclReflection>
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

    // ---------------- Methods ----------------

    public unsafe string? GetName() => spReflectionDecl_getName(_value);

    // ---------------- Native Imports ----------------

    [LibraryImport("slang")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    private static unsafe partial string spReflectionDecl_getName(nint decl);
}

[CustomMarshaller(typeof(DeclReflection), MarshalMode.Default, typeof(DeclReflectionMarshaller))]
internal static class DeclReflectionMarshaller
{
    public static nint ConvertToUnmanaged(DeclReflection managed) => managed;
    public static DeclReflection ConvertToManaged(nint unmanaged) => (DeclReflection)unmanaged;
}
