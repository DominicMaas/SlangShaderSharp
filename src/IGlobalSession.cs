﻿using SlangShaderSharp.Enums;
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
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult CreateSession(
        SessionDesc description,
        out ISession sesion);

    /// <summary>
    ///     Look up the internal ID of a profile by its `name`.
    ///
    ///     Profile IDs are *not* guaranteed to be stable across versions
    ///     of the Slang library, so clients are expected to look up
    ///     profiles by name at runtime.
    /// </summary>
    [PreserveSig]
    SlangProfileID FindProfile([MarshalAs(UnmanagedType.LPUTF8Str)] string name);

    /// <summary>
    ///     Set the path that downstream compilers (aka back end compilers) will be looked from.
    /// </summary>
    /// <param name="passThrough">Identifies the downstream compiler</param>
    /// <param name="path">The path to find the downstream compiler (shared library/dll/executable)</param>
    /// <remarks>
    ///     For back ends that are dlls/shared libraries, it will mean the path will
    ///     be prefixed with the path when calls are made out to ISlangSharedLibraryLoader.
    ///     For executables - it will look for executables along the path */
    /// </remarks>
    [PreserveSig]
    void SetDownstreamCompilerPath(
        SlangPassThrough passThrough,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string path);

    /// <summary>
    ///     Set the 'prelude' for generated code for a 'downstream compiler'.
    /// </summary>
    /// <param name="passThrough">The downstream compiler for generated code that will have the prelude applied to it.</param>
    /// <param name="preludeText">The text added pre-pended verbatim before the generated source</param>
    /// <remarks>
    ///     That for pass-through usage, prelude is not pre-pended, preludes are for code generation only.
    /// </remarks>
    [PreserveSig]
    [Obsolete("Use SetLanguagePrelude instead")]
    void SetDownstreamCompilerPrelude(
        SlangPassThrough passThrough,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string preludeText);

    /// <summary>
    ///     Get the 'prelude' associated with a specific source language.
    /// </summary>
    /// <param name="sourceLanguage"> The language the prelude should be inserted on.</param>
    /// <param name="prelude"> The language the prelude should be inserted on.</param>
    [PreserveSig]
    void GetLanguagePrelude(
        SlangSourceLanguage sourceLanguage,
        out ISlangBlob prelude);

    /// <summary>
    ///     Create a compile request.
    /// </summary>
    [PreserveSig]
    [Obsolete]
    void CreateCompileRequest(out ICompileRequest compileRequest);

    /// <summary>
    ///  Add new builtin declarations to be used in subsequent compiles.
    /// </summary>
    [PreserveSig]
    void AddBuiltins(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string sourcePath,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string sourceString);

    /// <summary>
    ///     Set the session shared library loader. If this changes the loader, it may cause shared
    ///     libraries to be unloaded
    /// </summary>
    /// <param name="loader">The loader to set. Setting nullptr sets the default loader.</param>
    [PreserveSig]
    void SetSharedLibraryLoader(ISlangSharedLibraryLoader loader);

    /// <summary>
    ///     Gets the currently set shared library loader
    /// </summary>
    /// <returns>Gets the currently set loader. If returns nullptr, it's the default loader</returns>
    [PreserveSig]
    ISlangSharedLibraryLoader GetSharedLibraryLoader();

    /// <summary>
    ///     Returns SLANG_OK if the compilation target is supported for this session
    /// </summary>
    /// <param name="target">The compilation target to test</param>
    /// <returns>
    ///     SLANG_OK if the target is available
    ///     SLANG_E_NOT_IMPLEMENTED if not implemented in this build
    ///     SLANG_E_NOT_FOUND if other resources (such as shared libraries) required to make target work
    ///     could not be found SLANG_FAIL other kinds of failures
    /// </returns>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult CheckCompileTargetSupport(SlangCompileTarget target);

    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult CheckPassThroughSupport(SlangPassThrough passThrough);

    /// <summary>
    ///     Compile from (embedded source) the core module on the session.
    ///      Will return a failure if there is already a core module available
    /// </summary>
    /// <param name="flags">flags to control compilation</param>
    /// <remarks>
    ///      NOTE! API is experimental and not ready for production code
    /// </remarks>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult CompileCoreModule(CompileCoreModuleFlags flags);

    /// <summary>
    ///     Load the core module. Currently loads modules from the file system.
    /// </summary>
    /// <param name="coreModule">Start address of the serialized core module</param>
    /// <param name="coreModuleSizeInBytes">The size in bytes of the serialized core module</param>
    /// <remarks>
    ///      NOTE! API is experimental and not ready for production code
    /// </remarks>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult LoadCoreModule(
        nint coreModule,
        nuint coreModuleSizeInBytes);

    /// <summary>
    ///     Save the core module to the file system
    /// </summary>
    /// <param name="archiveType">The type of archive used to hold the core module</param>
    /// <param name="outBlob">he serialized blob containing the core module</param>
    /// <remarks>
    ///     NOTE! API is experimental and not ready for production code
    /// </remarks>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult SaveCoreModule(
        SlangArchiveType archiveType,
        out ISlangBlob outBlob);

    /// <summary>
    ///     Look up the internal ID of a capability by its `name`.
    ///
    ///     Capability IDs are *not* guaranteed to be stable across versions
    ///     of the Slang library, so clients are expected to look up
    ///     capabilities by name at runtime.
    /// </summary>
    [PreserveSig]
    SlangCapabilityID FindCapability([MarshalAs(UnmanagedType.LPUTF8Str)] string name);

    /// <summary>
    ///     Set the downstream/pass through compiler to be used for a transition from the source type to
    ///     the target type
    /// </summary>
    /// <param name="source">he source 'code gen target'</param>
    /// <param name="target">The target 'code gen target'</param>
    /// <param name="compiler">The compiler/pass through to use for the transition from source to target</param>
    [PreserveSig]
    void SetDownstreamCompilerForTransition(
       SlangCompileTarget source,
       SlangCompileTarget target,
       SlangPassThrough compiler);

    /// <summary>
    ///     Get the downstream/pass through compiler for a transition specified by source and target
    /// </summary>
    /// <param name="source">The source 'code gen target'</param>
    /// <param name="target">The target 'code gen target'</param>
    /// <returns>The compiler that is used for the transition. Returns SLANG_PASS_THROUGH_NONE it is not defined</returns>
    [PreserveSig]
    SlangPassThrough GetDownstreamCompilerForTransition(
        SlangCompileTarget source,
        SlangCompileTarget target);

    /// <summary>
    ///     Get the time in seconds spent in the slang and downstream compiler.
    /// </summary>
    [PreserveSig]
    void GetCompilerElapsedTime(
        out double totalTime,
        out double downstreamTime);

    /// <summary>
    ///     Specify a spirv.core.grammar.json file to load and use when
    ///     parsing and checking any SPIR-V code
    /// </summary>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult SetSPIRVCoreGrammar([MarshalAs(UnmanagedType.LPUTF8Str)] string jsonPath);

    /// <summary>
    ///     Parse slangc command line options into a SessionDesc that can be used to create a session
    ///     with all the compiler options specified in the command line.
    /// </summary>
    /// <param name="argc">The number of command line arguments.</param>
    /// <param name="argv">An input array of command line arguments to parse.</param>
    /// <param name="sessionDesc">A pointer to a SessionDesc struct to receive parsed session desc.</param>
    /// <param name="auxAllocation">Auxiliary memory allocated to hold data used in the session desc.</param>
    /// <returns></>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult ParseCommandLineArguments(
        int argc,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPUTF8Str)] string[] argv,
        out SessionDesc sessionDesc,
        out nint auxAllocation);

    /// <summary>
    ///     Computes a digest that uniquely identifies the session description.
    /// </summary>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult GetSessionDescDigest(
        SessionDesc sessionDesc,
        out ISlangBlob blob);

    /// <summary>
    ///     Compile from (embedded source) the builtin module on the session.
    ///      Will return a failure if there is already a builtin module available.
    /// </summary>
    /// <param name="module">The builtin module name.</param>
    /// <param name="flags">flags to control compilation</param>
    /// <remarks>
    ///      NOTE! API is experimental and not ready for production code.
    /// </remarks>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult CompileBuiltinModule(
        BuiltinModuleName module,
        CompileCoreModuleFlags flags);

    /// <summary>
    ///     Load a builtin module. Currently loads modules from the file system.
    /// </summary>
    /// <param name="module">The builtin module name</param>
    /// <param name="moduleData">Start address of the serialized core module</param>
    /// <param name="sizeInBytes">The size in bytes of the serialized builtin module</param>
    /// <remarks>
    ///     NOTE! API is experimental and not ready for production code
    /// </remarks>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult LoadBuiltinModule(
        BuiltinModuleName module,
        nint moduleData,
        nuint sizeInBytes);

    /// <summary>
    ///      Save the builtin module to the file system
    /// </summary>
    /// <param name="module">The builtin module name</param>
    /// <param name="archiveType">The type of archive used to hold the builtin module</param>
    /// <param name="blob">The serialized blob containing the builtin module</param>
    /// <remarks>
    ///     NOTE! API is experimental and not ready for production code
    /// </remarks>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult SaveBuiltinModule(
        BuiltinModuleName module,
        SlangArchiveType archiveType,
        out ISlangBlob blob);
}
