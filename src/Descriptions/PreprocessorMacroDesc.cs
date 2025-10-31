using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[NativeMarshalling(typeof(PreprocessorMacroDescMarshaller))]
public struct PreprocessorMacroDesc
{
    public string? Name;
    public string? Value;
}

internal unsafe struct PreprocessorMacroDescUnmanaged
{
    public byte* name;
    public byte* value;
}

[CustomMarshaller(typeof(PreprocessorMacroDesc), MarshalMode.Default, typeof(PreprocessorMacroDescMarshaller))]
internal static unsafe class PreprocessorMacroDescMarshaller
{
    public static PreprocessorMacroDescUnmanaged ConvertToUnmanaged(PreprocessorMacroDesc managed)
    {
        var namePtr = Utf8StringMarshaller.ConvertToUnmanaged(managed.Name);
        var valuePtr = Utf8StringMarshaller.ConvertToUnmanaged(managed.Value);
        return new PreprocessorMacroDescUnmanaged
        {
            name = namePtr,
            value = valuePtr
        };
    }

    public static PreprocessorMacroDescUnmanaged* ConvertToUnmanagedArray(PreprocessorMacroDesc[]? managed, out int count)
    {
        var container = ArrayMarshaller<PreprocessorMacroDesc, PreprocessorMacroDescUnmanaged>.AllocateContainerForUnmanagedElements(managed, out count);

        if (managed != null)
        {
            for (var i = 0; i < count; i++)
            {
                container[i] = ConvertToUnmanaged(managed[i]);
            }
        }

        return container;
    }

    public static PreprocessorMacroDesc ConvertToManaged(PreprocessorMacroDescUnmanaged unmanaged)
    {
        var name = Utf8StringMarshaller.ConvertToManaged(unmanaged.name);
        var value = Utf8StringMarshaller.ConvertToManaged(unmanaged.value);
        return new PreprocessorMacroDesc
        {
            Name = name,
            Value = value
        };
    }

    public static PreprocessorMacroDesc[]? ConvertToManagedArray(PreprocessorMacroDescUnmanaged* unmanaged, int count)
    {
        var container = ArrayMarshaller<PreprocessorMacroDesc, PreprocessorMacroDescUnmanaged>.AllocateContainerForManagedElements(unmanaged, count);
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

    public static void Free(PreprocessorMacroDescUnmanaged unmanaged)
    {
        Utf8StringMarshaller.Free(unmanaged.name);
        Utf8StringMarshaller.Free(unmanaged.value);
    }

    public static void FreeArray(PreprocessorMacroDescUnmanaged* unmanaged, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Free(unmanaged[i]);
        }

        ArrayMarshaller<PreprocessorMacroDesc, PreprocessorMacroDescUnmanaged>.Free(unmanaged);
    }
}