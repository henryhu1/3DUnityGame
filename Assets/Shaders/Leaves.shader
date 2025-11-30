Shader "Custom/Leaves"
{
    Properties
    {
        _Cutoff("Mask Clip Value", Float) = 0.6
        _Color("Color", Color) = (0.97,0.97,0.97,1)
        _Scale_Normal("Scale_Normal", Range(0,3)) = 0
        _WindScale("Wind Scale", Range(0,1)) = 0.3622508
        _WindPower("Wind Power", Range(0,0.5)) = 0.2506492
        _WindSpeed("Wind Speed", Range(0,1)) = 0.2327153
        _Wind_Size("Wind_Size", Range(0,1)) = 0.5
        _Ambient_Occlusion("Ambient_Occlusion", Range(0,3)) = 0
        _Smoothness_Power("Smoothness_Power", Range(-3,3)) = 0
        _Color_Tilling("Color_Tilling", Color) = (0.97,0.97,0.97,1)
        _Tilling_Color("Tilling_Color", Float) = 1.49
        _Tilling_Contrast("Tilling_Contrast", Float) = 1.49

        _Base_Color("Base_Color", 2D) = "white" {}
        _Mask("Mask", 2D) = "white" {}
        _Noise("Noise", 2D) = "black" {}
        [Normal]_Normal("Normal", 2D) = "bump" {}
    }

    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="TransparentCutout" "Queue"="AlphaTest+0" }
        Cull Off
        ZWrite On

        Pass
        {
            Name "UniversalForward"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            // URP includes (URP 17)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            // Textures (SRP macros)
            TEXTURE2D(_Base_Color);      SAMPLER(sampler_Base_Color);
            TEXTURE2D(_Mask);            SAMPLER(sampler_Mask);
            TEXTURE2D(_Noise);           SAMPLER(sampler_Noise);
            TEXTURE2D(_Normal);          SAMPLER(sampler_Normal);

            // Properties
            float _Cutoff;
            float4 _Color;
            float _Scale_Normal;
            float _WindScale;
            float _WindPower;
            float _WindSpeed;
            float _Wind_Size;
            float _Ambient_Occlusion;
            float _Smoothness_Power;
            float4 _Color_Tilling;
            float _Tilling_Color;
            float _Tilling_Contrast;

            float4 _Base_Color_ST;
            float4 _Mask_ST;
            float4 _Normal_ST;

            // --- Utility: simplex/perlin noise (copied & adjusted from original) ---
            float3 mod289(float3 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
            float2 mod289(float2 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
            float3 permute_f(float3 x) { return mod289(((x * 34.0) + 1.0) * x); }

            float snoise(float2 v)
            {
                const float4 C = float4(0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439);
                float2 i = floor(v + dot(v, C.yy));
                float2 x0 = v - i + dot(i, C.xx);
                float2 i1 = (x0.x > x0.y) ? float2(1.0, 0.0) : float2(0.0, 1.0);
                float4 x12 = x0.xyxy + C.xxzz;
                x12.xy -= i1;
                i = mod289(i);
                float3 p = permute_f(permute_f(i.y + float3(0.0, i1.y, 1.0)) + i.x + float3(0.0, i1.x, 1.0));
                float3 m = max(0.5 - float3(dot(x0, x0), dot(x12.xy, x12.xy), dot(x12.zw, x12.zw)), 0.0);
                m = m * m;
                m = m * m;
                float3 x = 2.0 * frac(p * C.www) - 1.0;
                float3 h = abs(x) - 0.5;
                float3 ox = floor(x + 0.5);
                float3 a0 = x - ox;
                m *= 1.79284291400159 - 0.85373472095314 * (a0 * a0 + h * h);
                float3 g;
                g.x = a0.x * x0.x + h.x * x0.y;
                g.yz = a0.yz * x12.xz + h.yz * x12.yw;
                return 130.0 * dot(m, g);
            }

            inline float4 CalculateContrast(float contrastValue, float4 colorTarget)
            {
                float t = 0.5 * (1.0 - contrastValue);
                return mul(float4x4(contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1), colorTarget);
            }

            // Triplanar sampling helper using SRP macros (LOD=0)
            float4 TriplanarSample2D(Texture2D tex, SamplerState samp, float3 worldPos, float3 worldNormal, float falloff, float2 tiling)
            {
                float3 projNormal = pow(abs(worldNormal), falloff);
                projNormal /= (projNormal.x + projNormal.y + projNormal.z) + 1e-5;
                float3 nsign = sign(worldNormal);

                float2 uvX = tiling * worldPos.zy * float2(nsign.x, 1.0);
                float2 uvY = tiling * worldPos.xz * float2(nsign.y, 1.0);
                float2 uvZ = tiling * worldPos.xy * float2(-nsign.z, 1.0);

                float4 xSample = SAMPLE_TEXTURE2D_LOD(tex, samp, uvX, 0);
                float4 ySample = SAMPLE_TEXTURE2D_LOD(tex, samp, uvY, 0);
                float4 zSample = SAMPLE_TEXTURE2D_LOD(tex, samp, uvZ, 0);

                return xSample * projNormal.x + ySample * projNormal.y + zSample * projNormal.z;
            }

            // Vertex input/output definitions
            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 worldNormal : TEXCOORD2;
                float3 worldTangent : TEXCOORD3;
                float3 worldBitangent : TEXCOORD4;
                float4 vcolor : COLOR;
                float4 mainLightShadowCoord : TEXCOORD5;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                // Object -> world
                float3 worldPos = TransformObjectToWorld(IN.positionOS).xyz;
                float3 worldNormal = TransformObjectToWorldNormal(IN.normalOS);
                float3 worldTangent = TransformObjectToWorldDir(IN.tangentOS.xyz);
                float tangentSign = IN.tangentOS.w;
                float3 worldBitangent = cross(worldNormal, worldTangent) * tangentSign;

                // Triplanar noise sample to drive wind
                float triplanarTiling = (0.1 + (_Wind_Size - 0.0) * (3.0 - 0.1));
                float4 triSample = TriplanarSample2D(_Noise, sampler_Noise, worldPos, worldNormal, 1.0, triplanarTiling);
                float2 triNoise = triSample.xx;

                // panner
                float2 panner = (_WindSpeed) * _Time.y + triNoise;

                // mask uv sample
                float2 uvMask = IN.uv * _Mask_ST.xy + _Mask_ST.zw;
                float4 maskSample = SAMPLE_TEXTURE2D_LOD(_Mask, sampler_Mask, uvMask, 0);

                // displacement in world space using noise texture
                float3 noiseSample = SAMPLE_TEXTURE2D_LOD(_Noise, sampler_Noise, panner * _WindScale, 0).rgb;
                float3 displacementWS = (noiseSample * _WindPower) * maskSample.a;

                float3 displacedWorldPos = worldPos + displacementWS;

                OUT.positionCS = TransformWorldToHClip(float4(displacedWorldPos, 1.0));
                OUT.uv = IN.uv;
                OUT.worldPos = displacedWorldPos;
                OUT.worldNormal = normalize(worldNormal);
                OUT.worldTangent = normalize(worldTangent);
                OUT.worldBitangent = normalize(worldBitangent);
                OUT.vcolor = IN.color;

                // compute main light shadow coord if helper exists
                #ifdef MAIN_LIGHT_SHADOWS
                    OUT.mainLightShadowCoord = TransformWorldToShadowCoord(displacedWorldPos);
                #else
                    OUT.mainLightShadowCoord = float4(0,0,0,0);
                #endif

                return OUT;
            }

            // Compute lit result using URP lighting helpers (main directional light)
            float3 ComputeURPLit(float3 albedo, float3 normalWS, float3 viewDirWS, float smoothness, float3 specColor, float4 mainLightShadowCoord)
            {
                // Get main light info (URP helper)
                Light mainLight = GetMainLight(mainLightShadowCoord);

                // Light color already includes intensity
                float3 lightColor = mainLight.color.rgb;

                // Calculate diffuse using URP helper (Lambert)
                float NdotL = saturate(dot(normalWS, -mainLight.direction)); // mainLight.direction points from surface to light? URP defines direction as -lightDir sometimes; we use negative to ensure positive NdotL
                float3 diffuse = albedo * NdotL * lightColor;

                // Blinn-Phong style specular
                float3 H = normalize(-mainLight.direction + viewDirWS);
                float NdotH = saturate(dot(normalWS, H));
                // Map smoothness to shininess exponent:
                float specPower = lerp(8.0, 2048.0, smoothness);
                float spec = pow(NdotH, specPower);
                float3 specular = spec * specColor * lightColor * (_Smoothness_Power * 0.0 + 1.0); // modest spec scaling

                // Add tiny ambient
                float3 ambient = 0.08 * albedo;

                // Apply main light shadow attenuation if provided
                float shadowAtten = 1.0;
                #if defined(MAIN_LIGHT_SHADOWS)
                    shadowAtten = mainLight.shadowAttenuation;
                #endif

                return (diffuse + specular) * shadowAtten + ambient;
            }

            // Fragment shader
            float4 frag(Varyings IN) : SV_Target
            {
                float2 uv = IN.uv;

                // Base color sampling + tiling/contrast/noise
                float2 uvBase = uv * _Base_Color_ST.xy + _Base_Color_ST.zw;
                float4 baseSample = SAMPLE_TEXTURE2D(_Base_Color, sampler_Base_Color, uvBase);

                // Perlin/noise tiling blend
                float2 uvTiling = uv * 3.0;
                float p = snoise(uvTiling * _Tilling_Color);
                p = p * 0.5 + 0.5;
                float4 perlinVec = float4(p, p, p, p);

                float4 colorTilingBlend = (_Color * baseSample) + (_Color_Tilling * baseSample * CalculateContrast(_Tilling_Contrast, perlinVec));
                float3 albedo = colorTilingBlend.rgb;

                // Mask & smoothness
                float2 uvMask = uv * _Mask_ST.xy + _Mask_ST.zw;
                float4 maskSample = SAMPLE_TEXTURE2D(_Mask, sampler_Mask, uvMask);
                float smoothness = saturate(_Smoothness_Power + maskSample.g);

                // Ambient occlusion from vertex color
                float ao = lerp(1.0, IN.vcolor.r, _Ambient_Occlusion);

                // Normal mapping: sample normal map and transform via TBN
                float2 uvNormal = uv * _Normal_ST.xy + _Normal_ST.zw;
                float3 nrmSample = SAMPLE_TEXTURE2D(_Normal, sampler_Normal, uvNormal).rgb;
                float3 normalTex = normalize(nrmSample * 2.0 - 1.0);
                normalTex.xy *= (1.0 + _Scale_Normal);
                float3 normalWS = normalize(normalTex.x * IN.worldTangent + normalTex.y * IN.worldBitangent + normalTex.z * IN.worldNormal);

                // Alpha clip using base alpha (original used base color alpha)
                clip(baseSample.a - _Cutoff);

                // View direction
                float3 viewDir = normalize(GetWorldSpaceViewDir(IN.worldPos));

                // Specular tint approx
                float3 specColor = lerp(float3(0.04, 0.04, 0.04), _Color.rgb, 0.2);

                // Compute lighting
                float3 lit = ComputeURPLit(albedo, normalWS, viewDir, smoothness, specColor, IN.mainLightShadowCoord);

                // Apply AO
                lit *= ao;

                // Final output: lit color + original alpha for cutout
                return float4(lit, baseSample.a);
            }

            ENDHLSL
        } // End Pass
    } // End SubShader

    FallBack "Diffuse"
}
