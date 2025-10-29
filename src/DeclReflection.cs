using System.Diagnostics;

namespace SlangShaderSharp;

[DebuggerDisplay("{_value}")]
public readonly struct DeclReflection(nint value) : IEquatable<DeclReflection>
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
}