using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     A global session for interaction with the Slang library.
///
///     An application may create and re-use a single global session across
///     multiple sessions, in order to amortize startups costs (in current
///     Slang this is mostly the cost of loading the Slang standard library).
///
///     The global session is currently *not* thread-safe and objects created from
///     a single global session should only be used from a single thread at
///     a time.
/// </summary>
[GeneratedComInterface]
[Guid("c140b5fd-0c78-452e-ba7c-1a1e70c7f71c")]
public partial interface IGlobalSession
{
    /// <summary>
    ///     Create a new session for loading and compiling code.
    /// </summary>
    [PreserveSig]
    int CreateSession(SessionDesc description, out ISession sesion);

    /// <summary>
    ///     Look up the internal ID of a profile by its `name`.
    ///
    ///     Profile IDs are *not* guaranteed to be stable across versions
    ///     of the Slang library, so clients are expected to look up
    ///     profiles by name at runtime.
    /// </summary>
    [PreserveSig]
    SlangProfileID FindProfile([MarshalAs(UnmanagedType.LPStr)] string name);

    //nint IModule LoadModule([MarshalAs(UnmanagedType.LPStr)] string moduleName);
}
