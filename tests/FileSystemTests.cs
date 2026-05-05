using Shouldly;
using SlangShaderSharp.Tests.Support;
using System.Runtime.InteropServices.Marshalling;

namespace SlangShaderSharp.Tests;

[Collection("GlobalSession")]
public class FileSystemTests(GlobalSessionFixture fixture)
{
    [Fact]
    public void TestFileSystem()
    {
        // This should be kept alive and not GCed until the session is destroyed?
        // Keeping it alongside the ISession should be fine
        var fileSystem = new MyFileSystem();

        fixture.GlobalSession.CreateSession(new SessionDesc
        {
            FileSystem = fileSystem,
            Targets = [
                new TargetDesc
                {
                    Format = SlangCompileTarget.Hlsl
                }
            ]
        }, out var mySession).ShouldBe(SlangResult.SLANG_OK);
        mySession.ShouldNotBeNull();

        // Use the compile request API, this takes in defaults from the session description (including the file system)
        mySession.CreateCompileRequest(out var request).ShouldBe(SlangResult.SLANG_OK);
        request.ShouldNotBeNull();

        // Add a translation unit with a source file that the file system can handle
        var testIndex = request.AddTranslationUnit(SlangSourceLanguage.Slang, "TestTranslationUnit");
        request.AddTranslationUnitSourceFile(testIndex, "VFS/MyShader.slang");

        // Set our entry point
        request.AddEntryPoint(testIndex, "computeMain", SlangStage.Compute);

        // Compile
        request.Compile().ShouldBe(SlangResult.SLANG_OK, request.GetDiagnosticOutput());

        // These helper methods are provided by ICompileRequestExtensions

        // Get the compiled result as a byte span, this does not allocate any memory on the managed side, it
        // is just a view into the memory owned by the compile request, which should be valid until the compile request is destroyed
        request.GetCompileRequestCodeAsSpan().IsEmpty.ShouldBeFalse();

        // Get the compiled result as a string, this will allocate a new string on the managed side and copy the data from the
        // compile request, so it should be used with care
        request.GetCompileRequestCodeAsUtf8String().ShouldNotBeNullOrWhiteSpace();
    }
}

[GeneratedComClass]
public partial class MyFileSystem : ISlangFileSystem
{
    public unsafe void* CastAs(Guid guid)
    {
        /*
         * Return null - CastAs should not increment ref count
         * ComInterfaceMarshaller<T>.ConvertToUnmanaged(this) will, so just leave it until something breaks!
         * This seems to be used by the internal slang library to check if the file system supports certain internal interfaces?
         *
         * In theory the following should work without incrementing the ref count, but I have not tested.
         *
         *  if (guid == typeof(ISlangFileSystem).GUID)
         *  {
         *      if (ComWrappers.TryGetComInstance(this, out var ptr))
         *      {
         *          return ptr.ToPointer();
         *      }
         *  }
         *
         *  return null
         */

        return null;
    }

    public SlangResult LoadFile(string path, out ISlangBlob outBlob)
    {
        // Here is a virtual module
        if (string.Equals(path, "VFS/MyModule.slang", StringComparison.OrdinalIgnoreCase))
        {
            outBlob = Slang.CreateBlob("""
                module MyModule;

                public int foo(int a)
                {
                    return a + 1;
                }
            """u8);

            return SlangResult.SLANG_OK;
        }

        // Here is the main slang shader that imports the virtual module
        if (string.Equals(path, "VFS/MyShader.slang", StringComparison.OrdinalIgnoreCase))
        {
            outBlob = Slang.CreateBlob("""
                import MyModule;

                RWStructuredBuffer<int> outputBuffer;

                [shader("compute")]
                [numthreads(4, 1, 1)]
                void computeMain(uint3 dispatchThreadID : SV_DispatchThreadID)
                {
                    int index = (int)dispatchThreadID.x;
                    outputBuffer[index] = foo(index);
                }
            """u8);

            return SlangResult.SLANG_OK;
        }

        // Unknown file, return not found
        outBlob = null!;
        return SlangResult.SLANG_E_NOT_FOUND;
    }
}
