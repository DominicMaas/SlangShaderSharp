using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("8f241361-f5bd-4ca0-a3ac-02f7fa2402b8")]
public unsafe partial interface IEntryPoint : IComponentType
{
    [PreserveSig]
    FunctionReflection GetFunctionReflection();
}
