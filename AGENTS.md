# AGENTS.md

Fully-managed C# bindings for the [Slang](https://github.com/shader-slang/slang) shader compiler, built on source-generated COM wrappers (`[GeneratedComInterface]`) and `[LibraryImport]`. Targets `net8.0;net10.0`; trimmable / AOT-safe; warnings are errors.

**Read [`CONTRIBUTING.md`](CONTRIBUTING.md) first** — it holds the context shared by humans and agents: project layout, build/test commands, the Slang update process, the native→managed type-width table, and the full **Binding Conventions**. The [`README.md`](README.md) covers end-user usage. This file only adds agent-specific working notes; it does not repeat that content.

## Golden rule

`headers/slang.h`, `headers/slang-deprecated.h`, and `headers/slang-image-format-defs.h` are the **source of truth**. Every enum, struct, and interface must match those headers exactly. When a task is "update/audit the bindings", diff the headers and reconcile field-by-field.

## Before you edit a binding

Confirm against the header, because these fail silently at runtime (not compile time):

- **COM interfaces** — method declaration order = vtable order (append new methods at the end, never insert); `[Guid]` matches `SLANG_COM_INTERFACE(...)`; base interface matches native (`: ISlangCastable`, `: IComponentType`, …).
- **Type widths** — see the Conversion Types table in CONTRIBUTING.md. Watch `SlangInt`→`nint`, `SlangUInt`→`nuint`, and the C `long` = 32-bit-on-Windows caveat.
- **ABI structs** — leading `structSize`/`structureSize` field present, populated with `sizeof`, and every field copied in both marshaller directions.
- **Strings** — Slang-owned `const char*` returns use `NoFreeUtf8StringMarshaller`.

## Workflow

- Build/test: `dotnet build src/SlangShaderSharp.csproj` and `dotnet test` (see CONTRIBUTING.md). Keep it warning-clean — `TreatWarningsAsErrors` is on.
- After changing a P/Invoke signature or a public method, grep for call sites (incl. `tests/`) and update them.
- Native update: `pwsh ./update_slang.ps1 -Version <x>` syncs binaries **and** headers together; managed bindings are then reconciled by hand.
- Do not edit anything under `runtimes/` (native binaries) or `headers/` (vendored) by hand — those come from the update script.

## Gotcha

`loadModuleFromSource` needs a non-null source blob (native asserts); `path` is diagnostics/import-base only and is not read from disk. Read a file into a blob, or use `LoadModule`.
