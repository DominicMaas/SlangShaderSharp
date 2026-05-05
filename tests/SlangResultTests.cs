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

        Equals(result1, result2).ShouldBeTrue();
        Equals(result1, result3).ShouldBeFalse();

        Equals(result1, 12345).ShouldBeFalse();
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

    [Fact]
    public void SlangResult_ToString_Works()
    {
        var result = new SlangResult(0);
        result.ToString().ShouldBe("SLANG_OK (0x00000000)");
        result = SlangResult.SLANG_FAIL;
        result.ToString().ShouldBe("SLANG_FAIL (0x80004005)");
        result = new SlangResult(0x12345678);
        result.ToString().ShouldBe("0x12345678");
    }

    [Fact]
    public void SlangResult_GetSymbolicName_Works()
    {
        SlangResult.SLANG_OK.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_OK));
        SlangResult.SLANG_FAIL.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_FAIL));
        SlangResult.SLANG_E_NOT_IMPLEMENTED.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_NOT_IMPLEMENTED));
        SlangResult.SLANG_E_NO_INTERFACE.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_NO_INTERFACE));
        SlangResult.SLANG_E_ABORT.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_ABORT));
        SlangResult.SLANG_E_INVALID_HANDLE.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_INVALID_HANDLE));
        SlangResult.SLANG_E_INVALID_ARG.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_INVALID_ARG));
        SlangResult.SLANG_E_OUT_OF_MEMORY.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_OUT_OF_MEMORY));
        SlangResult.SLANG_E_BUFFER_TOO_SMALL.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_BUFFER_TOO_SMALL));
        SlangResult.SLANG_E_UNINITIALIZED.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_UNINITIALIZED));
        SlangResult.SLANG_E_PENDING.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_PENDING));
        SlangResult.SLANG_E_CANNOT_OPEN.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_CANNOT_OPEN));
        SlangResult.SLANG_E_NOT_FOUND.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_NOT_FOUND));
        SlangResult.SLANG_E_INTERNAL_FAIL.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_INTERNAL_FAIL));
        SlangResult.SLANG_E_NOT_AVAILABLE.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_NOT_AVAILABLE));
        SlangResult.SLANG_E_TIME_OUT.GetSymbolicName().ShouldBe(nameof(SlangResult.SLANG_E_TIME_OUT));
        new SlangResult(0x12345678).GetSymbolicName().ShouldBeNull();
    }

    [Fact]
    public void SlangResult_TestHashCode_Works()
    {
        var result1 = new SlangResult(0);
        var result2 = new SlangResult(0);
        var result3 = SlangResult.SLANG_FAIL;
        result1.GetHashCode().ShouldBe(result2.GetHashCode());
        result1.GetHashCode().ShouldNotBe(result3.GetHashCode());
    }

    [Fact]
    public void SlangResultMarshaller_TestRaw()
    {
        var result = new SlangResult(0x12345678);
        int intValue = result;
        intValue.ShouldBe(0x12345678);
        SlangResult resultFromInt = (SlangResult)intValue;
        resultFromInt.ShouldBe(result);
    }

    [Fact]
    public void SlangResultMarshaller_TestCuston()
    {
        var result = new SlangResult(0x12345678);
        int intValue = SlangResultMarshaller.ConvertToUnmanaged(result);
        intValue.ShouldBe(0x12345678);
        SlangResult resultFromInt = SlangResultMarshaller.ConvertToManaged(intValue);
        resultFromInt.ShouldBe(result);
    }
}
