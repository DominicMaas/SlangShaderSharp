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
        // 1. Determine the folder based on architecture and OS
        string arch = RuntimeInformation.ProcessArchitecture.ToString().ToLower(); // e.g. "x64"
        string os = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "win" : "linux";
        string ridFolder = $"{os}-{arch}"; // e.g. "win-x64"

        // 2. Add platform extension (.dll, .so, etc.)
        string fileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? $"{libraryName}.dll" : $"lib{libraryName}.so";

        // 3. Combine with the base directory
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ridFolder, "native", fileName);

        if (File.Exists(fullPath) && NativeLibrary.TryLoad(fullPath, out var handle))
        {
            return handle;
        }
        return IntPtr.Zero;
    }
}
