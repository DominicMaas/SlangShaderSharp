using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[DebuggerDisplay("{Handle}")]
[NativeMarshalling(typeof(GenericReflectionMarshaller))]
public readonly struct GenericReflection : IEquatable<GenericReflection>
{
    internal readonly nint Handle;

    [Obsolete("Use GenericReflection.Null instead.")]
    public GenericReflection()
    {
        Handle = 0;
    }

    internal GenericReflection(nint value)
    {
        Handle = value;
    }

    public static GenericReflection Null => new(0);

    public static bool operator ==(GenericReflection left, GenericReflection right) => left.Handle == right.Handle;
    public static bool operator !=(GenericReflection left, GenericReflection right) => !(left == right);

    public bool Equals(GenericReflection other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is GenericReflection other && Equals(other);
    public override int GetHashCode() => unchecked((int)Handle);
    public override string ToString() => $"0x{Handle:x}";

    // spReflectionGeneric_asDecl

    // spReflectionGeneric_GetName

    // spReflectionGeneric_GetTypeParameterCount

    // spReflectionGeneric_GetTypeParameter

    // spReflectionGeneric_GetValueParameterCount

    // spReflectionGeneric_GetValueParameter

    // spReflectionGeneric_GetTypeParameterConstraintCount

    // spReflectionGeneric_GetTypeParameterConstraintType

    // spReflectionGeneric_GetInnerDecl

    // spReflectionGeneric_GetInnerKind

    // spReflectionGeneric_GetOuterGenericContainer

    // spReflectionGeneric_GetConcreteType

    // spReflectionGeneric_GetConcreteIntVal

    // spReflectionGeneric_applySpecializations
}

[CustomMarshaller(typeof(GenericReflection), MarshalMode.Default, typeof(GenericReflectionMarshaller))]
internal static class GenericReflectionMarshaller
{
    public static nint ConvertToUnmanaged(GenericReflection managed) => managed.Handle;
    public static GenericReflection ConvertToManaged(nint unmanaged) => new(unmanaged);
}
