using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp.Internal;

/// <summary>
///     Used in places where the COM interface should not be freed during internal source generated marshaling
/// </summary>
/// <typeparam name="T"></typeparam>
[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.Default, typeof(NoFreeComInterfaceMarshaller<>))]
internal static unsafe class NoFreeComInterfaceMarshaller<T> where T : class
{
    public static void* ConvertToUnmanaged(T? managed) => ComInterfaceMarshaller<T>.ConvertToUnmanaged(managed);

    public static T? ConvertToManaged(void* unmanaged) => ComInterfaceMarshaller<T>.ConvertToManaged(unmanaged);

    public static void Free(void* unmanaged) { }
}