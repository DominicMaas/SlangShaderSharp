using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(VariableLayoutReflectionMarshaller))]
public readonly struct VariableLayoutReflection : IEquatable<VariableLayoutReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use VariableLayoutReflection.Null instead.")]
    public VariableLayoutReflection()
    {
        Handle = 0;
    }

    internal VariableLayoutReflection(nint value)
    {
        Handle = value;
    }

    public static VariableLayoutReflection Null => new(0);

    public static bool operator ==(VariableLayoutReflection left, VariableLayoutReflection right) => left.Handle == right.Handle;
    public static bool operator !=(VariableLayoutReflection left, VariableLayoutReflection right) => !(left == right);

    public bool Equals(VariableLayoutReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is VariableLayoutReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";
}

[CustomMarshaller(typeof(VariableLayoutReflection), MarshalMode.Default, typeof(VariableLayoutReflectionMarshaller))]
internal static class VariableLayoutReflectionMarshaller
{
    public static nint ConvertToUnmanaged(VariableLayoutReflection managed) => managed.Handle;
    public static VariableLayoutReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
