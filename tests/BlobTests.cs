using Shouldly;

namespace SlangShaderSharp.Tests;

public class BlobTests
{
    [Fact]
    public void TestCreateString()
    {
        Slang.CreateGlobalSession(0, out var globalSession);

        var blob = Slang.CreateBlob("Hello World!");
        blob.AsString.ShouldBe("Hello World!");

        Slang.Shutdown();
    }

    [Fact]
    public void TestCreateRaw()
    {
        Slang.CreateGlobalSession(0, out _);

        var myBytes = new ReadOnlySpan<byte>([255, 128, 255, 128]);

        var blob = Slang.CreateBlob(myBytes);
        blob.Buffer.SequenceEqual(myBytes).ShouldBeTrue();

        Slang.Shutdown();
    }

    [Fact]
    public void TestDataDropped()
    {
        Slang.CreateGlobalSession(0, out _);

        var myBytes = new ReadOnlySpan<byte>([255, 128, 255, 128]);

        var blob = Slang.CreateBlob(myBytes);
        AssertSame(blob);

        Slang.Shutdown();
    }

    private static void AssertSame(ISlangBlob blob)
    {
        blob.Buffer.Length.ShouldBe(4);

        blob.Buffer[0].ShouldBe((byte)255);
        blob.Buffer[1].ShouldBe((byte)128);
        blob.Buffer[2].ShouldBe((byte)255);
        blob.Buffer[3].ShouldBe((byte)128);
    }
}
