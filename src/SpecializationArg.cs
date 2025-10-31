using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp;

[NativeMarshalling(typeof(SpecializationArgMarshaller))]
public struct SpecializationArg
{
    /// <summary>
    ///     The kind of specialization argument.
    /// </summary>
    public SpecializationArgKind Kind;

    /// <summary>
    ///     The value of the specialization argument.
    /// </summary>
    public SpecializationArgUnion Value;

    public unsafe struct SpecializationArgUnion
    {
        /// <summary>
        ///     A type specialization argument, used for `Kind::Type`
        /// </summary>
        public TypeReflection Type;

        /// <summary>
        ///     An expression in Slang syntax, used for `Kind::Expr`
        /// </summary>
        public string? Expression;
    }

    public static SpecializationArg FromType(TypeReflection type) => new()
    {
        Kind = SpecializationArgKind.Type,
        Value = new SpecializationArgUnion
        {
            Type = type
        }
    };

    public static SpecializationArg FromExpression(string expression) => new()
    {
        Kind = SpecializationArgKind.Expr,
        Value = new SpecializationArgUnion
        {
            Expression = expression
        }
    };
}

internal unsafe struct SpecializationArgUnmanaged
{
    public SpecializationArgKind kind;
    public SpecializationArgUnionUnmanaged union;

    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct SpecializationArgUnionUnmanaged
    {
        [FieldOffset(0)]
        public nint type;
        [FieldOffset(0)]
        public byte* expr;
    }
}

[CustomMarshaller(typeof(SpecializationArg), MarshalMode.Default, typeof(SpecializationArgMarshaller))]
internal static unsafe class SpecializationArgMarshaller
{
    public static SpecializationArgUnmanaged ConvertToUnmanaged(SpecializationArg managed)
    {
        return new SpecializationArgUnmanaged
        {
            kind = managed.Kind,
            union = new SpecializationArgUnmanaged.SpecializationArgUnionUnmanaged
            {
                type = managed.Value.Type.Handle,
                expr = Utf8StringMarshaller.ConvertToUnmanaged(managed.Value.Expression)
            }
        };
    }

    public static SpecializationArg ConvertToManaged(SpecializationArgUnmanaged unmanaged)
    {
        return new SpecializationArg
        {
            Kind = unmanaged.kind,
            Value = new SpecializationArg.SpecializationArgUnion
            {
                Type = new TypeReflection(unmanaged.union.type),
                Expression = Utf8StringMarshaller.ConvertToManaged(unmanaged.union.expr)
            }
        };
    }

    public static void Free(SpecializationArgUnmanaged unmanaged)
    {
        Utf8StringMarshaller.Free(unmanaged.union.expr);
    }
}
