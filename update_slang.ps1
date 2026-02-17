param(
    [Parameter(Mandatory=$true)]
    [string]$Version
)

mkdir $env:TEMP\slang

Invoke-WebRequest -Uri "https://github.com/shader-slang/slang/releases/download/v$($Version)/slang-$($Version)-windows-aarch64.zip"   -OutFile "$env:TEMP\slang\windows-aarch64.zip"
Invoke-WebRequest -Uri "https://github.com/shader-slang/slang/releases/download/v$($Version)/slang-$($Version)-windows-x86_64.zip"    -OutFile "$env:TEMP\slang\windows-x86_64.zip"
Invoke-WebRequest -Uri "https://github.com/shader-slang/slang/releases/download/v$($Version)/slang-$($Version)-macos-x86_64.zip"      -OutFile "$env:TEMP\slang\macos-x86_64.zip"
Invoke-WebRequest -Uri "https://github.com/shader-slang/slang/releases/download/v$($Version)/slang-$($Version)-macos-aarch64.zip"     -OutFile "$env:TEMP\slang\macos-aarch64.zip"
Invoke-WebRequest -Uri "https://github.com/shader-slang/slang/releases/download/v$($Version)/slang-$($Version)-linux-aarch64.zip"     -OutFile "$env:TEMP\slang\linux-aarch64.zip"
Invoke-WebRequest -Uri "https://github.com/shader-slang/slang/releases/download/v$($Version)/slang-$($Version)-linux-x86_64.zip"      -OutFile "$env:TEMP\slang\linux-x86_64.zip"

# Expand Archives
Expand-Archive -Path "$env:TEMP\slang\windows-aarch64.zip"  -DestinationPath "$env:TEMP\slang\windows-aarch64"  -Force
Expand-Archive -Path "$env:TEMP\slang\windows-x86_64.zip"   -DestinationPath "$env:TEMP\slang\windows-x86_64"   -Force
Expand-Archive -Path "$env:TEMP\slang\macos-x86_64.zip"     -DestinationPath "$env:TEMP\slang\macos-x86_64"     -Force
Expand-Archive -Path "$env:TEMP\slang\macos-aarch64.zip"    -DestinationPath "$env:TEMP\slang\macos-aarch64"    -Force
Expand-Archive -Path "$env:TEMP\slang\linux-aarch64.zip"    -DestinationPath "$env:TEMP\slang\linux-aarch64"    -Force
Expand-Archive -Path "$env:TEMP\slang\linux-x86_64.zip"     -DestinationPath "$env:TEMP\slang\linux-x86_64"     -Force

# Copy binaries to destination folders
Copy-Item -Path "$env:TEMP\slang\windows-aarch64\bin\slang-compiler.dll"                  -Destination ".\runtimes\win-arm64\native\slang-compiler.dll"        -Force
Copy-Item -Path "$env:TEMP\slang\windows-x86_64\bin\slang-compiler.dll"                   -Destination ".\runtimes\win-x64\native\slang-compiler.dll"          -Force
Copy-Item -Path "$env:TEMP\slang\macos-x86_64\lib\libslang-compiler.0.$($Version).dylib"  -Destination ".\runtimes\osx-x64\native\libslang-compiler.dylib"     -Force
Copy-Item -Path "$env:TEMP\slang\macos-aarch64\lib\libslang-compiler.0.$($Version).dylib" -Destination ".\runtimes\osx-arm64\native\libslang-compiler.dylib"   -Force
Copy-Item -Path "$env:TEMP\slang\linux-aarch64\lib\libslang-compiler.so.0.$($Version)"    -Destination ".\runtimes\linux-arm64\native\libslang-compiler.so"    -Force
Copy-Item -Path "$env:TEMP\slang\linux-x86_64\lib\libslang-compiler.so.0.$($Version)"     -Destination ".\runtimes\linux-x64\native\libslang-compiler.so"      -Force

# Copy Header Files
Copy-Item -Path "$env:TEMP\slang\windows-x86_64\include\slang.h"                      -Destination ".\headers\slang.h"                     -Force
Copy-Item -Path "$env:TEMP\slang\windows-x86_64\include\slang-deprecated.h"           -Destination ".\headers\slang-deprecated.h"          -Force
Copy-Item -Path "$env:TEMP\slang\windows-x86_64\include\slang-image-format-defs.h"    -Destination ".\headers\slang-image-format-defs.h"   -Force
