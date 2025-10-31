using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{_result}")]
[NativeMarshalling(typeof(SlangResultMarshaller))]
public readonly struct SlangResult(int value) : IEquatable<SlangResult>
{
    // Facilities

    private const int SLANG_FACILITY_WIN_GENERAL = 0;
    private const int SLANG_FACILITY_WIN_API = 7;

    private const int SLANG_FACILITY_BASE = 0x200;
    private const int SLANG_FACILITY_CORE = SLANG_FACILITY_BASE;

    // Static error/success creators

    /// <summary>
    ///     Create an error result from a facility and code
    /// </summary>
    private static SlangResult MakeError(int facility, int code) => new((facility << 16) | code | unchecked((int)0x80000000));

    private static SlangResult MakeCoreError(int code) => MakeError(SLANG_FACILITY_CORE, code);

    private static SlangResult MakeWinGeneralError(int code) => MakeError(SLANG_FACILITY_WIN_GENERAL, code);

    // Windows COM compatible Results

    /// <summary>
    ///     OK Result
    /// </summary>
    public static readonly SlangResult SLANG_OK = new(0);

    /// <summary>
    ///      SLANG_FAIL is the generic failure code - meaning a serious error occurred and the call
    ///      couldn't complete
    /// </summary>
    public static readonly SlangResult SLANG_FAIL = MakeError(SLANG_FACILITY_WIN_GENERAL, 0x4005);

    /// <summary>
    ///     Functionality is not implemented
    /// </summary>
    public static readonly SlangResult SLANG_E_NOT_IMPLEMENTED = MakeWinGeneralError(0x4001);

    /// <summary>
    ///     Interface not be found
    /// </summary>
    public static readonly SlangResult SLANG_E_NO_INTERFACE = MakeWinGeneralError(0x4002);

    /// <summary>
    ///     Operation was aborted (did not correctly complete)
    /// </summary>
    public static readonly SlangResult SLANG_E_ABORT = MakeWinGeneralError(0x4004);

    /// <summary>
    ///     Indicates that a handle passed in as parameter to a method is invalid.
    /// </summary>
    public static readonly SlangResult SLANG_E_INVALID_HANDLE = MakeError(SLANG_FACILITY_WIN_API, 6);

    /// <summary>
    ///     Indicates that an argument passed in as parameter to a method is invalid.
    /// </summary>
    public static readonly SlangResult SLANG_E_INVALID_ARG = MakeError(SLANG_FACILITY_WIN_API, 0x57);

    /// <summary>
    ///     Operation could not complete - ran out of memory
    /// </summary>
    public static readonly SlangResult SLANG_E_OUT_OF_MEMORY = MakeError(SLANG_FACILITY_WIN_API, 0xe);

    // Other Results

    /// <summary>
    ///     Supplied buffer is too small to be able to complete
    /// </summary>
    public static readonly SlangResult SLANG_E_BUFFER_TOO_SMALL = MakeCoreError(1);

    /// <summary>
    ///     Used to identify a Result that has yet to be initialized.
    ///     It defaults to failure such that if used incorrectly will fail, as similar in concept to
    ///     using an uninitialized variable.
    /// </summary>
    public static readonly SlangResult SLANG_E_UNINITIALIZED = MakeCoreError(2);

    /// <summary>
    ///     Returned from an async method meaning the output is invalid (thus an error), but a result
    ///     for the request is pending, and will be returned on a subsequent call with the async handle.
    /// </summary>
    public static readonly SlangResult SLANG_E_PENDING = MakeCoreError(3);

    /// <summary>
    ///     Indicates a file/resource could not be opened
    /// </summary>
    public static readonly SlangResult SLANG_E_CANNOT_OPEN = MakeCoreError(4);

    /// <summary>
    ///     Indicates a file/resource could not be found
    /// </summary>
    public static readonly SlangResult SLANG_E_NOT_FOUND = MakeCoreError(5);

    /// <summary>
    ///     An unhandled internal failure (typically from unhandled exception)
    /// </summary>
    public static readonly SlangResult SLANG_E_INTERNAL_FAIL = MakeCoreError(6);

    /// <summary>
    ///     Could not complete because some underlying feature (hardware or software) was not available
    /// </summary>
    public static readonly SlangResult SLANG_E_NOT_AVAILABLE = MakeCoreError(7);

    /// <summary>
    ///     Could not complete because the operation times out.
    /// </summary>
    public static readonly SlangResult SLANG_E_TIME_OUT = MakeCoreError(8);

    // Result Fields

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
internal static class SlangResultMarshaller
{
    public static int ConvertToUnmanaged(SlangResult managed) => managed;
    public static SlangResult ConvertToManaged(int unmanaged) => (SlangResult)unmanaged;
}
