using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     An interface to provide a mechanism to cast, that doesn't require ref counting
///     and doesn't have to return a pointer to a ISlangUnknown derived class
/// </summary>
[GeneratedComInterface]
[Guid("87ede0e1-4852-44b0-8bf2-cb31874de239")]
public partial interface ISlangCastable
{
    /// <summary>
    ///     Can be used to cast to interfaces without reference counting.
    ///     Also provides access to internal implementations, when they provide a guid
    ///     Can simulate a 'generated' interface as long as kept in scope by cast from.
    /// </summary>
    [PreserveSig]
    unsafe void* CastAs(Guid guid);
}
