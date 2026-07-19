using SlangShaderSharp.Internal;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("197772c7-0155-4b91-84e8-6668baff0619")]
public partial interface ISlangProfiler
{
    [PreserveSig]
    nuint GetEntryCount();

    [PreserveSig]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    string GetEntryName(uint index);

    // Native returns C `long`, which is 32-bit on Windows (MSVC) and 64-bit on
    // LP64 (Linux/macOS). `int` reads the low register and is correct on all: on
    // Windows it matches the 32-bit return, elsewhere the millisecond value fits in 32 bits.
    [PreserveSig]
    int GetEntryTimeMs(uint index);

    [PreserveSig]
    uint GetEntryInvocationTimes(uint index);
}
