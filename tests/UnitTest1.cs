using Shouldly;

namespace SlangShaderSharp.Tests;

public class UnitTest1
{
    [Fact]
    public void TestUsingComWrappers()
    {
        Slang.CreateGlobalSession(Slang.ApiVersion, out var globalSession);

        ((int)globalSession.FindProfile("glsl_450")).ShouldBe(1441792);

        Slang.Shutdown();
    }

    [Fact]
    public void TestGlobalAndLocalSession()
    {
        Slang.CreateGlobalSession(Slang.ApiVersion, out var globalSession);

        var sessionDesc = new SessionDesc
        {
            Targets = [new TargetDesc { Format = SlangCompileTarget.SLANG_SPIRV, Profile = globalSession.FindProfile("spirv_1_5") }]
        };

        globalSession.CreateSession(sessionDesc, out var session).Succeeded.ShouldBeTrue();
        session.ShouldNotBeNull();

        Slang.Shutdown();
    }

    [Fact]
    public void TestModuleLoad()
    {
        // 1. Create Global Session

        Slang.CreateGlobalSession(Slang.ApiVersion, out var globalSession).Succeeded.ShouldBeTrue();

        // 2. Create Session

        var sessionDesc = new SessionDesc
        {
            Targets = [new TargetDesc { Format = SlangCompileTarget.SLANG_WGSL }],

            // Slang supports using the preprocessor.
            PreprocessorMacros = [
                new PreprocessorMacroDesc("BIAS_VALUE", "1138"),
                new PreprocessorMacroDesc("OTHER_MACRO", "float")
            ],
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

        // 4. Query Entry Points

        module.FindEntryPointByName("computeMain", out var entryPoint).Succeeded.ShouldBeTrue();

        // 5. Compose Modules + Entry Points

        session.CreateCompositeComponentType([module, entryPoint], out var composedProgram, out _).Succeeded.ShouldBeTrue();

        // 6. Link (with options)

        composedProgram.LinkWithOptions([
            new CompilerOptionEntry(CompilerOptionName.Obfuscate, CompilerOptionValue.FromInt(1))
        ], out _, out var linkError).Succeeded.ShouldBeTrue(linkError?.AsString ?? "Unknown Error");

        //composedProgram.Link(out _, out var linkError).Succeeded.ShouldBeTrue(linkError?.AsString ?? "Unknown Error");

        // 7. Get Target Kernel Code

        composedProgram.GetEntryPointCode(0, 0, out var wgslCode, out _).Succeeded.ShouldBeTrue();

        // Or compile all entry points
        composedProgram.GetTargetCode(0, out var targetCodeBlob, out _).Succeeded.ShouldBeTrue();

        // Output

        var code = wgslCode.AsString;

        var allCode = targetCodeBlob.AsString;

        // Done

        Slang.Shutdown();
    }

    [Fact]
    public void TestModuleLoadFromDisk()
    {
        Slang.CreateGlobalSession(Slang.ApiVersion, out var globalSession).Succeeded.ShouldBeTrue();

        globalSession.CreateSession(new()
        {
            Targets = [new TargetDesc { Format = SlangCompileTarget.SLANG_WGSL }],
            SearchPaths = ["Assets/"]
        }, out var session).Succeeded.ShouldBeTrue();

        var module = session.LoadModule("MyShader", out var moduleLoadInfo);
        var moduleLoadInfoStr = moduleLoadInfo?.AsString;

        module.ShouldNotBeNull(moduleLoadInfoStr ?? "Unknown Error");

        module.Link(out _, out var linkError).Succeeded.ShouldBeTrue(linkError?.AsString ?? "Unknown Error");

        // We can have multiple entry points so no need to compose into single program?

        module.GetTargetCode(0, out var targetCodeBlob, out var targetCodeError).Succeeded.ShouldBeTrue(targetCodeError?.AsString ?? "Unknown Error");

        // We should have valid WGSL code

        var wgsl = targetCodeBlob.AsString;
        wgsl.ShouldNotBeNullOrEmpty();

        Slang.Shutdown();
    }
}
