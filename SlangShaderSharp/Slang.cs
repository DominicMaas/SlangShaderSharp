using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: DisableRuntimeMarshalling]

namespace SlangShaderSharp;

internal partial class Slang
{
    /// <summary>
    ///     Create a global session, with the built-in core module.
    /// </summary>
    /// <param name="apiVersion">Pass in SLANG_API_VERSION</param>
    /// <param name="outGlobalSession">The created global session.</param>
    [LibraryImport("slang.dll", EntryPoint = "slang_createGlobalSession")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial int CreateGlobalSession(long apiVersion, out IGlobalSession globalSession);

    /// <summary>
    ///     Cleanup all global allocations used by Slang, to prevent memory leak detectors from
    ///     reporting them as leaks. This function should only be called after all Slang objects
    ///     have been released. No other Slang functions such as `createGlobalSession`
    ///     should be called after this function.
    /// </summary>
    [LibraryImport("slang.dll", EntryPoint = "slang_shutdown")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial void Shutdown();
}