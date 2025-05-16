Shader "Custom/Caustics"

{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        [Header(Caustics)]
        _CausticsTex("Caustics (RGB)", 2D) = "white" {}
        _Caustics1_ST("Caustics 1 ST (Tiling, Offset)", Vector) = (1,1,0,0)
        _Caustics2_ST("Caustics 2 ST (Tiling, Offset)", Vector) = (1,1,0,0)
        _Caustics1_Speed("Caustics 1 Speed", Vector) = (1, 1, 0, 0)
        _Caustics2_Speed("Caustics 2 Speed", Vector) = (1, 1, 0, 0)
        _SplitRGB("RGB Split Amount", Float) = 0.01
        _Metallic("Metallic", Range(0, 1)) = 0.0
        _Smoothness("Smoothness", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderPipeline"="UniversalRenderPipeline" }
        LOD 200

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            sampler2D _MainTex;
            sampler2D _CausticsTex;
            float4 _Caustics1_ST;
            float4 _Caustics2_ST;
            float4 _Caustics1_Speed;
            float4 _Caustics2_Speed;
            float _SplitRGB;
            float _Metallic;
            float _Smoothness;

            struct Attributes
            {
                float3 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = IN.uv;
                return OUT;
            }

            float3 causticsSample(sampler2D tex, float2 uv, float4 st, float4 speed)
            {
                float2 offsetUV = uv * st.xy + st.zw + speed.xy * _Time.y;
                float s = _SplitRGB;

                // RGB split sampling
                float r = tex2D(tex, offsetUV + float2(+s, +s)).r;
                float g = tex2D(tex, offsetUV + float2(+s, -s)).g;
                float b = tex2D(tex, offsetUV + float2(-s, -s)).b;

                return float3(r, g, b);
            }

            float4 frag(Varyings IN) : SV_Target
            {
                // Base texture
                float4 baseColor = tex2D(_MainTex, IN.uv);

                // Caustics sampling
                float3 c1 = causticsSample(_CausticsTex, IN.uv, _Caustics1_ST, _Caustics1_Speed);
                float3 c2 = causticsSample(_CausticsTex, IN.uv, _Caustics2_ST, _Caustics2_Speed);

                // Combine caustics using minimum
                float3 caustics = min(c1, c2);

                // Final color
                float3 finalColor = baseColor.rgb + caustics;

                return float4(finalColor, baseColor.a);
            }
            ENDHLSL
        }
    }
    FallBack "Hidden/InternalErrorShader"
}

