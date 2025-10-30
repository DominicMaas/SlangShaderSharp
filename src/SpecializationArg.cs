using System.Runtime.InteropServices;

namespace SlangShaderSharp;

public unsafe struct SpecializationArg
{
    /// <summary>
    ///     The kind of specialization argument.
    /// </summary>
    public SpecializationArgKind kind;

    public SpecializationArgUnion union;

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct SpecializationArgUnion
    {
        /// <summary>
        ///     A type specialization argument, used for `Kind::Type`
        /// </summary>
        [FieldOffset(0)]
        public TypeReflection* type;

        /// <summary>
        ///     An expression in Slang syntax, used for `Kind::Expr`
        /// </summary>
        [FieldOffset(0)]
        public sbyte* expr;
    }

    public static SpecializationArg FromType(TypeReflection* type) => new()
    {
        kind = SpecializationArgKind.Type,
        union = new SpecializationArgUnion
        {
            type = type
        }
    };

    public static SpecializationArg FromExpr(sbyte* expr) => new()
    {
        kind = SpecializationArgKind.Expr,
        union = new SpecializationArgUnion
        {
            expr = expr
        }
    };
}
