using Shouldly;
using SlangShaderSharp.Tests.Support;

namespace SlangShaderSharp.Tests;

[Collection("GlobalSession")]
public sealed class CoreTests(GlobalSessionFixture fixture)
{
    [Fact]
    public void TestUsingComWrappers()
    {
        ((int)fixture.GlobalSession.FindProfile("glsl_450")).ShouldBe(1441792);
    }

    [Fact]
    public void TestGlobalAndLocalSession()
    {
        var sessionDesc = new SessionDesc
        {
            Targets = [new TargetDesc { Format = SlangCompileTarget.Spirv, Profile = fixture.GlobalSession.FindProfile("spirv_1_5") }]
        };

        fixture.GlobalSession.CreateSession(sessionDesc, out var session).Succeeded.ShouldBeTrue();
        session.ShouldNotBeNull();
    }

    [Fact]
    public void TestModuleLoad()
    {
        // 2. Create Session

        var sessionDesc = new SessionDesc
        {
            Targets = [new TargetDesc { Format = SlangCompileTarget.Wgsl }],

            // Slang supports using the preprocessor.
            PreprocessorMacros = [
                new PreprocessorMacroDesc("BIAS_VALUE", "1138"),
                new PreprocessorMacroDesc("OTHER_MACRO", "float")
            ],
        };

        fixture.GlobalSession.CreateSession(sessionDesc, out var session).Succeeded.ShouldBeTrue();

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
    }

    [Fact]
    public void TestModuleLoadFromDisk()
    {
        fixture.GlobalSession.CreateSession(new()
        {
            Targets = [new TargetDesc { Format = SlangCompileTarget.Wgsl }],
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
    }
}
