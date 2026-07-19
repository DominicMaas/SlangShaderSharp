using Shouldly;
using SlangShaderSharp.Tests.Support;

namespace SlangShaderSharp.Tests;

[Collection("GlobalSession")]
public class SessionTests
{
    private readonly GlobalSessionFixture _fixture;
    private readonly ISession _session;


    public SessionTests(GlobalSessionFixture fixture)
    {
        fixture.GlobalSession.CreateSession(new SessionDesc
        {
            Targets = [new TargetDesc { Format = SlangCompileTarget.Spirv, Profile = fixture.GlobalSession.FindProfile("spirv_1_4") }]
        }, out var session).Succeeded.ShouldBeTrue();

        _fixture = fixture;
        _session = session;

    }

    [Fact]
    public void GetGlobalSession()
    {
        _session.GetGlobalSession().ShouldNotBeNull();
        _session.GetGlobalSession().ShouldBe(_fixture.GlobalSession);
    }

    [Fact]
    public void EnsureSessionCreated()
    {
        _session.ShouldNotBeNull();
    }

    [Fact]
    public void CompileAndCheckModule()
    {
        _session.GetLoadedModuleCount().ShouldBe(0);

        var blob = Slang.CreateBlob("""
            RWStructuredBuffer<int> outputBuffer;

            public int foo(int a)
            {
                return a + 1;
            }

            [shader("compute")]
            [numthreads(4, 1, 1)]
            void computeMain(uint3 dispatchThreadID : SV_DispatchThreadID)
            {
                int index = (int)dispatchThreadID.x;
                outputBuffer[index] = foo(index);
            }
            """u8);

        var module = _session.LoadModuleFromSource("MyShader", string.Empty, blob, out _)!;
        module.ShouldNotBeNull();

        _session.GetLoadedModuleCount().ShouldBe(1);

        module.GetTargetCode(0, out var spirv, out _).ShouldBe(SlangResult.SLANG_OK);
        spirv.Buffer.Length.ShouldBeGreaterThan(0);

        var mod0 = _session.GetLoadedModule(0);
        mod0.ShouldBeSameAs(module);
    }

    [Fact]
    public void CompileAndCheckModuleFromDisk()
    {
        _session.GetLoadedModuleCount().ShouldBe(0);

        var source = Slang.CreateBlob(File.ReadAllBytes("Assets/MyShader.slang"));
        var module = _session.LoadModuleFromSource("MyShader", "Assets/MyShader.slang", source, out _)!;
        module.ShouldNotBeNull();

        module.GetTargetCode(0, out var spirv, out _).ShouldBe(SlangResult.SLANG_OK);
        spirv.Buffer.Length.ShouldBeGreaterThan(0);
    }
}
