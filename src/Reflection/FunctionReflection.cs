using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(FunctionReflectionMarshaller))]
public readonly struct FunctionReflection : IEquatable<FunctionReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use FunctionReflection.Null instead.")]
    public FunctionReflection()
    {
        Handle = 0;
    }

    internal FunctionReflection(nint value)
    {
        Handle = value;
    }

    public static FunctionReflection Null => new(0);

    public static bool operator ==(FunctionReflection left, FunctionReflection right) => left.Handle == right.Handle;
    public static bool operator !=(FunctionReflection left, FunctionReflection right) => !(left == right);

    public bool Equals(FunctionReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is FunctionReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";
}

[CustomMarshaller(typeof(FunctionReflection), MarshalMode.Default, typeof(FunctionReflectionMarshaller))]
internal static class FunctionReflectionMarshaller
{
    public static nint ConvertToUnmanaged(FunctionReflection managed) => managed.Handle;
    public static FunctionReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}

