using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace SlangShaderSharp;

/// <summary>
///     A "blob" of binary data.
///
///     This interface definition is compatible with the `ID3DBlob` and `ID3D10Blob` interfaces.
/// </summary>
[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("8ba5fb08-5195-40e2-ac58-0d989c3a0102")]
public unsafe partial interface ISlangBlob
{
    [PreserveSig]
    void* GetBufferPointer();

    [PreserveSig]
    nuint GetBufferSize();
}

public static class ISlangBlobExtensions
{
    extension(ISlangBlob blob)
    {
        public unsafe ReadOnlySpan<byte> Buffer => new(blob.GetBufferPointer(), (int)blob.GetBufferSize());
        public unsafe string AsString => Encoding.UTF8.GetString((byte*)blob.GetBufferPointer(), (int)blob.GetBufferSize());
    }
}
