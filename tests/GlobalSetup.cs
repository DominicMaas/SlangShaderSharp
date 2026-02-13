using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SlangShaderSharp.Tests;

public class GlobalSetup
{
    [ModuleInitializer]
    public static void Initialize()
    {
        // Replace 'MyNativeMethods' with a class in the project containing your [DllImport]s
        var targetAssembly = typeof(Slang).Assembly;
        NativeLibrary.SetDllImportResolver(targetAssembly, CustomResolver);
    }

    private static IntPtr CustomResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        var arch = RuntimeInformation.ProcessArchitecture.ToString().ToLower(); // e.g. "x64"
        var os = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "win" :
                       RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "linux" : "osx";


        var fileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? $"{libraryName}.dll" :
                             RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? $"lib{libraryName}.so" : $"lib{libraryName}.dylib";

        var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{os}-{arch}", "native", fileName);
        if (File.Exists(fullPath) && NativeLibrary.TryLoad(fullPath, out var handle))
        {
            return handle;
        }

        return IntPtr.Zero;
    }
}
