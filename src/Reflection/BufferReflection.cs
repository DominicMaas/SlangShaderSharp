using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(BufferReflectionMarshaller))]
public readonly struct BufferReflection : IEquatable<BufferReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use BufferReflection.Null instead.")]
    public BufferReflection()
    {
        Handle = 0;
    }

    internal BufferReflection(nint value)
    {
        Handle = value;
    }

    public static BufferReflection Null => new(0);

    public static bool operator ==(BufferReflection left, BufferReflection right) => left.Handle == right.Handle;
    public static bool operator !=(BufferReflection left, BufferReflection right) => !(left == right);

    public bool Equals(BufferReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is BufferReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // ---------------- Public Interface ---------------- //

    // ---------------- Native Imports ---------------- //
}

[CustomMarshaller(typeof(BufferReflection), MarshalMode.Default, typeof(BufferReflectionMarshaller))]
internal static class BufferReflectionMarshaller
{
    public static nint ConvertToUnmanaged(BufferReflection managed) => managed.Handle;
    public static BufferReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
