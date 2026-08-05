namespace SlangShaderSharp;

/// <summary>
///     Options to control floating-point denormal handling mode for a target.
/// </summary>
/// <remarks>
///     Used as the value of the <see cref="CompilerOptionName.DenormalModeFp16" />,
///     <see cref="CompilerOptionName.DenormalModeFp32" /> and
///     <see cref="CompilerOptionName.DenormalModeFp64" /> compiler options.
/// </remarks>
public enum SlangFpDenormalMode : uint
{
    Any = 0,
    Preserve = 1,
    Ftz = 2,
}
