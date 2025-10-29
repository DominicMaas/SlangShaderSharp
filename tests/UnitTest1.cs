using Shouldly;
using System.Runtime.InteropServices;

namespace SlangShaderSharp.Tests;

public class UnitTest1
{
    [Fact]
    public void TestUsingComWrappers()
    {
        Slang.CreateGlobalSession(0, out var globalSession);

        ((int)globalSession.FindProfile("glsl_450")).ShouldBe(1441792);

        Slang.Shutdown();
    }

    [Fact]
    public unsafe void TestGlobalAndLocalSession()
    {
        Slang.CreateGlobalSession(0, out var globalSession);

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

        globalSession.CreateSession(sessionDesc, out var session).Succeeded.ShouldBeTrue();
        session.ShouldNotBeNull();

        Slang.Shutdown();
    }

    [Fact]
    public unsafe void TestModuleLoad()
    {
        // 1. Create Global Session

        Slang.CreateGlobalSession(0, out var globalSession).Succeeded.ShouldBeTrue();

        // 2. Create Session

        var sessionDesc = new SessionDesc
        {
            structureSize = (nuint)sizeof(SessionDesc),
        };

        var targetDesc = new TargetDesc
        {
            structureSize = (nuint)sizeof(TargetDesc),
            format = SlangCompileTarget.SLANG_WGSL,
        };

        sessionDesc.targets = &targetDesc;
        sessionDesc.targetCount = 1;

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
    }
}
