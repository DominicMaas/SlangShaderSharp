namespace SlangShaderSharp.Internal;

public static class ReflectionHelpers
{
    public static void AssertValid(nint handle)
    {
        if (handle == nint.Zero)
        {
            throw new ArgumentException("Invalid Handle!");
        }
    }
}
