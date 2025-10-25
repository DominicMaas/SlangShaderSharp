using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[GeneratedComInterface]
[Guid("5bc42be8-5c50-4929-9e5e-d15e7c24015f")]
public partial interface IComponentType
{
    /// <summary>
    ///     Get the runtime session that this component type belongs to.
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    public ISession GetSession();

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
    public void GetLayout(int targetIndex, out ISlangBlob diagnostics);
}
