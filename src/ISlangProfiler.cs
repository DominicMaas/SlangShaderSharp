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
    string GetEntryName(uint index);

    [PreserveSig]
    long GetEntryTimeMs(uint index);

    [PreserveSig]
    uint GetEntryInvocationTimes(uint index);
}
