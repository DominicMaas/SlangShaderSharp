using Shouldly;

namespace SlangShaderSharp.Tests;

public class SlangResultTests
{
    [Fact]
    public void SlangResult_Equality_Works()
    {
        var result1 = new SlangResult(0);
        var result2 = new SlangResult(0);
        var result3 = SlangResult.SLANG_FAIL;

        result1.ShouldBe(result2);
        result1.ShouldNotBe(result3);
        result1.Equals(result2).ShouldBeTrue();
        result1.Equals(result3).ShouldBeFalse();
        (result1 == result2).ShouldBeTrue();
        (result1 != result3).ShouldBeTrue();
    }

    [Fact]
    public void SlangResult_SucceededAndFailed_Works()
    {
        var successResult = new SlangResult(0);
        var failureResult = SlangResult.SLANG_FAIL;
        successResult.Succeeded.ShouldBeTrue();
        successResult.Failed.ShouldBeFalse();
        failureResult.Succeeded.ShouldBeFalse();
        failureResult.Failed.ShouldBeTrue();
    }

    [Fact]
    public void SlangResult_FacilityAndCode_Works()
    {
        var result = new SlangResult((1 << 16) | 42); // Facility 1, Code 42
        result.GetFacility().ShouldBe(1);
        result.GetCode().ShouldBe(42);
    }

    [Fact]
    public void SlangResult_ImplicitAndExplicitConversion_Works()
    {
        int intValue = 12345;
        SlangResult resultFromInt = (SlangResult)intValue;
        int intFromResult = resultFromInt;
        intFromResult.ShouldBe(intValue);
    }

    [Fact]
    public void SlangResult_StaticFields_Works()
    {
        SlangResult.SLANG_OK.Failed.ShouldBeFalse();

        SlangResult.SLANG_FAIL.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_NOT_IMPLEMENTED.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_NO_INTERFACE.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_ABORT.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_INVALID_HANDLE.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_INVALID_ARG.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_OUT_OF_MEMORY.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_BUFFER_TOO_SMALL.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_UNINITIALIZED.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_PENDING.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_CANNOT_OPEN.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_NOT_FOUND.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_INTERNAL_FAIL.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_NOT_AVAILABLE.Failed.ShouldBeTrue();
        SlangResult.SLANG_E_TIME_OUT.Failed.ShouldBeTrue();
    }
}
