# SlangShaderSharp

A very rough in development C# binding for the Slang shading language.


## Usage

```csharp
// 1. Create Global Session

Slang.CreateGlobalSession(0, out var globalSession);

// 2. Create Session

var sessionDesc = new SessionDesc
{
    structureSize = (nuint)Marshal.SizeOf<SessionDesc>(),
};

var targetDesc = new TargetDesc
{
    structureSize = (nuint)Marshal.SizeOf<TargetDesc>(),
    format = SlangCompileTarget.SLANG_SPIRV,
    profile = globalSession.FindProfile("spirv_1_5"),
};

sessionDesc.targets = &targetDesc;
sessionDesc.targetCount = 1;

globalSession.CreateSession(sessionDesc, out var session).ShouldBe(0);

// 3. Load module

var module = session.LoadModuleFromSource("test", "test.slang", Slang.CreateBlob("""
    StructuredBuffer<float> buffer0;
    StructuredBuffer<float> buffer1;
    RWStructuredBuffer<float> result;

    [shader("compute")]
    [numthreads(1,1,1)]
    void computeMain(uint3 threadId : SV_DispatchThreadID)
    {
        uint index = threadId.x;
        result[index] = buffer0[index] + buffer1[index];
    }
    """u8), out _);

// 4. Query Entry Points

module.FindEntryPointByName("computeMain", out var entryPoint).ShouldBe(0);

// 5. Compose Modules + Entry Points

ComWrappers.TryGetComInstance(module, out nint modulePointer).ShouldBeTrue();
ComWrappers.TryGetComInstance(entryPoint, out nint entryPointPointer).ShouldBeTrue();

var pointers = stackalloc nint[2];
pointers[0] = modulePointer;
pointers[1] = entryPointPointer;

session.CreateCompositeComponentType(pointers, 2, out var composedProgram, out _).ShouldBe(0);

// 6. Link

composedProgram.Link(out var linkedProgram, out _).ShouldBe(0);

// 7. Get Target Kernel Code

composedProgram.GetEntryPointCode(0, 0, out var spirvCode, out _).ShouldBe(0);

var codeSpan = new ReadOnlySpan<byte>(spirvCode.GetBufferPointer(), (int)spirvCode.GetBufferSize());
var codeString = Encoding.UTF8.GetString(codeSpan);
```