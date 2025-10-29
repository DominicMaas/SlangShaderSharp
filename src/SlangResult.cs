using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{_value}")]
public readonly struct SlangResult(int value) : IEquatable<SlangResult>
{
    private readonly int _result = value;

    /// <summary>
    ///     Use to test if a result was failure. Never use result != SLANG_OK to test for failure, as
    ///     there may be successful codes != SLANG_OK.
    /// </summary>
    public bool Failed => _result < 0;

    /// <summary>
    ///     Use to test if a result succeeded. Never use result == SLANG_OK to test for success, as will
    ///     detect other successful codes as a failure.
    /// </summary>
    public bool Succeeded => _result >= 0;

    /// <summary>
    ///     Get the facility the result is associated with
    /// </summary>
    public int GetFacility() => (_result >> 16) & 0x7fff;

    /// <summary>
    ///     Get the result code for the facility
    /// </summary>
    public int GetCode() => _result & 0xffff;

    public static implicit operator int(SlangResult value) => value._result;
    public static explicit operator SlangResult(int value) => new(value);

    public static bool operator ==(SlangResult left, SlangResult right) => left._result == right._result;
    public static bool operator !=(SlangResult left, SlangResult right) => !(left == right);

    public bool Equals(SlangResult other) => _result == other._result;
    public override bool Equals(object? obj) => obj is SlangResult other && Equals(other);
    public override int GetHashCode() => unchecked(_result);
    public override string ToString() => $"{_result}";
}

[CustomMarshaller(typeof(SlangResult), MarshalMode.Default, typeof(SlangResultMarshaller))]
public static class SlangResultMarshaller
{
    public static int ConvertToUnmanaged(SlangResult managed) => managed;
    public static SlangResult ConvertToManaged(int unmanaged) => (SlangResult)unmanaged;
}