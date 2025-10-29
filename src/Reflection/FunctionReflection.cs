using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{_value}")]
public readonly struct FunctionReflection(nint value) : IEquatable<FunctionReflection>
{
    private readonly nint _value = value;

    public static FunctionReflection Null => default;

    public static implicit operator nint(FunctionReflection value) => value._value;
    public static explicit operator FunctionReflection(nint value) => new(value);

    public static bool operator ==(FunctionReflection left, FunctionReflection right) => left._value == right._value;
    public static bool operator !=(FunctionReflection left, FunctionReflection right) => !(left == right);

    public bool Equals(FunctionReflection other) => _value == other._value;
    public override bool Equals(object? obj) => obj is FunctionReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)_value);
    public override string ToString() => $"0x{_value:x}";
}

[CustomMarshaller(typeof(FunctionReflection), MarshalMode.Default, typeof(FunctionReflectionMarshaller))]
internal static class FunctionReflectionMarshaller
{
    public static nint ConvertToUnmanaged(FunctionReflection managed) => managed;
    public static FunctionReflection ConvertToManaged(nint unmanaged) => (FunctionReflection)unmanaged;
}

