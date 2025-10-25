using Shouldly;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp.Tests;

public class UnitTest1
{
    [Fact]
    public void TestUsingComWrappers()
    {
        Slang.slang_createGlobalSession(0, out var sessionPtr);

        ComWrappers cw = new StrategyBasedComWrappers();
        IGlobalSession foo = (IGlobalSession)cw.GetOrCreateObjectForComInstance(sessionPtr, CreateObjectFlags.None);

        ((int)foo.FindProfile("glsl_450")).ShouldBe(1441792);
    }

    [Fact]
    public unsafe void TestGlobalAndLocalSession()
    {
        Slang.slang_createGlobalSession(0, out var sessionPtr);

        ComWrappers cw = new StrategyBasedComWrappers();
        var globalSession = (IGlobalSession)cw.GetOrCreateObjectForComInstance(sessionPtr, CreateObjectFlags.None);

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

        var output = globalSession.CreateSession(sessionDesc, out var test);

    }
}
