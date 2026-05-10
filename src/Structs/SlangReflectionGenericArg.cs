using System.Runtime.InteropServices;

namespace SlangShaderSharp;

[StructLayout(LayoutKind.Explicit)]
public struct SlangReflectionGenericArg
{
    [FieldOffset(0)]
    internal TypeReflection TypeVal;

    [FieldOffset(0)]
    internal long IntVal;

    [FieldOffset(0)]
    internal bool BoolVal;

    public static SlangReflectionGenericArg FromType(TypeReflection type) => new() { TypeVal = type };
    public static SlangReflectionGenericArg FromInt(long value) => new() { IntVal = value };
    public static SlangReflectionGenericArg FromBool(bool value) => new() { BoolVal = value };
}
