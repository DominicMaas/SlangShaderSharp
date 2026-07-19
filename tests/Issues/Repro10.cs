using SlangShaderSharp.Tests.Support;

namespace SlangShaderSharp.Tests.Issues;

[Collection("GlobalSession")]
public class Repro10(GlobalSessionFixture fixture)
{
    [Fact]
    public void ReproIssue()
    {
        for (var i = 0; i < 100; i++)
        {
            _ = CompileShaders("MyShader", "Assets/MyShader.slang");

            GC.Collect();
        }
    }

    public byte[] CompileShaders(string moduleName, string fileName)
    {
        var sessionDesc = new SessionDesc
        {
            Targets = [new TargetDesc { Format = SlangCompileTarget.Spirv, Profile = fixture.GlobalSession.FindProfile("spirv_1_4") }],
            DefaultMatrixLayoutMode = SlangMatrixLayoutMode.RowMajor,
            CompilerOptionEntries = [
                new(CompilerOptionName.EmitSpirvDirectly, CompilerOptionValue.FromInt(1)),
                new(CompilerOptionName.MatrixLayoutRow, CompilerOptionValue.FromEnum(SlangMatrixLayoutMode.RowMajor)),
                new(CompilerOptionName.DownstreamArgs, CompilerOptionValue.FromString("Test")),
            ],
        };

        fixture.GlobalSession.CreateSession(sessionDesc, out var session).Check();

        var source = Slang.CreateBlob(File.ReadAllBytes(fileName));
        var module = session.LoadModuleFromSource(moduleName, fileName, source, out _)!;
        module!.GetTargetCode(0, out var spirv, out _).Check();

        return spirv.Buffer.ToArray();
    }
}

public static class SlangResultExtensions
{
    public static void Check(this SlangResult result)
    {
        if (result != SlangResult.SLANG_OK)
        {
            throw new Exception($"Slang operation failed with result: {result}");
        }
    }
}