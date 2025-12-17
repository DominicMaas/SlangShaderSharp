using Shouldly;

namespace SlangShaderSharp.Tests;

public class ModuleReflectionTests
{
    [Fact]
    public void Test()
    {
        // 1. Create Global Session

        Slang.CreateGlobalSession(Slang.ApiVersion, out var globalSession).Succeeded.ShouldBeTrue();

        // 2. Create Session

        var sessionDesc = new SessionDesc
        {
            Targets = [new TargetDesc { Format = SlangCompileTarget.Wgsl }],
        };

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

        reflectionModule.Name.ShouldBe("test");
        reflectionModule.Kind.ShouldBe(DeclReflectionKind.Module);

        reflectionModule.Count.ShouldBe(4);

        reflectionModule[0].Name.ShouldBe("buffer0");
        reflectionModule[0].Kind.ShouldBe(DeclReflectionKind.Variable);

        var buffer0Reflection = reflectionModule[0].AsVariable();
        buffer0Reflection.ShouldNotBe(VariableReflection.Null);

        reflectionModule[0].AsFunction().ShouldBe(FunctionReflection.Null);

        reflectionModule[1].Name.ShouldBe("buffer1");
        reflectionModule[1].Kind.ShouldBe(DeclReflectionKind.Variable);

        reflectionModule[2].Name.ShouldBe("result");
        reflectionModule[2].Kind.ShouldBe(DeclReflectionKind.Variable);

        var computeMain = reflectionModule[3];
        computeMain.Name.ShouldBe("computeMain");
        computeMain.Kind.ShouldBe(DeclReflectionKind.Func);

        computeMain.Count.ShouldBe(1);
        computeMain[0].Name.ShouldBe("threadId");
        computeMain[0].Kind.ShouldBe(DeclReflectionKind.Variable);

        var computeMainFunction = computeMain.AsFunction();
        computeMainFunction.ShouldNotBe(FunctionReflection.Null);

        computeMainFunction.ParameterCount.ShouldBe((uint)1);
        var param0 = computeMainFunction.GetParameter(0);
    }
}
