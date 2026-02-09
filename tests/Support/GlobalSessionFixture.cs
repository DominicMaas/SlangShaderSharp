using Shouldly;

namespace SlangShaderSharp.Tests.Support;

public sealed class GlobalSessionFixture : IDisposable
{
    public IGlobalSession GlobalSession { get; }

    public object GlobalLock { get; } = new();

    public GlobalSessionFixture()
    {
        Slang.CreateGlobalSession(Slang.ApiVersion, out var globalSession).ShouldBe(SlangResult.SLANG_OK);
        GlobalSession = globalSession;
    }

    public void Dispose()
    {
        Slang.Shutdown();
    }
}

[CollectionDefinition("GlobalSession", DisableParallelization = true)]
public class GlobalSessionCollection : ICollectionFixture<GlobalSessionFixture>
{ }