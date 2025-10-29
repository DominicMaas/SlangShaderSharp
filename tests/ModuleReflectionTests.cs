using Shouldly;

namespace SlangShaderSharp.Tests;

public class ModuleReflectionTests
{
    [Fact]
    public unsafe void Test()
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

        module.GetName().ShouldBe("test");
        module.GetFilePath().ShouldBe("test.slang");

        // 4. Get Module Reflection

        var reflectionModule = module.GetModuleReflection();
        reflectionModule.ShouldNotBe(DeclReflection.Null);

        reflectionModule.GetName().ShouldBe("test");
        reflectionModule.GetKind().ShouldBe(DeclReflection.Kind.Module);

        reflectionModule.Count.ShouldBe(4);

        reflectionModule[0].GetName().ShouldBe("buffer0");
        reflectionModule[1].GetName().ShouldBe("buffer1");
        reflectionModule[2].GetName().ShouldBe("result");
        reflectionModule[3].GetName().ShouldBe("computeMain");
    }
}
