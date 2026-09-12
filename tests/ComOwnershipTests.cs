using Shouldly;
using SlangShaderSharp.Tests.Support;
using System.Text;

namespace SlangShaderSharp.Tests;

/// <summary>
///     Guards the reference-counting contract of every binding that hands back a COM interface it does
///     not own.
///     <para>
///         Slang splits into two groups. Most calls hand the caller a reference they own (the
///         implementation ends in <c>.detach()</c> or <c>addRef()</c>), and the generated stub must
///         release it. A handful instead return a pointer the callee keeps owning - <c>return
///         m_linkage;</c>, <c>return asExternal(getSessionImpl());</c>, <c>return asExternal(module);</c>
///         - and releasing those destroys an object Slang is still using. Those are the ones marked
///         <c>NoFreeComInterfaceMarshaller</c>, and the ones pinned here.
///     </para>
///     <para>
///         Which group a method belongs to is invisible at the call site AND invisible in
///         <c>slang.h</c>: <c>IModule* loadModule(...)</c> reads identically whether the implementation
///         ends in <c>asExternal(module)</c> or <c>asExternal(module.detach())</c>. Reviewing the header
///         diff on a Slang bump can never surface a change here, so the refcount is asserted directly.
///         The assertions are two-sided - they fire if a binding starts releasing a borrowed pointer
///         (count falls) and if Slang starts transferring one (count climbs).
///     </para>
/// </summary>
[Collection("GlobalSession")]
public class ComOwnershipTests
{
    private const string ShaderSource = """
        RWStructuredBuffer<int> outputBuffer;

        [shader("compute")]
        [numthreads(4, 1, 1)]
        void computeMain(uint3 dispatchThreadID : SV_DispatchThreadID)
        {
            outputBuffer[dispatchThreadID.x] = (int)dispatchThreadID.x;
        }
        """;

    /// <summary>
    ///     These tests read absolute refcounts, so they use a global session of their own rather than the
    ///     shared fixture - otherwise an ownership bug exercised by an unrelated test decides the result.
    /// </summary>
    private static IGlobalSession CreateGlobalSession()
    {
        Slang.CreateGlobalSession(Slang.ApiVersion, out var globalSession).ShouldBe(SlangResult.SLANG_OK);
        return globalSession;
    }

    private static ISession CreateSession(IGlobalSession globalSession, string[]? searchPaths = null)
    {
        globalSession.CreateSession(new SessionDesc
        {
            Targets = [new TargetDesc { Format = SlangCompileTarget.Spirv, Profile = globalSession.FindProfile("spirv_1_4") }],
            SearchPaths = searchPaths
        }, out var session).Succeeded.ShouldBeTrue();

        return session;
    }

    private static IModule LoadShader(ISession session, string moduleName)
    {
        var module = session.LoadModuleFromSource(moduleName, string.Empty, Slang.CreateBlob(ShaderSource), out var diagnostics);
        return module.ShouldNotBeNull(diagnostics?.AsString ?? "Unknown Error");
    }

    /// <summary>
    ///     Re-fetches <paramref name="module"/> through <paramref name="reload"/> a few times and requires
    ///     its reference count to sit still. Every binding here returns a module Slang already owns, so any
    ///     movement in either direction is the bug.
    /// </summary>
    private static void ShouldNotMoveTheReferenceCount(IModule module, string binding, Func<IModule?> reload)
    {
        var expected = ComRefCount.Read(module);

        try
        {
            // Call 1 is the caller's own load, which established the count above.
            for (var call = 2; call <= 4; call++)
            {
                reload().ShouldNotBeNull();

                ComRefCount.Read(module).ShouldBe(expected,
                    $"{binding} call {call} changed the module's reference count. Slang returns a borrowed " +
                    $"pointer here, so the return value needs NoFreeComInterfaceMarshaller<IModule>.");
            }
        }
        finally
        {
            // Hand back whatever a leak took, so the assertion above is what fails - not the teardown.
            ComRefCount.RestoreTo(module, expected);
        }
    }

    /// <summary>
    ///     <c>Linkage::getGlobalSession</c> is <c>return asExternal(getSessionImpl());</c> - a borrowed
    ///     pointer. Releasing it drops the global session's count by one per call, and the fourth call
    ///     dereferences freed memory.
    /// </summary>
    [Fact]
    public void GetGlobalSessionDoesNotConsumeAReference()
    {
        var globalSession = CreateGlobalSession();
        var session = CreateSession(globalSession);

        var expected = ComRefCount.Read(globalSession);

        try
        {
            for (var call = 1; call <= 3; call++)
            {
                session.GetGlobalSession().ShouldNotBeNull();

                ComRefCount.Read(globalSession).ShouldBe(expected,
                    $"GetGlobalSession() call {call} released the global session. Slang returns a borrowed " +
                    "pointer here, so the return value needs NoFreeComInterfaceMarshaller<IGlobalSession>.");
            }
        }
        finally
        {
            ComRefCount.RestoreTo(globalSession, expected);
        }
    }

    /// <summary>
    ///     <c>ComponentType::getSession</c> is <c>return m_linkage;</c> - a borrowed pointer, and the same
    ///     native object the caller already holds as <see cref="ISession"/>.
    /// </summary>
    [Fact]
    public void GetSessionDoesNotConsumeAReference()
    {
        var globalSession = CreateGlobalSession();
        var session = CreateSession(globalSession);
        var module = LoadShader(session, "MyShader");

        var expected = ComRefCount.Read(session);

        try
        {
            for (var call = 1; call <= 3; call++)
            {
                module.GetSession().ShouldNotBeNull();

                ComRefCount.Read(session).ShouldBe(expected,
                    $"IComponentType.GetSession() call {call} released the session. Slang returns a borrowed " +
                    "pointer here, so the return value needs NoFreeComInterfaceMarshaller<ISession>.");
            }
        }
        finally
        {
            ComRefCount.RestoreTo(session, expected);
        }
    }

    /// <summary>
    ///     <c>Linkage::loadModule</c> ends in <c>return asExternal(module);</c>, where <c>module</c> is a
    ///     <c>RefPtr</c> that drops its reference on the way out - nothing is transferred.
    /// </summary>
    [Fact]
    public void LoadModuleDoesNotMoveTheReferenceCount()
    {
        var session = CreateSession(CreateGlobalSession(), searchPaths: ["Assets/"]);

        var module = session.LoadModule("MyShader", out var diagnostics);
        module.ShouldNotBeNull(diagnostics?.AsString ?? "Unknown Error");

        ShouldNotMoveTheReferenceCount(module, "LoadModule()",
            () => session.LoadModule("MyShader", out _));
    }

    /// <summary>
    ///     <c>Linkage::loadModuleFromBlob</c> ends in <c>return asExternal(module.get());</c>, and returns
    ///     a bare <c>return loadedModule;</c> once the module is cached.
    /// </summary>
    [Fact]
    public void LoadModuleFromSourceDoesNotMoveTheReferenceCount()
    {
        var session = CreateSession(CreateGlobalSession());
        var source = Slang.CreateBlob(ShaderSource);

        var module = session.LoadModuleFromSource("MyShader", string.Empty, source, out var diagnostics);
        module.ShouldNotBeNull(diagnostics?.AsString ?? "Unknown Error");

        ShouldNotMoveTheReferenceCount(module, "LoadModuleFromSource()",
            () => session.LoadModuleFromSource("MyShader", string.Empty, source, out _));
    }

    /// <summary>
    ///     <c>Linkage::loadModuleFromSourceString</c> wraps the string in a blob and forwards straight to
    ///     <c>loadModuleFromSource</c>, so it borrows for exactly the same reason.
    /// </summary>
    [Fact]
    public void LoadModuleFromSourceStringDoesNotMoveTheReferenceCount()
    {
        var session = CreateSession(CreateGlobalSession());

        var module = session.LoadModuleFromSourceString("MyShader", string.Empty, ShaderSource, out var diagnostics);
        module.ShouldNotBeNull(diagnostics?.AsString ?? "Unknown Error");

        ShouldNotMoveTheReferenceCount(module, "LoadModuleFromSourceString()",
            () => session.LoadModuleFromSourceString("MyShader", string.Empty, ShaderSource, out _));
    }

    /// <summary>
    ///     The IR path through <c>loadModuleFromBlob</c>, which short-circuits to <c>return
    ///     loadedModule;</c> without even comparing a source digest.
    /// </summary>
    [Fact]
    public void LoadModuleFromIRBlobDoesNotMoveTheReferenceCount()
    {
        var globalSession = CreateGlobalSession();

        // Serialize from one session and load into another, so the IR load is a genuine first load
        // rather than a cache hit on the module it was serialized from.
        LoadShader(CreateSession(globalSession), "MyShader").Serialize(out var ir).Succeeded.ShouldBeTrue();

        var session = CreateSession(globalSession);

        var module = session.LoadModuleFromIRBlob("MyShader", string.Empty, ir, out var diagnostics);
        module.ShouldNotBeNull(diagnostics?.AsString ?? "Unknown Error");

        ShouldNotMoveTheReferenceCount(module, "LoadModuleFromIRBlob()",
            () => session.LoadModuleFromIRBlob("MyShader", string.Empty, ir, out _));
    }

    /// <summary>
    ///     <c>Linkage::getLoadedModule</c> is <c>return loadedModulesList[index].get();</c> - the most
    ///     obviously borrowed of the set, since it only ever reads out of a list the session owns.
    /// </summary>
    [Fact]
    public void GetLoadedModuleDoesNotMoveTheReferenceCount()
    {
        var session = CreateSession(CreateGlobalSession());
        var module = LoadShader(session, "MyShader");

        session.GetLoadedModuleCount().ShouldBe(1);

        ShouldNotMoveTheReferenceCount(module, "GetLoadedModule()",
            () => session.GetLoadedModule(0));
    }

    /// <summary>
    ///     <c>slang_loadModuleFromSource</c> is a thin wrapper over <c>session-&gt;loadModuleFromSource</c>,
    ///     so it inherits the borrow. This is the <see cref="Slang"/> free function, marshalled by
    ///     <c>LibraryImport</c> rather than the COM generator - a separate code path with the same contract.
    /// </summary>
    [Fact]
    public void SlangLoadModuleFromSourceDoesNotMoveTheReferenceCount()
    {
        var session = CreateSession(CreateGlobalSession());
        var source = Encoding.UTF8.GetBytes(ShaderSource);

        var module = Slang.LoadModuleFromSource(session, "MyShader", string.Empty, source, (nuint)source.Length, out var diagnostics);
        module.ShouldNotBeNull(diagnostics?.AsString ?? "Unknown Error");

        ShouldNotMoveTheReferenceCount(module, "Slang.LoadModuleFromSource()",
            () => Slang.LoadModuleFromSource(session, "MyShader", string.Empty, source, (nuint)source.Length, out _));
    }

    /// <summary>
    ///     <c>slang_loadModuleFromIRBlob</c>, the <c>LibraryImport</c> counterpart of the IR path.
    /// </summary>
    [Fact]
    public void SlangLoadModuleFromIRBlobDoesNotMoveTheReferenceCount()
    {
        var globalSession = CreateGlobalSession();

        LoadShader(CreateSession(globalSession), "MyShader").Serialize(out var ir).Succeeded.ShouldBeTrue();

        var session = CreateSession(globalSession);
        var irBytes = ir.Buffer.ToArray();

        var module = Slang.LoadModuleFromIRBlob(session, "MyShader", string.Empty, irBytes, (nuint)irBytes.Length, out var diagnostics);
        module.ShouldNotBeNull(diagnostics?.AsString ?? "Unknown Error");

        ShouldNotMoveTheReferenceCount(module, "Slang.LoadModuleFromIRBlob()",
            () => Slang.LoadModuleFromIRBlob(session, "MyShader", string.Empty, irBytes, (nuint)irBytes.Length, out _));
    }
}
