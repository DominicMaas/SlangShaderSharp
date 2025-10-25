using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     A session provides a scope for code that is loaded.
///
///     A session can be used to load modules of Slang source code,
///     and to request target-specific compiled binaries and layout
///     information.
///
///     In order to be able to load code, the session owns a set
///     of active "search paths" for resolving `#include` directives
///     and `import` declarations, as well as a set of global
///     preprocessor definitions that will be used for all code
///     that gets `import`ed in the session.
///
///     If multiple user shaders are loaded in the same session,
///     and import the same module (e.g., two source files do `import X`)
///     then there will only be one copy of `X` loaded within the session.
///
///     In order to be able to generate target code, the session
///     owns a list of available compilation targets, which specify
///     code generation options.
///
///     Code loaded and compiled within a session is owned by the session
///     and will remain resident in memory until the session is released.
///     Applications wishing to control the memory usage for compiled
///     and loaded code should use multiple sessions.
/// </summary>
[GeneratedComInterface]
[Guid("67618701-d116-468f-ab3b-474bedce0e3d")]
public partial interface ISession
{
    /// <summary>
    ///     Get the global session thas was used to create this session.
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    public IGlobalSession GetGlobalSession();
}
