namespace SlangShaderSharp;

public enum SlangLineDirectiveMode : uint
{
    /// <summary>
    ///     Default behavior: pick behavior base on target.
    /// </summary>
    Default = 0,

    /// <summary>
    ///     Don't emit line directives at all.
    /// </summary>
    None = 1,

    /// <summary>
    ///     Emit standard C-style `#line` directives.
    /// </summary>
    Standard = 2,

    /// <summary>
    ///     Emit GLSL-style directives with file *number* instead of name
    /// </summary>
    Glsl = 3,

    /// <summary>
    ///     Use a source map to track line mappings (ie no #line will appear in emitting source)
    /// </summary>
    SourceMap = 4,
}
