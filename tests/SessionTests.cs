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
            Targets = [new TargetDesc { Format = SlangCompileTarget.Dxil }]
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
}
