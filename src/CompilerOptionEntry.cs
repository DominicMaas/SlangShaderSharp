using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[NativeMarshalling(typeof(CompilerOptionEntryMarshaller))]
public struct CompilerOptionEntry(CompilerOptionName name, CompilerOptionValue value)
{
    public CompilerOptionName Name = name;
    public CompilerOptionValue Value = value;
}

internal struct CompilerOptionEntryUnmanaged
{
    public CompilerOptionName name;
    public CompilerOptionValueUnmanaged value;
}

[CustomMarshaller(typeof(CompilerOptionEntry), MarshalMode.Default, typeof(CompilerOptionEntryMarshaller))]
internal static unsafe class CompilerOptionEntryMarshaller
{
    public static CompilerOptionEntryUnmanaged ConvertToUnmanaged(CompilerOptionEntry managed)
    {
        return new CompilerOptionEntryUnmanaged
        {
            name = managed.Name,
            value = CompilerOptionValueMarshaller.ConvertToUnmanaged(managed.Value),
        };
    }

    public static CompilerOptionEntryUnmanaged* ConvertToUnmanagedArray(CompilerOptionEntry[]? managed, out int count)
    {
        var container = ArrayMarshaller<CompilerOptionEntry, CompilerOptionEntryUnmanaged>.AllocateContainerForUnmanagedElements(managed, out count);

        if (managed != null)
        {
            for (var i = 0; i < count; i++)
            {
                container[i] = ConvertToUnmanaged(managed[i]);
            }
        }

        return container;
    }

    public static CompilerOptionEntry ConvertToManaged(CompilerOptionEntryUnmanaged unmanaged)
    {
        return new CompilerOptionEntry
        {
            Name = unmanaged.name,
            Value = CompilerOptionValueMarshaller.ConvertToManaged(unmanaged.value),
        };
    }

    public static CompilerOptionEntry[]? ConvertToManagedArray(CompilerOptionEntryUnmanaged* unmanaged, int count)
    {
        var container = ArrayMarshaller<CompilerOptionEntry, CompilerOptionEntryUnmanaged>.AllocateContainerForManagedElements(unmanaged, count);
        if (container == null)
        {
            return null;
        }

        for (var i = 0; i < count; i++)
        {
            container[i] = ConvertToManaged(unmanaged[i]);
        }

        return container;
    }

    public static void Free(CompilerOptionEntryUnmanaged unmanaged)
    {
        CompilerOptionValueMarshaller.Free(unmanaged.value);
    }

    public static void FreeArray(CompilerOptionEntryUnmanaged* unmanaged, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Free(unmanaged[i]);
        }

        ArrayMarshaller<CompilerOptionEntry, CompilerOptionEntryUnmanaged>.Free(unmanaged);
    }
}
