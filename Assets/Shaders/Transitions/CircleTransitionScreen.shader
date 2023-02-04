Shader "Transition/CircleTransition"
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

                v2f vert(appdata v, out float4 outpos : SV_POSITION)
                {
                    v2f o;
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    outpos = UnityObjectToClipPos(v.vertex);
                    o.screenPos = ComputeScreenPos(v.vertex);
                    return o;
                }

                float2 hextile(inout fixed2 p) {
                    const float2 sz = float2(1.0, sqrt(3.0));
                    const float2 hsz = 0.5 * sz;

                    float2 p1 = fmod(p, sz) - hsz;
                    float2 p2 = fmod(p - hsz, sz) - hsz;
                    float2 p3 = dot(p1, p1) < dot(p2, p2) ? p1 : p2;
                    float2 n = ((p3 - p + hsz) / sz);
                    p = p3;

                    n -= float2(0.5, 0.5);
                    // Rounding to make hextile 0,0 well behaved
                    return round(n * 2.0) / 2.0;
                }

                float tanh_approx(float x) {
                    //  return tanh(x);
                    float x2 = x * x;
                    return clamp(x * (27.0 + x2) / (27.0 + 9.0 * x2), -1.0, 1.0);
                }

                float hex(float2 p, float r) {
                    p.xy = p.yx;
                    float3 k = float3(-sqrt(3.0 / 4.0), 1.0 / 2.0, 1.0 / sqrt(3.0));
                    p = abs(p);
                    p -= 2.0 * min(dot(k.xy, p), 0.0) * k.xy;
                    p -= float2(clamp(p.x, -k.z * r, k.z * r), r);
                    return length(p) * sign(p.y);
                }

                float hash(fixed2 co) {
                    return frac(sin(dot(co, float2(12.9898, 58.233))) * 13758.5453);
                }

                float3 hexTransition(float2 p, float aa, float3 from, float3 to, float m) {
                    m = clamp(m, 0.0, 1.0);
                    float hz = 0.4;
                    float rz = 0.75;
                    float2 hp = p;
                    hp /= hz;
                    //  hp *= ROT(0.5*(1.0-m));
                    float2 hn = hextile(hp) * hz * -fixed2(-1.0, sqrt(3.0));
                    float r = hash(hn + 123.4);

                    float off = 3.0;
                    float fi = smoothstep(0.0, 0.1, m);
                    float fo = smoothstep(0.6, 1.0, m);

                    float sz = 0.45 * (0.5 + 0.5 * tanh_approx(((rz * r + hn.x + hn.y - off + m * off * 2.0)) * 2.0));
                    float hd = (hex(hp, sz) - 0.1 * sz) * hz;

                    float mm = smoothstep(-aa, aa, -hd);
                    mm = lerp(0.0, mm, fi);
                    mm = lerp(mm, 1.0, fo);

                    float3 col = lerp(from, to, mm);
                    float2 ahn = abs(hn);
                    return col;
                }

                fixed4 frag(v2f i, UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target
                {
                    // sample the texture
                    fixed4 col = tex2D(_MainTex, i.uv);

                    float2 q = screenPos / _ScreenParams.xy + 0.55f;
                    float2 p = -1. + 2. * q;
				    p.x *= _ScreenParams.x / _ScreenParams.y;
                    float aa = 2.0 / _ScreenParams.y;

                    float nt = _Time[2] / 8;
                    float m = frac(_Progress) * 1.25;
                    float n = fmod(floor(_Progress), 2.0);

                    fixed4 from = n == 0.0 ? col : fixed4(0.0, 0.0, 0.0, 1.0);
                    fixed4 to = n != 0.0 ? col : fixed4(0.0, 0.0, 0.0, 1.0);

                    float3 color = hexTransition(p, aa, from, to, m);

                    return fixed4(color, 1.0);
                }
                ENDCG
            }
        }

}
