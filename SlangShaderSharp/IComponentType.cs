using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[GeneratedComInterface]
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
    ShaderReflection GetLayout(int targetIndex, out ISlangBlob diagnostics);

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
    int GetEntryPointCode(int entryPointIndex, int targetIndex, out ISlangBlob outCode, out ISlangBlob outDiagnostics);

    /// <summary>
    ///     Get the compilation result as a file system.
    ///
    ///     Has the same requirements as getEntryPointCode.
    ///
    ///     The result is not written to the actual OS file system, but is made available as an
    ///     in memory representation.
    /// </summary>
    [PreserveSig]
    int GetResultAsFileSystem(int entryPointIndex, int targetIndex, out ISlangMutableFileSystem fileSystem);

    /// <summary>
    ///     Compute a hash for the entry point at `entryPointIndex` for the chosen `targetIndex`.
    ///
    ///     This computes a hash based on all the dependencies for this component type as well as the
    ///     target settings affecting the compiler backend. The computed hash is used as a key for caching
    ///     the output of the compiler backend to implement shader caching.
    /// </summary>
    [PreserveSig]
    public void GetEntryPointHash(int entryPointIndex, int targetIndex, out ISlangBlob hash);

    [PreserveSig]
    public int Specialize(nint* specializationArgs, int specializationArgCount, out IComponentType specializedComponentType, out ISlangBlob? diagnostics);

    [PreserveSig]
    public int Link(out IComponentType lLinkedComponentType, out ISlangBlob? diagnostics);

    [PreserveSig]
    int GetEntryPointHostCallable();

    [PreserveSig]
    int RenameEntryPoint();

    [PreserveSig]
    int LinkWithOptions();

    [PreserveSig]
    int GetTargetCode(int targetIndex, out ISlangBlob code, out ISlangBlob? diagnostics);

    [PreserveSig]
    int GetTargetMetadata(int targetIndex, out IMetadata metadata, out ISlangBlob? diagnostics);

    [PreserveSig]
    int GetEntryPointMetadata(int entryPointIndex, int targetIndex, out IMetadata metadata, out ISlangBlob? diagnostics);
}
