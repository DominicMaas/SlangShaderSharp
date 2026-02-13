using Shouldly;
using SlangShaderSharp.Tests.Support;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp.Tests;

/// <summary>
///     Tests all Vtable functions of IGlobalSession to ensure everything is setup correctly.
/// </summary>
/// <param name="fixture"></param>
[Collection("GlobalSession")]
public class GlobalSessionTests(GlobalSessionFixture fixture)
{
    [Fact]
    public void CreateSession()
    {
        var sessionDesc = new SessionDesc
        {
            Targets = [new TargetDesc { Format = SlangCompileTarget.Spirv, Profile = fixture.GlobalSession.FindProfile("spirv_1_5") }]
        };

        fixture.GlobalSession.CreateSession(sessionDesc, out var session).Succeeded.ShouldBeTrue();
        session.ShouldNotBeNull();
    }


    [Fact]
    public void FindProfile()
    {
        ((int)fixture.GlobalSession.FindProfile("glsl_450")).ShouldBe(1441792);
    }

    [Fact]
    public void SetDownstreamCompilerPath()
    {
        fixture.GlobalSession.SetDownstreamCompilerPath(SlangPassThrough.Dxc, "dxc123.exe");
    }

    [Fact]
    public void DownstreamCompilerPrelude()
    {
        fixture.GlobalSession.SetDownstreamCompilerPrelude(SlangPassThrough.Dxc, "Hello World");

        fixture.GlobalSession.GetDownstreamCompilerPrelude(SlangPassThrough.Dxc, out var prelude);
        prelude.AsString.ShouldBe("Hello World");
    }

    [Fact]
    public void GetBuildTagString()
    {
        fixture.GlobalSession.GetBuildTagString().ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void SetDefaultDownstreamCompiler()
    {
        fixture.GlobalSession.SetDefaultDownstreamCompiler(SlangSourceLanguage.Cpp, SlangPassThrough.Llvm).ShouldBe(SlangResult.SLANG_OK);
    }

    [Fact]
    public void GetDefaultDownstreamCompiler()
    {
        fixture.GlobalSession.SetDefaultDownstreamCompiler(SlangSourceLanguage.Hlsl, SlangPassThrough.Dxc).ShouldBe(SlangResult.SLANG_OK);

        fixture.GlobalSession.GetDefaultDownstreamCompiler(SlangSourceLanguage.Hlsl).ShouldBe(SlangPassThrough.Dxc);
    }

    [Fact]
    public void LanguagePrelude()
    {
        fixture.GlobalSession.SetLanguagePrelude(SlangSourceLanguage.Wgsl, "Hello WGSL");
        fixture.GlobalSession.GetLanguagePrelude(SlangSourceLanguage.Wgsl, out var wgslPrelude);

        wgslPrelude.AsString.ShouldBe("Hello WGSL");
    }

    [Fact]
    public void CreateCompileRequest()
    {
        // TODO: Uncomment once bug is fixed in slang: https://github.com/shader-slang/slang/issues/10009
        //fixture.GlobalSession.CreateCompileRequest(out var compileRequest).ShouldBe(SlangResult.SLANG_OK);
        //compileRequest.ShouldNotBeNull();
    }

    [Fact]
    public void AddBuiltins()
    {
        //fixture.GlobalSession.AddBuiltins("my_builtins.slang", "my_builtins.hlsl");
    }

    [Fact]
    public void SharedLibraryLoader()
    {
        var myLoader = new DummyLoader();

        fixture.GlobalSession.SetSharedLibraryLoader(myLoader);

        var nativeLoader = fixture.GlobalSession.GetSharedLibraryLoader();
        nativeLoader.ShouldBe(myLoader);
    }
}

[GeneratedComClass]
public partial class DummyLoader : ISlangSharedLibraryLoader
{
    public SlangResult LoadSharedLibrary(string path, out ISlangSharedLibrary sharedLibrary)
    {
        sharedLibrary = new DummyLibrary();
        return SlangResult.SLANG_OK;
    }
}

[GeneratedComClass]
public partial class DummyLibrary : ISlangSharedLibrary
{
    public unsafe void* CastAs(Guid guid)
    {
        return null;
    }

    public nint FindFuncByName(string name)
    {
        return nint.Zero;
    }

    public nint FindSymbolAddressByName(string name)
    {
        return nint.Zero;
    }
}