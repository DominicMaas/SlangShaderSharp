using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(TypeReflectionMarshaller))]
public readonly struct TypeReflection : IEquatable<TypeReflection>
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
}

[CustomMarshaller(typeof(TypeReflection), MarshalMode.Default, typeof(TypeReflectionMarshaller))]
internal static class TypeReflectionMarshaller
{
    public static nint ConvertToUnmanaged(TypeReflection managed) => managed.Handle;
    public static TypeReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}