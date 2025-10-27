using Shouldly;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace SlangShaderSharp.Tests;

public class UnitTest1
{
    [Fact]
    public void TestUsingComWrappers()
    {
        Slang.CreateGlobalSession(0, out var globalSession);

        ((int)globalSession.FindProfile("glsl_450")).ShouldBe(1441792);
    }

    [Fact]
    public unsafe void TestGlobalAndLocalSession()
    {
        Slang.CreateGlobalSession(0, out var globalSession);

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

        globalSession.CreateSession(sessionDesc, out _).ShouldBe(0);
    }

    [Fact]
    public unsafe void TestModuleLoad()
    {
        // 1. Create Global Session

        Slang.CreateGlobalSession(0, out var globalSession);

        // 2. Create Session

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

        globalSession.CreateSession(sessionDesc, out var session).ShouldBe(0);

        // 3. Load module

        var shader = """
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
            """u8;
        using var shaderSrc = new MyTestSource(shader);
        var module = session.LoadModuleFromSource("test", "test.slang", shaderSrc, out _);

        // 4. Query Entry Points

        module.FindEntryPointByName("computeMain", out var entryPoint).ShouldBe(0);

        // 5. Compose Modules + Entry Points

        ComWrappers.TryGetComInstance(module, out nint modulePointer).ShouldBeTrue();
        ComWrappers.TryGetComInstance(entryPoint, out nint entryPointPointer).ShouldBeTrue();

        var pointers = stackalloc nint[2];
        pointers[0] = modulePointer;
        pointers[1] = entryPointPointer;

        session.CreateCompositeComponentType(pointers, 2, out var composedProgram, out _).ShouldBe(0);

        // 6. Link

        composedProgram.Link(out var linkedProgram, out _).ShouldBe(0);

        // 7. Get Target Kernel Code

        composedProgram.GetEntryPointCode(0, 0, out var spirvCode, out _).ShouldBe(0);

        var codeSpan = new ReadOnlySpan<byte>(spirvCode.GetBufferPointer(), (int)spirvCode.GetBufferSize());
        var codeString = Encoding.UTF8.GetString(codeSpan);
    }
}

/// <summary>
///     TODO: Gotta be a better way
/// </summary>
[GeneratedComClass]
public partial class MyTestSource : ISlangBlob, IDisposable
{
    private readonly byte[] _data;
    private GCHandle _handle;

    public MyTestSource(ReadOnlySpan<byte> data)
    {
        _data = data.ToArray();
        _handle = GCHandle.Alloc(_data, GCHandleType.Pinned);
    }

    public unsafe void* GetBufferPointer()
    {
        return (void*)_handle.AddrOfPinnedObject();
    }

    public nuint GetBufferSize()
    {
        return (nuint)_data.Length;
    }

    public void Dispose()
    {
        if (_handle.IsAllocated)
        {
            _handle.Free();
        }
    }
}
