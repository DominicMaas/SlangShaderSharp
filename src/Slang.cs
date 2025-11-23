using SlangShaderSharp.Internal;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

[assembly: DisableRuntimeMarshalling]

namespace SlangShaderSharp;

public partial class Slang
{
    internal const string LibraryName = "slang-compiler";

    public const int ApiVersion = 0;

    [LibraryImport(LibraryName, EntryPoint = "slang_createBlob", StringMarshalling = StringMarshalling.Utf8)]
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
    ///     Load a module from source code with size specification.
    /// </summary>
    /// <param name="session">The session to load the module into.</param>
    /// <param name="moduleName">The name of the module.</param>
    /// <param name="path">The path for the module.</param>
    /// <param name="source">Pointer to the source code data.</param>
    /// <param name="sourceSize">Size of the source code data in bytes.</param>
    /// <param name="diagnostics">Diagnostics output.</param>
    /// <returns>The loaded module on success, or nullptr on failure.</returns>
    [LibraryImport(LibraryName, EntryPoint = "slang_loadModuleFromSource", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial IModule? LoadModuleFromSource(IGlobalSession session, string moduleName, string path, ReadOnlySpan<byte> source, nuint sourceSize, out ISlangBlob? diagnostics);

    /// <summary>
    ///     Load a module from IR data.
    /// </summary>
    /// <param name="session">The session to load the module into.</param>
    /// <param name="moduleName">The name of the module.</param>
    /// <param name="path">Path for the module (used for diagnostics).</param>
    /// <param name="source">IR data containing the module.</param>
    /// <param name="sourceSize">Size of the IR data in bytes.</param>
    /// <param name="diagnostics">Diagnostics output.</param>
    /// <returns>The loaded module on success, or nullptr on failure.</returns>
    [LibraryImport(LibraryName, EntryPoint = "slang_loadModuleFromIRBlob", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial IModule? LoadModuleFromIRBlob(IGlobalSession session, string moduleName, string path, ReadOnlySpan<byte> source, nuint sourceSize, out ISlangBlob? diagnostics);

    /// <summary>
    ///     Read module info (name and version) from IR data.
    /// </summary>
    /// <param name="session">The session to use for loading module info.</param>
    /// <param name="source">IR data containing the module.</param>
    /// <param name="sourceSize">Size of the IR data in bytes.</param>
    /// <param name="moduleVersion">Module version number.</param>
    /// <param name="moduleCompilerVersion">Compiler version that created the module.</param>
    /// <param name="moduleName">Name of the module.</param>
    /// <returns>SLANG_OK on success, or an error code on failure.</returns>
    [LibraryImport(LibraryName, EntryPoint = "slang_loadModuleInfoFromIRBlob", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial SlangResult LoadModuleInfoFromIRBlob(ISession session, ReadOnlySpan<byte> source, nuint sourceSize, out int moduleVersion, out string moduleCompilerVersion, out string moduleName);

    /// <summary>
    ///     Create a global session, with the built-in core module.
    /// </summary>
    /// <param name="apiVersion">Pass in SLANG_API_VERSION</param>
    /// <param name="globalSession">The created global session.</param>
    [LibraryImport(LibraryName, EntryPoint = "slang_createGlobalSession", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial SlangResult CreateGlobalSession(int apiVersion, out IGlobalSession globalSession);

    /// <summary>
    ///     Create a global session, with the built-in core module.
    /// </summary>
    /// <param name="desc">Description of the global session.</param>
    /// <param name="globalSession">The created global session.</param>
    [LibraryImport(LibraryName, EntryPoint = "slang_createGlobalSession2", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial SlangResult CreateGlobalSession2(SlangGlobalSessionDesc desc, out IGlobalSession globalSession);

    /// <summary>
    ///     Create a global session, but do not set up the core module. The core module can
    ///     then be loaded via loadCoreModule or compileCoreModule
    /// </summary>
    /// <param name="apiVersion">Pass in SLANG_API_VERSION</param>
    /// <param name="globalSession">The created global session that doesn't have a core module setup.</param>
    [LibraryImport(LibraryName, EntryPoint = "slang_createGlobalSessionWithoutCoreModule", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial SlangResult CreateGlobalSessionWithoutCoreModule(int apiVersion, out IGlobalSession globalSession);

    /// <summary>
    ///      Returns a blob that contains the serialized core module.
    ///      Returns nullptr if there isn't an embedded core module.
    ///
    ///     NOTE! API is experimental and not ready for production code
    /// </summary>
    [LibraryImport(LibraryName, EntryPoint = "slang_getEmbeddedCoreModule", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial ISlangBlob GetEmbeddedCoreModule();

    /// <summary>
    ///     Cleanup all global allocations used by Slang, to prevent memory leak detectors from
    ///     reporting them as leaks. This function should only be called after all Slang objects
    ///     have been released. No other Slang functions such as `createGlobalSession`
    ///     should be called after this function.
    /// </summary>
    [LibraryImport(LibraryName, EntryPoint = "slang_shutdown", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible", Justification = "Is Safe")]
    public static partial void Shutdown();

    /// <summary>
    ///     Return the last signaled internal error message.
    /// </summary>
    [LibraryImport(LibraryName, EntryPoint = "slang_getLastInternalErrorMessage", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    public static partial string GetLastInternalErrorMessage();

    /// <summary>
    ///      Disassemble a Slang byte code blob into human-readable text.
    /// </summary>
    [LibraryImport(LibraryName, EntryPoint = "slang_disassembleByteCode", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static partial SlangResult DisassembleByteCode(ISlangBlob moduleBlob, out ISlangBlob disassemblyBlob);
}