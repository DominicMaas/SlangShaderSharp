using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[NativeMarshalling(typeof(ByteCodeRunnerDescMarshaller))]
public struct ByteCodeRunnerDesc
{ }

[StructLayout(LayoutKind.Sequential)]
internal struct ByteCodeRunnerDescUnmanaged
{
    public nuint structSize;
}

[CustomMarshaller(typeof(ByteCodeRunnerDesc), MarshalMode.Default, typeof(ByteCodeRunnerDescMarshaller))]
internal static unsafe class ByteCodeRunnerDescMarshaller
{
    public static ByteCodeRunnerDescUnmanaged ConvertToUnmanaged(ByteCodeRunnerDesc managed)
    {
        return new ByteCodeRunnerDescUnmanaged
        {
            structSize = (nuint)sizeof(ByteCodeRunnerDescUnmanaged)
        };
    }

    public static ByteCodeRunnerDesc ConvertToManaged(ByteCodeRunnerDescUnmanaged unmanaged)
    {
        return new ByteCodeRunnerDesc();
    }

    public static void Free(ByteCodeRunnerDescUnmanaged unmanaged)
    {
    }
}
