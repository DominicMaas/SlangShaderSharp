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
    IModule LoadModule([MarshalAs(UnmanagedType.LPUTF8Str)] string moduleName, out ISlangBlob diagnostics);

    /// <summary>
    ///     Load a module from Slang source code.
    /// </summary>
    [PreserveSig]
    IModule LoadModuleFromSource([MarshalAs(UnmanagedType.LPUTF8Str)] string moduleName, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, ISlangBlob source, out ISlangBlob? diagnostics);

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
    int CreateCompositeComponentType(nint* componentTypes, long componentTypeCount, out IComponentType compositeComponentType, out ISlangBlob? diagnostics);

    // specializeType

    // getTypeLayout

    // getContainerType

    // getDynamicType

    // getTypeRTTIMangledName

    // getTypeConformanceWitnessMangledName

    // getTypeConformanceWitnessSequentialID

    // createCompileRequest

    // createTypeConformanceComponentType

    // loadModuleFromIRBlob

    // getLoadedModuleCount

    // getLoadedModule

    // isBinaryModuleUpToDate

    // loadModuleFromSourceString

    // etc.
}

public static class ISessionExtensions
{
    extension(ISession session)
    {
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
        public unsafe int CreateCompositeComponentType(ReadOnlySpan<IComponentType> componentTypes, out IComponentType compositeComponentType, out ISlangBlob? diagnostics)
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