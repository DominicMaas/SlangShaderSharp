using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("1ec36168-e9f4-430d-bb17-048a8046b31f")]
public unsafe partial interface ISlangClonable : ISlangCastable
{
    /// <summary>
    ///     Note the use of guid is for the desired interface/object.
    ///     The object is returned *not* ref counted. Any type that can implements the interface,
    ///     derives from ICastable, and so (not withstanding some other issue) will always return
    ///     an ICastable interface which other interfaces/types are accessible from via castAs
    /// </summary>
    [PreserveSig]
    void* Clone(Guid guid);
}
