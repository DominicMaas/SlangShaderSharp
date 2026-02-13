using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[StructLayout(LayoutKind.Sequential)]
public struct ByteCodeFuncInfo
{
    public uint ParameterCount;
    public uint ReturnValueSize;
}

public enum OperandDataType
{
    General = 0, // General data type, can be any type.
    Int32 = 1,   // 32-bit integer.
    Int64 = 2,   // 64-bit integer.
    Float32 = 3, // 32-bit floating-point number.
    Float64 = 4, // 64-bit floating-point number.
    String = 5,  // String data type, represented as a pointer to a null-terminated string.
}

[StructLayout(LayoutKind.Explicit, Size = 16)]
public unsafe struct VMExecOperand
{
    // uint8_t** section;
    [FieldOffset(0)]
    public byte** Section;

    // uint32_t type : 8;
    // uint32_t size : 24;
    [FieldOffset(8)]
    private uint _typeAndSize;

    [FieldOffset(12)]
    public uint Offset;

    public OperandDataType Type
    {
        readonly get => (OperandDataType)(_typeAndSize & 0xFF);
        set => _typeAndSize = (_typeAndSize & 0xFFFFFF00) | ((uint)value & 0xFF);
    }

    public uint Size
    {
        readonly get => _typeAndSize >> 8;
        set => _typeAndSize = (_typeAndSize & 0xFF) | ((value & 0xFFFFFF) << 8);
    }

    public void* GetPtr() => *Section + Offset;
}

[StructLayout(LayoutKind.Explicit, Size = 16)]
public unsafe struct VMExecInstHeader
{
    // VMExtFunction functionPtr;
    [FieldOffset(0)]
    public delegate* unmanaged[Cdecl]<IByteCodeRunner, VMExecInstHeader*, void*, void> FunctionPtr;

    [FieldOffset(8)]
    public uint OpcodeExtension;

    [FieldOffset(12)]
    public uint OperandCount;

    public VMExecInstHeader* GetNextInst()
    {
        fixed (VMExecInstHeader* ptr = &this)
        {
            var operands = (VMExecOperand*)(ptr + 1);
            return (VMExecInstHeader*)(operands + OperandCount);
        }
    }

    public VMExecOperand* GetOperand(int index)
    {
        fixed (VMExecInstHeader* ptr = &this)
        {
            var operands = (VMExecOperand*)(ptr + 1);
            return &operands[index];
        }
    }
}

/// <summary>
///     Represents a byte code runner that can execute Slang byte code.
/// </summary>
[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("afdab195-361f-42cb-9513-9006261DD8CD")]
public unsafe partial interface IByteCodeRunner
{
    /// <summary>
    ///     Load a byte code module into the execution context.
    /// </summary>
    [PreserveSig]
    SlangResult LoadModule(ISlangBlob moduleBlob);

    /// <summary>
    ///     Select a function for execution.
    /// </summary>
    [PreserveSig]
    SlangResult SelectFunctionByIndex(uint functionIndex);

    [PreserveSig]
    int FindFunctionByName(string name);

    [PreserveSig]
    SlangResult GetFunctionInfo(
        uint index,
        out ByteCodeFuncInfo outInfo);

    /// <summary>
    ///     Obtain the current working set memory for the selected function.
    /// </summary>
    [PreserveSig]
    void* GetCurrentWorkingSet();

    /// <summary>
    ///     Execute the selected function.
    /// </summary>
    [PreserveSig]
    SlangResult Execute(
        void* argumentData,
        nuint argumentSize);

    /// <summary>
    ///      Query the error string.
    /// </summary>
    [PreserveSig]
    void GetErrorString(out ISlangBlob outBlob);

    /// <summary>
    ///      Retrieve the return value of the last executed function.
    /// </summary>
    [PreserveSig]
    void* GetReturnValue(out nuint outValueSize);

    /// <summary>
    ///     Set the user data for the external instruction handler.
    /// </summary>
    [PreserveSig]
    void SetExtInstHandlerUserData(void* userData);

    /// <summary>
    ///     Register an external function that can be called from the byte code.
    /// </summary>
    [PreserveSig]
    SlangResult RegisterExtCall(
        string name,
        delegate* unmanaged[Cdecl]<IByteCodeRunner, VMExecInstHeader*, void*, void> functionPtr);

    /// <summary>
    ///     Set a callback function to print messages from the byte code runner.
    /// </summary>
    [PreserveSig]
    SlangResult SetPrintCallback(
        delegate* unmanaged[Cdecl]<char*, void*, void> callback,
        void* userData);
}
