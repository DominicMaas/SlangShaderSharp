using Shouldly;
using SlangShaderSharp.Tests.Support;

namespace SlangShaderSharp.Tests;

[Collection("GlobalSession")]
public class BlobTests(GlobalSessionFixture fixture)
{
    [Fact]
    public void TestCreateString()
    {
        var blob = Slang.CreateBlob("Hello World!");
        blob.AsString.ShouldBe("Hello World!");
    }

    [Fact]
    public void TestCreateRaw()
    {
        var myBytes = new ReadOnlySpan<byte>([255, 128, 255, 128]);

        var blob = Slang.CreateBlob(myBytes);
        blob.Buffer.SequenceEqual(myBytes).ShouldBeTrue();
    }

    [Fact]
    public void TestDataDropped()
    {
        var myBytes = new ReadOnlySpan<byte>([255, 128, 255, 128]);

        var blob = Slang.CreateBlob(myBytes);
        AssertSame(blob);
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
