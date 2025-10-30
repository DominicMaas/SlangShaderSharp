# SlangShaderSharp

A very rough in development C# binding for the Slang shading language.

Bindings written against 2025.19.1.

Not all bindings have been implemented yet!

## Usage

### Loading a Module and Getting WGSL Code for a Compute Shader

```csharp
// 1. Create Global Session

Slang.CreateGlobalSession(0, out var globalSession).Succeeded.ShouldBeTrue();

// 2. Create Session

var sessionDesc = new SessionDesc();

var targetDesc = new TargetDesc { Format = SlangCompileTarget.SLANG_WGSL, };

sessionDesc.Targets = &targetDesc;
sessionDesc.TargetCount = 1;

globalSession.CreateSession(sessionDesc, out var session).Succeeded.ShouldBeTrue();

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
    """u8), out var moduleLoadError);

module.ShouldNotBeNull(moduleLoadError?.AsString ?? "Unknown Error");

// 4. Query Entry Points

module.FindEntryPointByName("computeMain", out var entryPoint).Succeeded.ShouldBeTrue();

// 5. Compose Modules + Entry Points

session.CreateCompositeComponentType([module, entryPoint], out var composedProgram, out _).Succeeded.ShouldBeTrue();

// 6. Link

composedProgram.Link(out _, out var linkError).Succeeded.ShouldBeTrue(linkError?.AsString ?? "Unknown Error");

// 7. Get Target Kernel Code

composedProgram.GetEntryPointCode(0, 0, out var wgslCode, out _).Succeeded.ShouldBeTrue();

// Output

_ = wgslCode.AsString;

// Done

Slang.Shutdown();
```