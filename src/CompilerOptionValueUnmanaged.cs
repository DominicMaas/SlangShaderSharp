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

    public static CompilerOptionValue CreateInt(int value0, int? value1 = null)
    {
        return new CompilerOptionValue
        {
            Kind = CompilerOptionValueKind.Int,
            IntValue0 = value0,
            IntValue1 = value1 ?? 0
        };
    }

    public static CompilerOptionValue CreateString(string value0, string? value1 = null)
    {
        return new CompilerOptionValue
        {
            Kind = CompilerOptionValueKind.String,
            StringValue0 = value0,
            StringValue1 = value1
        };
    }


}

internal unsafe struct CompilerOptionValueUnmanaged
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