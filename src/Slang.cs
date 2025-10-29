using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

[assembly: DisableRuntimeMarshalling]

namespace SlangShaderSharp;

internal partial class Slang
{
    [LibraryImport("slang", EntryPoint = "slang_createBlob")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static unsafe partial ISlangBlob CreateBlob(void* data, nuint size);

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
    ///     Create a blob from binary data.
    /// </summary>
    /// <param name="data">Binary data to store in the blob. Must not be null or empty.</param>
    /// <returns>The created blob on success, or nullptr on failure.</returns>
    public static unsafe ISlangBlob CreateBlob(string data) => CreateBlob(Encoding.UTF8.GetBytes(data));

    /// <summary>
    ///     Create a global session, with the built-in core module.
    /// </summary>
    /// <param name="apiVersion">Pass in SLANG_API_VERSION</param>
    /// <param name="globalSession">The created global session.</param>
    [LibraryImport("slang", EntryPoint = "slang_createGlobalSession")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    public static partial SlangResult CreateGlobalSession(int apiVersion, out IGlobalSession globalSession);

    /// <summary>
    ///     Create a global session, with the built-in core module.
    /// </summary>
    /// <param name="desc">Description of the global session.</param>
    /// <param name="globalSession">The created global session.</param>
    [LibraryImport("slang", EntryPoint = "slang_createGlobalSession2")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    public static partial SlangResult CreateGlobalSession2(SlangGlobalSessionDesc desc, out IGlobalSession globalSession);

    /// <summary>
    ///     Create a global session, but do not set up the core module. The core module can
    ///     then be loaded via loadCoreModule or compileCoreModule
    /// </summary>
    /// <param name="apiVersion">Pass in SLANG_API_VERSION</param>
    /// <param name="globalSession">The created global session that doesn't have a core module setup.</param>
    [LibraryImport("slang", EntryPoint = "slang_createGlobalSessionWithoutCoreModule")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    public static partial SlangResult CreateGlobalSessionWithoutCoreModule(int apiVersion, out IGlobalSession globalSession);

    /// <summary>
    ///      Returns a blob that contains the serialized core module.
    ///      Returns nullptr if there isn't an embedded core module.
    ///
    ///     NOTE! API is experimental and not ready for production code
    /// </summary>
    [LibraryImport("slang", EntryPoint = "slang_getEmbeddedCoreModule")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial ISlangBlob GetEmbeddedCoreModule();

    /// <summary>
    ///     Cleanup all global allocations used by Slang, to prevent memory leak detectors from
    ///     reporting them as leaks. This function should only be called after all Slang objects
    ///     have been released. No other Slang functions such as `createGlobalSession`
    ///     should be called after this function.
    /// </summary>
    [LibraryImport("slang", EntryPoint = "slang_shutdown")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial void Shutdown();

    /// <summary>
    ///     Return the last signaled internal error message.
    /// </summary>
    [LibraryImport("slang", EntryPoint = "slang_getLastInternalErrorMessage")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.LPUTF8Str)]
    public static partial string GetLastInternalErrorMessage();
}