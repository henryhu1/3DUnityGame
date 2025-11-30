Shader "Custom/CandleFlame"
{
    Properties
    {
        [HDR]_Textures("Textures", 2D) = "white" {}
        [HDR]_TextureSample0("Texture Sample 0", 2D) = "white" {}
        _BrightnessMultiplier("Brightness Multiplier", Range(0,15)) = 2.7
        _Noise("Noise", 2D) = "white" {}
        _CoreColour("Core Colour", Color) = (0.9,0.78,0.36,1)
        _FlameFlickerSpeed("Flame Flicker Speed", Float) = 0.13
        _OuterColour("Outer Colour", Color) = (0.79,0.29,0.17,1)
        _BaseColour("Base Colour", Color) = (0.16,0.28,0.78,1)
        _NoiseScale("Noise Scale", Float) = 0.44
        _FakeGlow("Fake Glow", Range(0,1)) = 0.39
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            Name "Forward"
            Tags{ "LightMode"="UniversalForward" }

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_Textures);         SAMPLER(sampler_Textures);
            TEXTURE2D(_TextureSample0);   SAMPLER(sampler_TextureSample0);
            TEXTURE2D(_Noise);            SAMPLER(sampler_Noise);

            float _BrightnessMultiplier;
            float4 _CoreColour;
            float4 _OuterColour;
            float4 _BaseColour;
            float _FlameFlickerSpeed;
            float _NoiseScale;
            float _FakeGlow;
            float4 _TextureSample0_ST;

            struct Attributes {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                float2 uv = IN.uv;

                // Flame distortion
                float2 flickerOffset = float2(-_FlameFlickerSpeed, -_FlameFlickerSpeed);
                float2 panner = 0.37 * _Time.y * flickerOffset +
                                (_NoiseScale * uv);

                float2 noiseSample = SAMPLE_TEXTURE2D(_Noise, sampler_Noise, panner).rg;
                float2 finalUV = uv + (uv.y * noiseSample * uv.x * uv.y);

                // Flame texture RGB channels
                float4 tex = SAMPLE_TEXTURE2D(_Textures, sampler_Textures, finalUV);

                // Alpha mask
                float2 uvMask = uv * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
                float4 mask = SAMPLE_TEXTURE2D(_TextureSample0, sampler_TextureSample0, uvMask);

                // Emission color computation
                float3 emission =
                    _BrightnessMultiplier *
                    ( _CoreColour.rgb * tex.r +
                      _OuterColour.rgb * tex.g +
                      _BaseColour.rgb * tex.b +
                      _CoreColour.rgb * _FakeGlow * mask.a );

                return float4(emission, mask.a);
            }

            ENDHLSL
        }
    }
}
