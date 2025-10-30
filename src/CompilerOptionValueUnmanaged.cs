using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[NativeMarshalling(typeof(CompilerOptionValueMarshaller))]
public struct CompilerOptionValue
{
    public CompilerOptionValueKind Kind;
    public int IntValue0;
    public int IntValue1;
    public string? StringValue0;
    public string? StringValue1;
}

// TODO: Make internal
public unsafe struct CompilerOptionValueUnmanaged
{
    public CompilerOptionValueKind kind;
    public int intValue0;
    public int intValue1;
    public byte* stringValue0;
    public byte* stringValue1;
}

[CustomMarshaller(typeof(CompilerOptionValue), MarshalMode.Default, typeof(CompilerOptionValueMarshaller))]
internal static unsafe class CompilerOptionValueMarshaller
{
    public static CompilerOptionValueUnmanaged ConvertToUnmanaged(CompilerOptionValue managed)
    {
        var str0Ptr = Utf8StringMarshaller.ConvertToUnmanaged(managed.StringValue0);
        var str1Ptr = Utf8StringMarshaller.ConvertToUnmanaged(managed.StringValue1);
        return new CompilerOptionValueUnmanaged
        {
            kind = managed.Kind,
            intValue0 = managed.IntValue0,
            intValue1 = managed.IntValue1,
            stringValue0 = str0Ptr,
            stringValue1 = str1Ptr
        };
    }

    public static CompilerOptionValue ConvertToManaged(CompilerOptionValueUnmanaged unmanaged)
    {
        return new CompilerOptionValue
        {
            Kind = unmanaged.kind,
            IntValue0 = unmanaged.intValue0,
            IntValue1 = unmanaged.intValue1,
            StringValue0 = Utf8StringMarshaller.ConvertToManaged(unmanaged.stringValue0),
            StringValue1 = Utf8StringMarshaller.ConvertToManaged(unmanaged.stringValue1),
        };
    }

    public static void Free(CompilerOptionValueUnmanaged unmanaged)
    {
        Utf8StringMarshaller.Free(unmanaged.stringValue0);
        Utf8StringMarshaller.Free(unmanaged.stringValue1);
    }
}