using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid("5bc42be8-5c50-4929-9e5e-d15e7c24015f")]
public unsafe partial interface IComponentType
{
    /// <summary>
    ///     Get the runtime session that this component type belongs to.
    /// </summary>
    [PreserveSig]
    ISession GetSession();

    /// <summary>
    ///     Get the layout for this program for the chosen `targetIndex`.
    ///
    ///     The resulting layout will establish offsets/bindings for all
    ///     of the global and entry-point shader parameters in the
    ///     component type.
    ///
    ///     If this component type has specialization parameters (that is,
    ///     it is not fully specialized), then the resulting layout may
    ///     be incomplete, and plugging in arguments for generic specialization
    ///     parameters may result in a component type that doesn't have
    ///     a compatible layout. If the component type only uses
    ///     interface-type specialization parameters, then the layout
    ///     for a specialization should be compatible with an unspecialized
    ///     layout(all parameters in the unspecialized layout will have
    ///     the same offset/binding in the specialized layout).
    ///
    ///     If this component type is combined into a composite, then
    ///     the absolute offsets/bindings of parameters may not stay the same.
    ///     If the shader parameters in a component type don't make
    ///     use of explicit binding annotations(e.g., `register(...)`),
    ///     then the *relative* offset of shader parameters will stay
    ///     the same when it is used in a composition.
    /// </summary>
    [PreserveSig]
    ShaderReflection GetLayout(
        int targetIndex,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///     Get the number of (unspecialized) specialization parameters for the component type.
    /// </summary>
    [PreserveSig]
    int GetSpecializationParamCount();

    /// <summary>
    ///     Get the compiled code for the entry point at `entryPointIndex` for the chosen `targetIndex`
    ///
    ///     Entry point code can only be computed for a component type that
    ///     has no specialization parameters (it must be fully specialized)
    ///     and that has no requirements (it must be fully linked).
    ///
    ///     If code has not already been generated for the given entry point and target,
    ///     then a compilation error may be detected, in which case `outDiagnostics`
    ///     (if non-null) will be filled in with a blob of messages diagnosing the error.
    /// </summary>
    [PreserveSig]
    SlangResult GetEntryPointCode(
        int entryPointIndex,
        int targetIndex,
        out ISlangBlob outCode,
        out ISlangBlob? outDiagnostics);

    /// <summary>
    ///     Get the compilation result as a file system.
    ///
    ///     Has the same requirements as getEntryPointCode.
    ///
    ///     The result is not written to the actual OS file system, but is made available as an
    ///     in memory representation.
    /// </summary>
    [PreserveSig]
    SlangResult GetResultAsFileSystem(
        int entryPointIndex,
        int targetIndex,
        out ISlangMutableFileSystem fileSystem);

    /// <summary>
    ///     Compute a hash for the entry point at `entryPointIndex` for the chosen `targetIndex`.
    ///
    ///     This computes a hash based on all the dependencies for this component type as well as the
    ///     target settings affecting the compiler backend. The computed hash is used as a key for caching
    ///     the output of the compiler backend to implement shader caching.
    /// </summary>
    [PreserveSig]
    void GetEntryPointHash(
        int entryPointIndex,
        int targetIndex,
        out ISlangBlob hash);

    /// <summary>
    ///     Specialize the component by binding its specialization parameters to concrete arguments.
    ///
    ///     The `specializationArgs` array must have `specializationArgCount` entries, and
    ///     this must match the number of specialization parameters on this component type.
    ///
    ///      If any diagnostics (error or warnings) are produced, they will be written to `diagnostics`.
    /// </summary>
    [PreserveSig]
    SlangResult Specialize(
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)][In] SpecializationArg[] specializationArgs,
        int specializationArgCount,
        out IComponentType specializedComponentType,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///     Link this component type against all of its unsatisfied dependencies.
    ///
    ///     A component type may have unsatisfied dependencies. For example, a module
    ///     depends on any other modules it `import`s, and an entry point depends
    ///     on the module that defined it.
    ///
    ///     A user can manually satisfy dependencies by creating a composite
    ///     component type, and when doing so they retain full control over
    ///     the relative ordering of shader parameters in the resulting layout.
    ///
    ///     It is an error to try to generate/access compiled kernel code for
    ///     a component type with unresolved dependencies, so if dependencies
    ///     remain after whatever manual composition steps an application
    ///     cares to perform, the `link()` function can be used to automatically
    ///     compose in any remaining dependencies.The order of parameters
    ///     (and hence the global layout) that results will be deterministic,
    ///     but is not currently documented.
    /// </summary>
    [PreserveSig]
    SlangResult Link(
        out IComponentType lLinkedComponentType,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///     Get entry point 'callable' functions accessible through the ISlangSharedLibrary interface.
    ///
    ///     The functions remain in scope as long as the ISlangSharedLibrary interface is in scope.
    ///     NOTE! Requires a compilation target of SLANG_HOST_CALLABLE.
    /// </summary>
    /// <param name="entryPointIndex">The index of the entry point to get code for.</param>
    /// <param name="targetIndex">The index of the target to get code for (default: zero).</param>
    /// <param name="sharedLibrary">A pointer to a ISharedLibrary interface which functions can be queried on.</param>
    /// <returns>A `SlangResult` to indicate success or failure.</returns>
    [PreserveSig]
    SlangResult GetEntryPointHostCallable(int entryPointIndex,
        int targetIndex,
        out ISlangSharedLibrary sharedLibrary,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///     Get a new ComponentType object that represents a renamed entry point.
    ///
    ///     The current object must be a single EntryPoint, or a CompositeComponentType or
    ///     SpecializedComponentType that contains one EntryPoint component.
    /// </summary>
    [PreserveSig]
    SlangResult RenameEntryPoint(
        string newName,
        out IComponentType entryPoint);

    /// <summary>
    ///     Link and specify additional compiler options when generating code
    ///     from the linked program.
    /// </summary>
    [PreserveSig]
    SlangResult LinkWithOptions(
        out IComponentType linkedComponentType,
        uint compilerOptionEntryCount,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)][In] CompilerOptionEntry[] compilerOptionEntries,
        out ISlangBlob? diagnostics);

    [PreserveSig]
    SlangResult GetTargetCode(
        int targetIndex,
        out ISlangBlob code,
        out ISlangBlob? diagnostics);

    [PreserveSig]
    SlangResult GetTargetMetadata(
        int targetIndex,
        out IMetadata metadata,
        out ISlangBlob? diagnostics);

    [PreserveSig]
    SlangResult GetEntryPointMetadata(
        int entryPointIndex,
        int targetIndex,
        out IMetadata metadata,
        out ISlangBlob? diagnostics);
}

public static class IComponentTypeExtensions
{
    extension(IComponentType compontentType)
    {
        /// <summary>
        ///     Specialize the component by binding its specialization parameters to concrete arguments.
        ///
        ///     The `specializationArgs` array must match the number of specialization parameters on this component type.
        ///
        ///      If any diagnostics (error or warnings) are produced, they will be written to `diagnostics`.
        /// </summary>
        public SlangResult Specialize(SpecializationArg[] specializationArgs, out IComponentType specializedComponentType, out ISlangBlob? diagnostics)
        {
            return compontentType.Specialize(specializationArgs, specializationArgs.Length, out specializedComponentType, out diagnostics);
        }

        /// <summary>
        ///     Link and specify additional compiler options when generating code
        ///     from the linked program.
        /// </summary>
        public SlangResult LinkWithOptions(CompilerOptionEntry[] compilerOptionEntries, out IComponentType linkedComponentType, out ISlangBlob? diagnostics)
        {
            return compontentType.LinkWithOptions(out linkedComponentType, (uint)compilerOptionEntries.Length, compilerOptionEntries, out diagnostics);
        }
    }
}