namespace SlangShaderSharp;

public enum SlangStage : uint
{
    None = 0,
    Vertex = 1,
    Hull = 2,
    Domain = 3,
    Geometry = 4,
    Fragment = 5,
    Compute = 6,
    RayGeneration = 7,
    Intersection = 8,
    AnyHit = 9,
    ClosestHit = 10,
    Miss = 11,
    Callable = 12,
    Mesh = 13,
    Amplification = 14,
    Dispatch = 15,
    Node = 16,

    Count,

    Pixel = Fragment,
}
