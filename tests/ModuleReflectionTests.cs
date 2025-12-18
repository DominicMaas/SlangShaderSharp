using Shouldly;
using System.Diagnostics;

namespace SlangShaderSharp.Tests;

public class ModuleReflectionTests
{
    [Fact]
    public void TestBasic()
    {
        // 1. Create Global Session

        Slang.CreateGlobalSession(Slang.ApiVersion, out var globalSession).Succeeded.ShouldBeTrue();

        // 2. Create Session

        var sessionDesc = new SessionDesc
        {
            Targets = [new TargetDesc { Format = SlangCompileTarget.Wgsl }],
        };

        globalSession.CreateSession(sessionDesc, out var session).Succeeded.ShouldBeTrue();

        // 3. Load module

        var module = session.LoadModuleFromSource("test", "test.slang", Slang.CreateBlob("""
            StructuredBuffer<float> buffer0;
            StructuredBuffer<float> buffer1;
            RWStructuredBuffer<float> result;

            [shader("compute")]
            [numthreads(1,1,1)]
            void computeMain(uint3 threadId : SV_DispatchThreadID)
            {
                uint index = threadId.x;
                result[index] = buffer0[index] + buffer1[index];
            }
            """u8), out var moduleLoadError);

        module.ShouldNotBeNull(moduleLoadError?.AsString ?? "Unknown Error");


        module.GetName().ShouldBe("test");
        module.GetFilePath().ShouldBe("test.slang");

        // 4. Get Module Reflection

        var reflectionModule = module.GetModuleReflection();
        reflectionModule.ShouldNotBe(DeclReflection.Null);

        reflectionModule.Name.ShouldBe("test");
        reflectionModule.Kind.ShouldBe(DeclReflectionKind.Module);

        reflectionModule.Count.ShouldBe(4);

        reflectionModule[0].Name.ShouldBe("buffer0");
        reflectionModule[0].Kind.ShouldBe(DeclReflectionKind.Variable);

        var buffer0Reflection = reflectionModule[0].AsVariable();
        buffer0Reflection.ShouldNotBe(VariableReflection.Null);

        reflectionModule[0].AsFunction().ShouldBe(FunctionReflection.Null);

        reflectionModule[1].Name.ShouldBe("buffer1");
        reflectionModule[1].Kind.ShouldBe(DeclReflectionKind.Variable);

        reflectionModule[2].Name.ShouldBe("result");
        reflectionModule[2].Kind.ShouldBe(DeclReflectionKind.Variable);

        var computeMain = reflectionModule[3];
        computeMain.Name.ShouldBe("computeMain");
        computeMain.Kind.ShouldBe(DeclReflectionKind.Func);

        computeMain.Count.ShouldBe(1);
        computeMain[0].Name.ShouldBe("threadId");
        computeMain[0].Kind.ShouldBe(DeclReflectionKind.Variable);

        var computeMainFunction = computeMain.AsFunction();
        computeMainFunction.ShouldNotBe(FunctionReflection.Null);

        computeMainFunction.ParameterCount.ShouldBe((uint)1);
        var param0 = computeMainFunction.GetParameter(0);
    }

    /// <summary>
    ///     Reflection tests for internal engine project, using a simplified shader layout
    /// </summary>
    [Fact]
    public void TestBuildLayout()
    {
        // 1. Create Global Session

        Slang.CreateGlobalSession(Slang.ApiVersion, out var globalSession).Succeeded.ShouldBeTrue();

        // 2. Create Session

        var sessionDesc = new SessionDesc
        {
            Targets = [new TargetDesc { Format = SlangCompileTarget.Wgsl }],
        };

        globalSession.CreateSession(sessionDesc, out var session).Succeeded.ShouldBeTrue();

        // 3. Load module

        var module = session.LoadModuleFromSource("test2", "test2.slang", Slang.CreateBlob("""
            // Vertex Data

            struct Vertex3DInput
            {
                uint instanceID : SV_InstanceID;
                float3 position : POSITION;
                float3 normal : NORMAL;
                float2 uv0 : TEXCOORD0;
                float4 color : COLOR0;
                float3 tangent : TANGENT0;
            };

            struct VertexOutput
            {
                float4 position : SV_Position;
                float3 normal;
                float2 uv0;
                float4 color;
                float4 worldPosition;
                float3 worldNormal;
                float3 worldTangent;
                float3 worldBiTangent;
            };

            // Mesh Data

            struct Mesh {
                float4x4 model;
            }

            //  Material Data

            struct MaterialProperties {
                float4 diffuseColor;
            }

            struct Material {
                ConstantBuffer<MaterialProperties> properties;
                Texture2D diffuseTexture;
                SamplerState diffuseSampler;
                Texture2D normalTexture;
                SamplerState normalSampler;
            }

            // Environment Data

            struct Camera {
                float4 viewPosition;
                float4x4 view;
                float4x4 projection;
            };

            struct Environment {
                ConstantBuffer<Camera> camera;
            }

            // Shadow Data
            struct ShadowMap {
                DepthTexture2D texture;
                SamplerComparisonState sampler;
            }

            ParameterBlock<Mesh> mesh;
            ParameterBlock<Environment> environment;
            ParameterBlock<Material> material;

            ParameterBlock<ShadowMap> shadowMap;

            [shader("vertex")]
            VertexOutput vertexMain(Vertex3DInput input) {
                VertexOutput output;

                output.normal = input.normal;
                output.position = mul(mul(float4(input.position, 1.0), mesh.model), mul(environment.camera.view, environment.camera.projection));

                return output;
            }

            [shader("fragment")]
            float4 fragmentMain(VertexOutput input) : SV_Target {
                return float4(normalize(input.normal), 1.0);
            }
            """u8), out var moduleLoadError);

        module.ShouldNotBeNull(moduleLoadError?.AsString ?? "Unknown Error");

        module.FindEntryPointByName("vertexMain", out var vertexEntryPoint).Succeeded.ShouldBeTrue();
        module.FindEntryPointByName("fragmentMain", out var fragmentEntryPoint).Succeeded.ShouldBeTrue();

        session.CreateCompositeComponentType([module, vertexEntryPoint, fragmentEntryPoint], out var composedProgram, out _).Succeeded.ShouldBeTrue();

        var layout = composedProgram.GetLayout(0, out var layoutError);
        layout.ShouldNotBe(ShaderReflection.Null, layoutError?.AsString ?? "Unknown Error");

        layout.ParameterCount.ShouldBe((uint)4);

        // Loop through all the parameters
        for (uint i = 0; i < layout.ParameterCount; i++)
        {
            var param = layout.GetParameterByIndex(i);
            param.ShouldNotBe(VariableLayoutReflection.Null);

            var paramType = param.TypeLayout.Type;
            paramType.ShouldNotBe(TypeReflection.Null);

            Debug.WriteLine($"Parameter: {param.Name} at binding {param.BindingIndex}, space {param.BindingSpace}");

            // Handle ParameterBlock
            if (paramType.Kind == SlangTypeKind.ParameterBlock)
            {
                var elementType = paramType.ElementType;
                var elementlayout = param.TypeLayout.ElementTypeLayout;

                // Iterate through fields in the parameter block
                for (uint j = 0; j < elementlayout.FieldCount; j++)
                {
                    var field = elementType.GetFieldByIndex(j);
                    var fieldLayout = elementlayout.GetFieldByIndex(j);
                    var fieldType = field.Type;

                    var binding = param.BindingIndex + fieldLayout.GetOffset();
                    Debug.WriteLine($"\tField: {field.Name} of type {fieldType.Kind} at binding {binding}");

                    switch (fieldType.Kind)
                    {
                        case SlangTypeKind.ConstantBuffer:
                            Debug.WriteLine($"\t\tUniform");
                            break;

                        case SlangTypeKind.Resource:
                            var shape = fieldType.ResourceShape;
                            var access = fieldType.ResourceAccess;
                            var scalarType = fieldType.ScalarType;


                            Debug.WriteLine($"\t\tResource: Shape: {shape}, Access: {access}, Scalar Type: {scalarType}");
                            break;

                        // Determine our type of sampler
                        case SlangTypeKind.SamplerState:

                            var flavor = fieldType.ResourceShape & SlangResourceShape.BaseShapeMask;


                            Debug.WriteLine($"\t\tSampler");
                            break;

                        default:
                            Debug.WriteLine($"\t\tUniform");
                            break;
                    }

                }
            }
        }

    }
}
