using SlangShaderSharp.Internal;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

/// <summary>
///     A module is the granularity of shader code compilation and loading.
///
///     In most cases a module corresponds to a single compile "translation unit."
///     This will often be a single `.slang` or `.hlsl` file and everything it
///     `#include`s.
///
///     Notably, a module `M` does *not* include the things it `import`s, as these
///     as distinct modules that `M` depends on. There is a directed graph of
///     module dependencies, and all modules in the graph must belong to the
///     same session (`ISession`).
///
///     A module establishes a namespace for looking up types, functions, etc.
/// </summary>
[GeneratedComInterface]
[Guid("0c720e64-8722-4d31-8990-638a98b1c279")]
public unsafe partial interface IModule : IComponentType
{
    /// <summary>
    ///     Find and an entry point by name.
    ///     Note that this does not work in case the function is not explicitly designated as an entry
    ///     point, e.g. using a `[shader("...")]` attribute. In such cases, consider using
    ///     `IModule::findAndCheckEntryPoint` instead.
    /// </summary>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult FindEntryPointByName(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string name,
        out IEntryPoint entryPoint);

    /// <summary>
    ///      Get number of entry points defined in the module. An entry point defined in a module
    ///      is by default not included in the linkage, so calls to `IComponentType::getEntryPointCount`
    ///      on an `IModule` instance will always return 0. However `IModule::getDefinedEntryPointCount`
    ///      will return the number of defined entry points.
    /// </summary>
    [PreserveSig]
    int GetDefinedEntryPointCount();

    /// <summary>
    ///     Get the name of an entry point defined in the module.
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult GetDefinedEntryPoint(
        int index,
        out IEntryPoint entryPoint);

    /// <summary>
    ///     Get a serialized representation of the checked module.
    /// </summary>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult Serialize(out ISlangBlob serializedBlob);

    /// <summary>
    ///     Write the serialized representation of this module to a file.
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult WriteToFile([MarshalAs(UnmanagedType.LPUTF8Str)] string fileName);

    /// <summary>
    ///     Get the name of the module.
    /// </summary>
    [PreserveSig]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    string GetName();

    /// <summary>
    ///      Get the path of the module.
    /// </summary>
    [PreserveSig]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    string GetFilePath();

    /// <summary>
    ///      Get the unique identity of the module.
    /// </summary>
    [PreserveSig]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    string GetUniqueIdentity();

    /// <summary>
    ///      Find and validate an entry point by name, even if the function is
    ///      not marked with the `[shader("...")]` attribute.
    /// </summary>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult FindAndCheckEntryPoint(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string name,
        SlangStage stage,
        out IEntryPoint entryPoint,
        out ISlangBlob? diagnostics);

    /// <summary>
    ///     Get the number of dependency files that this module depends on.
    ///     This includes both the explicit source files, as well as any
    ///     additional files that were transitively referenced (e.g., via
    ///     a `#include` directive).
    /// </summary>
    [PreserveSig]
    int GetDependencyFileCount();

    /// <summary>
    ///      Get the path to a file this module depends on.
    /// </summary>
    [PreserveSig]
    [return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]
    string GetDependencyFilePath(int index);

    [PreserveSig]
    [return: MarshalUsing(typeof(DeclReflectionMarshaller))]
    DeclReflection GetModuleReflection();

    /// <summary>
    ///     Disassemble a module.
    /// </summary>
    /// <param name="disassembledBlob"></param>
    /// <returns></returns>
    [PreserveSig]
    [return: MarshalUsing(typeof(SlangResultMarshaller))]
    SlangResult Disassemble(out ISlangBlob disassembledBlob);
}
