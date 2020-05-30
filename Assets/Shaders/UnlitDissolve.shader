Shader "Unlit/UnlitDissolve"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}

        _SliceGuide("Slice Guide (RGB)", 2D) = "white" {}
        _SliceAmount("Slice Amount", Range(0.0, 1.0)) = 0

        _BurnSize("Burn Size", Range(0.0, 1.0)) = 0.15
        _BurnRamp("Burn Ramp (RGB)", 2D) = "white" {}
        [HDR]_BurnColor("Burn Color", Color) = (1,1,1,1)

        _EmissionAmount("Emission amount", float) = 2.0

        _Speed("Speed",float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;
            
            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _SliceGuide;
            sampler2D _BumpMap;
            sampler2D _BurnRamp;
            fixed4 _BurnColor;
            fixed _BurnSize;
            fixed _SliceAmount;
            fixed _EmissionAmount;

            fixed _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 c = tex2D(_MainTex, i.uv) * _Color;

                half test = tex2D(_SliceGuide, i.uv).rgb - _SliceAmount;
                clip(test); // make rest transparent

                if (test < _BurnSize && _SliceAmount > 0)
                {
                    c *= tex2D(_BurnRamp, fixed2(test * (1 / _BurnSize), 0)) * _BurnColor * _EmissionAmount;
                }

                UNITY_APPLY_FOG(i.fogCoord, c);
                return c;
            }
            ENDCG
        }
    }
}
