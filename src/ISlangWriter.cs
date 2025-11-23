using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     A stream typically of text, used for outputting diagnostic as well as other information.
/// </summary>
[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("ec457f0e-9add-4e6b-851c-d7fa716d15fd")]
public partial interface ISlangWriter
{
    /// <summary>
    ///     Begin an append buffer.
    ///     NOTE! Only one append buffer can be active at any time.
    /// </summary>
    /// <param name="maxNumChars">The maximum of chars that will be appended</param>
    /// <returns>The start of the buffer for appending to.</returns>
    [PreserveSig]
    unsafe byte* BeginAppendBuffer(nuint maxNumChars);

    /// <summary>
    ///     Ends the append buffer, and is equivalent to a write of the append buffer.
    ///     NOTE! That an EndAppendBuffer is not necessary if there are no characters to write.
    /// </summary>
    /// <param name="buffer">buffer is the start of the data to append and must be identical to last value returned from BeginAppendBuffer</param>
    /// <param name="numChars">must be a value less than or equal to what was returned from last call to BeginAppendBuffer</param>
    /// <returns>Result, will be SLANG_OK on success</returns>
    [PreserveSig]
    unsafe SlangResult EndAppendBuffer(
        byte* buffer,
        nuint numChars);

    /// <summary>
    ///     Write text to the writer
    /// </summary>
    /// <param name="chars">The characters to write out</param>
    /// <param name="numChars">The amount of characters</param>
    /// <returns>SLANG_OK on success</returns>
    [PreserveSig]
    unsafe SlangResult Write(
        byte* chars,
        nuint numChars);

    /// <summary>
    ///     Flushes any content to the output
    /// </summary>
    [PreserveSig]
    void Flush();

    /// <summary>
    ///     Determines if the writer stream is to the console, and can be used to alter the output
    /// </summary>
    /// <returns>Returns true if is a console writer</returns>
    [PreserveSig]
    [return: MarshalAs(UnmanagedType.I1)]
    bool IsConsole();

    /// <summary>
    ///     Set the mode for the writer to use
    /// </summary>
    /// <param name="mode">The mode to use</param>
    /// <returns>SLANG_OK on success</returns>
    [PreserveSig]
    SlangResult SetMode(SlangWriterMode mode);
}
