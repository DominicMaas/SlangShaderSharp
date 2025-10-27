using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[GeneratedComInterface]
[Guid("8f241361-f5bd-4ca0-a3ac-02f7fa2402b8")]
public partial interface IEntryPoint : IComponentType
{
    [PreserveSig]
    public nint GetFunctionReflection();
}
