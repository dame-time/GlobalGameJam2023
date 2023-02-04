Shader "Transition/SquareTransition"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Progress("Progress transition", Range(0.0, 1.0)) = 0.0
    }
    SubShader
    {
        Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }

            Cull Off
            Lighting Off
            ZWrite Off
            Fog { Mode Off }
            Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 screenPos: TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Progress;

            v2f vert (appdata v, out float4 outpos : SV_POSITION)
            {
                v2f o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                outpos = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i, UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                screenPos.xy /= _ScreenParams.xy;

                float xFraction = frac(screenPos.x / 0.1);
                float yFraction = frac(screenPos.y / 0.1);
                float xDistance = abs(xFraction - 0.5);
                float yDistance = abs(yFraction - 0.5);

                _Progress = 1 - _Progress;

                return xDistance + yDistance + i.uv.x + i.uv.y > _Progress * 4? fixed4(0, 0, 0, 1) : col;
            }
            ENDCG
        }
    }
}
