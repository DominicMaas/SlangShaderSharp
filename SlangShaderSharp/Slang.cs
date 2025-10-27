using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: DisableRuntimeMarshalling]

namespace SlangShaderSharp;

internal partial class Slang
{
    /// <summary>
    ///     Create a blob from binary data.
    /// </summary>
    /// <param name="data">Pointer to the binary data to store in the blob. Must not be null.</param>
    /// <param name="size">Size of the data in bytes. Must be greater than 0.</param>
    /// <returns>The created blob on success, or nullptr on failure.</returns>
    [LibraryImport("slang.dll", EntryPoint = "slang_createBlob")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static unsafe partial ISlangBlob CreateBlob(void* data, nuint size);

    /// <summary>
    ///     Create a blob from binary data.
    /// </summary>
    /// <param name="data">Binary data to store in the blob. Must not be null or empty.</param>
    /// <returns>The created blob on success, or nullptr on failure.</returns>
    public static unsafe ISlangBlob CreateBlob(ReadOnlySpan<byte> data)
    {
        // CreateBlob takes a copy of the data so it's safe to drop this at the end of the method
        fixed (void* p = data)
        {
            return CreateBlob(p, (nuint)data.Length);
        }
    }

    /// <summary>
    ///     Create a global session, with the built-in core module.
    /// </summary>
    /// <param name="apiVersion">Pass in SLANG_API_VERSION</param>
    /// <param name="globalSession">The created global session.</param>
    [LibraryImport("slang.dll", EntryPoint = "slang_createGlobalSession")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial int CreateGlobalSession(int apiVersion, out IGlobalSession globalSession);

    /// <summary>
    ///     Create a global session, with the built-in core module.
    /// </summary>
    /// <param name="desc">Description of the global session.</param>
    /// <param name="globalSession">The created global session.</param>
    [LibraryImport("slang.dll", EntryPoint = "slang_createGlobalSession2")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial int CreateGlobalSession2(SlangGlobalSessionDesc desc, out IGlobalSession globalSession);

    /// <summary>
    ///     Create a global session, but do not set up the core module. The core module can
    ///     then be loaded via loadCoreModule or compileCoreModule
    /// </summary>
    /// <param name="apiVersion">Pass in SLANG_API_VERSION</param>
    /// <param name="globalSession">The created global session that doesn't have a core module setup.</param>
    [LibraryImport("slang.dll", EntryPoint = "slang_createGlobalSessionWithoutCoreModule")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial int CreateGlobalSessionWithoutCoreModule(int apiVersion, out IGlobalSession globalSession);

    /// <summary>
    ///      Returns a blob that contains the serialized core module.
    ///      Returns nullptr if there isn't an embedded core module.
    ///
    ///     NOTE! API is experimental and not ready for production code
    /// </summary>
    [LibraryImport("slang.dll", EntryPoint = "slang_getEmbeddedCoreModule")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial nint GetEmbeddedCoreModule();

    /// <summary>
    ///     Cleanup all global allocations used by Slang, to prevent memory leak detectors from
    ///     reporting them as leaks. This function should only be called after all Slang objects
    ///     have been released. No other Slang functions such as `createGlobalSession`
    ///     should be called after this function.
    /// </summary>
    [LibraryImport("slang.dll", EntryPoint = "slang_shutdown")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial void Shutdown();

    /// <summary>
    ///     Return the last signaled internal error message.
    /// </summary>
    [LibraryImport("slang.dll", EntryPoint = "slang_getLastInternalErrorMessage")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.LPUTF8Str)]
    public static partial string GetLastInternalErrorMessage();
}