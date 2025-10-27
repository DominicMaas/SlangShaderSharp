using System.Diagnostics;

namespace SlangShaderSharp;

[DebuggerDisplay("{_value}")]
public readonly struct ShaderReflection(nint value) : IEquatable<ShaderReflection>
{
    private readonly nint _value = value;

    public static ShaderReflection Null => default;

    public static implicit operator nint(ShaderReflection value) => value._value;
    public static explicit operator ShaderReflection(nint value) => new(value);

    public static bool operator ==(ShaderReflection left, ShaderReflection right) => left._value == right._value;
    public static bool operator !=(ShaderReflection left, ShaderReflection right) => !(left == right);

    public bool Equals(ShaderReflection other) => _value == other._value;
    public override bool Equals(object? obj) => obj is ShaderReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)_value);
    public override string ToString() => $"0x{_value:x}";
}
