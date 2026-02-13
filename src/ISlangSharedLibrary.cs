using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     An interface that can be used to encapsulate access to a shared library. An implementation
///     does not have to implement the library as a shared library
/// </summary>
[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("70dbc7c4-dc3b-4a07-ae7e-752af6a81555")]
public unsafe partial interface ISlangSharedLibrary : ISlangCastable
{
    /// <summary>
    ///     Get a symbol by name. If the library is unloaded will only return nullptr.
    /// </summary>
    /// <param name="name">The name of the symbol</param>
    /// <returns>The pointer related to the name or nullptr if not found</returns>
    [PreserveSig]
    nint FindSymbolAddressByName(string name);
}
