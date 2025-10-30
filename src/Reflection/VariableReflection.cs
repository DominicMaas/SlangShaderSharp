using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(VariableReflectionMarshaller))]
public readonly struct VariableReflection : IEquatable<VariableReflection>
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
}

[CustomMarshaller(typeof(VariableReflection), MarshalMode.Default, typeof(VariableReflectionMarshaller))]
internal static class VariableReflectionMarshaller
{
    public static nint ConvertToUnmanaged(VariableReflection managed) => managed.Handle;
    public static VariableReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
