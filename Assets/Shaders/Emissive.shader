Shader "Custom/Emissive" {
    Properties {
        _MainTex ("Base Map", 2D) = "white" {}
        _EmissiveMap ("Emissive Map", 2D) = "white" {}
        _Bloom ("Bloom Intensity", Range(0, 100)) = 1.0
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 200

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _EmissiveMap;

            float _Bloom;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target {
                float4 col = tex2D(_MainTex, i.uv);
                float4 emissiveColor = tex2D(_EmissiveMap, i.uv) * _Bloom;

                return col + emissiveColor;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
