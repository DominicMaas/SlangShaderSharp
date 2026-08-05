# Contributing to SlangShaderSharp

SlangShaderSharp is a set of fully-managed C# bindings for the [Slang](https://github.com/shader-slang/slang) shader compiler, built on source-generated COM wrappers (`[GeneratedComInterface]`) and `[LibraryImport]`.

The vendored headers in `headers/` (`slang.h`, `slang-deprecated.h`, `slang-image-format-defs.h`) are the **source of truth for every binding**. When writing or reviewing a binding, match the header exactly.

## Project Layout

| Path | Contents |
| --- | --- |
| `src/Enums/` | Enum bindings (`enum X : uint`, values mirror native) |
| `src/Structs/` | Plain data structs + their custom marshallers |
| `src/Descriptions/` | `*Desc` config structs passed into native (e.g. `SessionDesc`, `TargetDesc`) |
| `src/Reflection/` | Opaque-handle reflection wrappers over the `spReflection*` C API |
| `src/I*.cs` | COM interfaces (`[GeneratedComInterface]`), one per native `struct IXxx` |
| `src/Internal/` | Marshalling helpers (e.g. `NoFreeUtf8StringMarshaller`) |
| `src/Slang.cs` | Static entry point: global `slang_*` free functions, `CreateBlob`, `ApiVersion`, `Shutdown`, `LibraryName` |
| `headers/` | Vendored Slang C headers — **the source of truth for all bindings** |
| `runtimes/<rid>/native/` | Native `slang-compiler` + `slang-glslang` binaries, one folder per RID |
| `tests/` | xUnit + Shouldly tests; `tests/Assets/` holds `.slang` fixtures copied to output |

Supported RIDs: `win-x64`, `win-arm64`, `linux-x64`, `linux-arm64`, `osx-x64`, `osx-arm64`.

## Building and Testing

```bash
dotnet build src/SlangShaderSharp.csproj
dotnet test
```

The library multi-targets `net8.0;net10.0` and is trimmable / AOT-compatible with `TreatWarningsAsErrors` on, so bindings must be trim/AOT-safe and warning-clean. The test project has `InternalsVisibleTo` access.

## Updating Slang

Native binaries and the vendored headers are updated **together** so they always match:

```bash
pwsh ./update_slang.ps1 -Version <slang-version>
```

This downloads all six platform release zips, copies the binaries into `runtimes/` and the headers into `headers/`. The `.github/workflows/update_slang.yml` workflow (manual dispatch) runs this and opens a `slang-update/<version>` PR. **After the headers change, the managed bindings must be reconciled by hand** — diff `headers/*.h` and update the affected enums, structs, and interfaces to match (see conventions below). Do not hand-edit anything under `runtimes/` or `headers/`; those come from the update script.

## Conversion Types

Native type widths map to managed types as follows. Getting a width wrong silently corrupts the ABI, so this table is the reference when writing or reviewing a binding:

| Native | Managed |
| --- | --- |
| `SlangInt32` / `int32_t` | `int` |
| `SlangUInt32` / `uint32_t` | `uint` |
| `int64_t` | `long` |
| `uint64_t` | `ulong` |
| `SlangInt` / `SlangSSizeT` | `nint` (pointer-width) |
| `SlangUInt` / `SlangSizeT` / `size_t` | `nuint` (pointer-width) |
| `bool` / `SlangBool` | 1 byte — `[MarshalAs(UnmanagedType.I1)] bool` or `byte` |
| `const char*` | UTF-8 `string` (see string ownership below) |
| opaque `SlangReflection*` etc. | handle struct (`nint`) or COM interface |

> **Caveat — C `long`:** the C `long` type is **32-bit on Windows (MSVC)** but **64-bit on Linux/macOS (LP64)**. There is no single correct managed width; bind it as `int` (reads the low register, correct on Windows and fine for values that fit in 32 bits).

## Binding Conventions

These invariants keep the managed bindings ABI-compatible with the native library. The vendored `headers/*.h` are the source of truth; when in doubt, match the header exactly. Most of these fail **silently at runtime** rather than at compile time.

- **COM vtable = declaration order.** For a `[GeneratedComInterface]`, the order methods are declared *is* the vtable layout. Never reorder or insert a method in the middle — append new native methods at the end, in native order. The `[Guid]` must exactly equal the native `SLANG_COM_INTERFACE(...)`, and the interface's **base** (`: ISlangCastable`, `: IComponentType`, …) must match the native `struct : public IParent` so the vtable prefix lines up. Every method is `[PreserveSig]`.
- **Enums** mirror the native backing width (`enum X : uint` for `uint32_t`/`SlangUInt32`), every explicit value must match, and terminal `CountOf`/`Count` sentinels are kept.
- **Type widths** follow the Conversion Types table above. Mind the C `long` caveat (32-bit on Windows, 64-bit on Linux/macOS — bind as `int`).
- **String ownership.** Slang-owned `const char*` returns must use `[return: MarshalUsing(typeof(NoFreeUtf8StringMarshaller))]` so the marshaller does not free native memory; input strings use `StringMarshalling.Utf8`.
- **ABI-versioned structs** begin with a `size_t structSize`/`structureSize` field. The C# unmanaged mirror must carry the leading `nuint` field, populate it with `sizeof(<unmanaged>)`, keep field order identical to native, and copy every field in **both** marshaller directions.
- **Reflection wrappers** are `readonly struct` opaque handles (`nint Handle`) with a custom handle marshaller; P/Invokes use the exact `spReflection*` entry-point name plus `[LibraryImport(LibraryName)]` + `[UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]`.
- **`loadModuleFromSource` requires a non-null source blob** (native asserts otherwise). Its `path` argument is used only for diagnostics and as the base directory for resolving `import`s — it is **not** read from disk. To load from a file, read it into a blob first, or use `LoadModule` (by name, via search paths).

After changing a P/Invoke signature or a public method, grep for call sites (including `tests/`) and update them.
