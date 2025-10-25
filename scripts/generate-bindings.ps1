dotnet ClangSharpPInvokeGenerator `
    --config `
        preview-codegen `
        multi-file `
        generate-file-scoped-namespaces `
        generate-helper-types `
        generate-disable-runtime-marshalling `
        generate-macro-bindings `
        log-potential-typedef-remappings `
        log-exclusions `
        strip-enum-member-type-name `
        exclude-empty-records `
        generate-cpp-attributes `
        generate-template-bindings `
        generate-aggressive-inlining `
        generate-vtbl-index-attribute `
        generate-guid-member `
    --file .\headers\slang.h `
    --namespace SlangShaderSharp.Native `
    --libraryPath wgpu_native.dll `
    --output .\SlangShaderSharp\Native