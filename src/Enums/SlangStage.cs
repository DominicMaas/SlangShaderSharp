namespace SlangShaderSharp;

public enum SlangStage : uint
{
    None,
    Vertex,
    Hull,
    Domain,
    Geometry,
    Fragment,
    Compute,
    RayGeneration,
    Intersection,
    AnyHit,
    ClosestHit,
    Miss,
    Callable,
    Mesh,
    Amplification,
    Dispatch,

    Count,

    Pixel = Fragment,
}
