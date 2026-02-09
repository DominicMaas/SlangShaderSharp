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
[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("67618701-d116-468f-ab3b-474bedce0e3d")]
public unsafe partial interface ISession
{
    /// <summary>
    ///     Get the global session thas was used to create this session.
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    IGlobalSession GetGlobalSession();

    /// <summary>
    ///     Load a module as it would be by code using `import`.
    /// </summary>
    [PreserveSig]
    IModule? LoadModule(
        string moduleName,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///     Load a module from Slang source code.
    /// </summary>
    [PreserveSig]
    IModule? LoadModuleFromSource(
        string moduleName,
        string path,
        ISlangBlob source,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///     Combine multiple component types to create a composite component type.
    ///
    ///     The `componentTypes` array must contain `componentTypeCount` pointers
    ///     to component types that were loaded or created using the same session.
    ///
    ///     The shader parameters and specialization parameters of the composite will
    ///     be the union of those in `componentTypes`. The relative order of child
    ///     component types is significant, and will affect the order in which
    ///     parameters are reflected and laid out.
    ///
    ///     The entry-point functions of the composite will be the union of those in
    ///     `componentTypes`, and will follow the ordering of `componentTypes`.
    ///
    ///     The requirements of the composite component type will be a subset of
    ///     those in `componentTypes`. If an entry in `componentTypes` has a requirement
    ///     that can be satisfied by another entry, then the composition will
    ///     satisfy the requirement and it will not appear as a requirement of
    ///     the composite. If multiple entries in `componentTypes` have a requirement
    ///     for the same type, then only the first such requirement will be retained
    ///     on the composite. The relative ordering of requirements on the composite
    ///     will otherwise match that of `componentTypes`.
    ///
    ///     If any diagnostics are generated during creation of the composite, they
    ///     will be written to `outDiagnostics`. If an error is encountered, the
    ///     function will return null.
    ///
    ///     It is an error to create a composite component type that recursively
    ///     aggregates a single module more than once.
    /// </summary>
    [PreserveSig]
    SlangResult CreateCompositeComponentType(
        nint* componentTypes,
        long componentTypeCount,
        out IComponentType compositeComponentType,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///     Specialize a type based on type arguments.
    /// </summary>
    [PreserveSig]
    TypeReflection SpecializeType(
        TypeReflection type,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)][In] SpecializationArg[] specializationArgs,
        nint specializationArgCount,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///     Get the layout `type` on the chosen `target`.
    /// </summary>
    [PreserveSig]
    TypeLayoutReflection GetTypeLayout(
        TypeReflection type,
        nint targetIndex,
        LayoutRules rules,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///     Get a container type from `elementType`. For example, given type `T`, returns
    ///     a type that represents `StructuredBuffer<T>`.
    /// </summary>
    /// <param name="elementType">The element type to wrap around.</param>
    /// <param name="containerType">The type of the container to wrap `elementType` in.</param>
    /// <param name="diagnostics">A blob to receive diagnostic messages.</param>
    [PreserveSig]
    TypeReflection GetContainerType(
        TypeReflection elementType,
        ContainerType containerType,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///      Return a `TypeReflection` that represents the `__Dynamic` type.
    ///      This type can be used as a specialization argument to indicate using
    ///      dynamic dispatch.
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    TypeReflection GetDynamicType();

    /// <summary>
    ///     Get the mangled name for a type RTTI object.
    /// </summary>
    [PreserveSig]
    SlangResult GetTypeRTTIMangledName(
        TypeReflection type,
        out ISlangBlob nameBlob);

    /// <summary>
    ///     Get the mangled name for a type witness.
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    SlangResult GetTypeConformanceWitnessMangledName(
        TypeReflection type,
        TypeReflection interfaceType,
        out ISlangBlob nameBlob);

    /// <summary>
    ///     Get the sequential ID used to identify a type witness in a dynamic object.
    ///     The sequential ID is part of the RTTI bytes returned by `getDynamicObjectRTTIBytes`.
    /// </summary>
    [PreserveSig]
    SlangResult GetTypeConformanceWitnessSequentialID(
        TypeReflection type,
        TypeReflection interfaceType,
        out uint id);

    /// <summary>
    ///     Create a request to load/compile front-end code.
    /// </summary>
    [PreserveSig]
    SlangResult CreateCompileRequest(out ICompileRequest compileRequest);

    /// <summary>
    ///     Creates a `IComponentType` that represents a type's conformance to an interface.
    ///     The retrieved `ITypeConformance` objects can be included in a composite `IComponentType`
    ///     to explicitly specify which implementation types should be included in the final compiled
    ///     code.For example, if an module defines `IMaterial` interface and `AMaterial`,
    ///     `BMaterial`, `CMaterial` types that implements the interface, the user can exclude
    ///     `CMaterial` implementation from the resulting shader code by explicitly adding
    ///     `AMaterial:IMaterial` and `BMaterial:IMaterial` conformances to a composite
    ///     `IComponentType` and get entry point code from it.The resulting code will not have
    ///     anything related to `CMaterial` in the dynamic dispatch logic.If the user does not
    ///     explicitly include any `TypeConformances` to an interface type, all implementations to
    ///     that interface will be included by default. By linking a `ITypeConformance`, the user is
    ///     also given the opportunity to specify the dispatch ID of the implementation type.If
    ///     `conformanceIdOverride` is -1, there will be no override behavior and Slang will
    ///     automatically assign IDs to implementation types.The automatically assigned IDs can be
    ///     queried via `ISession::getTypeConformanceWitnessSequentialID`.
    /// </summary>
    /// <returns>SLANG_OK if succeeds, or SLANG_FAIL if `type` does not conform to `interfaceType`.</returns>
    [PreserveSig]
    SlangResult CreateTypeConformanceComponentType(
        TypeReflection type,
        TypeReflection interfaceType,
        out ITypeConformance conformance,
        long conformanceIdOverride,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///     Load a module from a Slang module blob.
    /// </summary>
    [PreserveSig]
    IModule? LoadModuleFromIRBlob(
        string moduleName,
        string path,
        ISlangBlob source,
        out ISlangBlob? diagnostics);

    [PreserveSig]
    long GetLoadedModuleCount();

    [PreserveSig]
    IModule? GetLoadedModule(long index);

    /// <summary>
    ///     Checks if a precompiled binary module is up-to-date with the current compiler
    ///     option settings and the source file contents.
    /// </summary>
    [PreserveSig]
    [return: MarshalAs(UnmanagedType.I1)]
    bool IsBinaryModuleUpToDate(
        string modulePath,
        ISlangBlob binaryModuleBlob);

    /// <summary>
    ///     Load a module from a string.
    /// </summary>
    [PreserveSig]
    IModule? LoadModuleFromSourceString(
        string moduleName,
        string path,
        string sourceStr,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///     Get the 16-byte RTTI header to fill into a dynamic object.
    ///     This header is used to identify the type of the object for dynamic dispatch purpose.
    ///     For example, given the following shader:
    ///
    ///     <code lang="slang">
    ///     [anyValueSize(32)] dyn interface IFoo { int eval(); }
    ///     struct Impl : IFoo { int eval() { return 1; } }
    ///
    ///     ConstantBuffer<dyn IFoo> cb0;
    ///
    ///     [numthreads(1,1,1)
    ///     void main()
    ///     {
    ///         cb0.eval();
    ///     }
    ///     </code>
    ///
    ///     The constant buffer `cb0` should be filled with 16+32=48 bytes of data, where the first
    ///      16 bytes should be the RTTI bytes returned by calling `getDynamicObjectRTTIBytes(type_Impl,
    ///      type_IFoo)`, and the rest 32 bytes should hold the actual data of the dynamic object (in
    ///      this case, fields in the `Impl` type).
    ///
    ///     `bufferSizeInBytes` must be greater than 16.
    /// </summary>
    [PreserveSig]
    SlangResult GetDynamicObjectRTTIBytes(
        TypeReflection type,
        TypeReflection interfaceType,
        out long rttiDataBuffer,
        long bufferSizeInBytes);

    /// <summary>
    ///     Read module info (name and version) from a module blob
    ///
    ///     The returned pointers are valid for as long as the session.
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    SlangResult LoadModuleInfoFromIRBlob(
        ISlangBlob source,
        out long moduleVersion,
        out string moduleCompilerVersion,
        out string moduleName);
}

public static class ISessionExtensions
{
    extension(ISession session)
    {
        /// <summary>
        ///     Specialize a type based on type arguments.
        /// </summary>
        public TypeReflection SpecializeType(TypeReflection type, SpecializationArg[] specializationArgs, out ISlangBlob? diagnostics)
        {
            return session.SpecializeType(type, specializationArgs, specializationArgs.Length, out diagnostics);
        }

        /// <summary>
        ///     Combine multiple component types to create a composite component type.
        ///
        ///     The `componentTypes` array must contain `componentTypeCount` pointers
        ///     to component types that were loaded or created using the same session.
        ///
        ///     The shader parameters and specialization parameters of the composite will
        ///     be the union of those in `componentTypes`. The relative order of child
        ///     component types is significant, and will affect the order in which
        ///     parameters are reflected and laid out.
        ///
        ///     The entry-point functions of the composite will be the union of those in
        ///     `componentTypes`, and will follow the ordering of `componentTypes`.
        ///
        ///     The requirements of the composite component type will be a subset of
        ///     those in `componentTypes`. If an entry in `componentTypes` has a requirement
        ///     that can be satisfied by another entry, then the composition will
        ///     satisfy the requirement and it will not appear as a requirement of
        ///     the composite. If multiple entries in `componentTypes` have a requirement
        ///     for the same type, then only the first such requirement will be retained
        ///     on the composite. The relative ordering of requirements on the composite
        ///     will otherwise match that of `componentTypes`.
        ///
        ///     If any diagnostics are generated during creation of the composite, they
        ///     will be written to `outDiagnostics`. If an error is encountered, the
        ///     function will return null.
        ///
        ///     It is an error to create a composite component type that recursively
        ///     aggregates a single module more than once.
        /// </summary>
        public unsafe SlangResult CreateCompositeComponentType(ReadOnlySpan<IComponentType> componentTypes, out IComponentType compositeComponentType, out ISlangBlob? diagnostics)
        {
            var pointers = stackalloc nint[componentTypes.Length];
            for (int i = 0; i < componentTypes.Length; i++)
            {
                if (!ComWrappers.TryGetComInstance(componentTypes[i], out nint pointer))
                    throw new InvalidOperationException("Failed to get COM pointer for component type.");

                pointers[i] = pointer;
            }

            return session.CreateCompositeComponentType(pointers, componentTypes.Length, out compositeComponentType, out diagnostics);
        }
    }
}