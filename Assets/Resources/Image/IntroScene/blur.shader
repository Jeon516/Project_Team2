Shader "blur"
{
    Properties
    {
        _Radius("Radius", Range(1, 255)) = 1
    }

    Category
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Opaque" }

        SubShader
        {
            GrabPass
            {
                Tags { "LightMode" = "Always" }
            }

            Pass
            {
                Tags { "LightMode" = "Always" }

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f
                {
                    float4 vertex : SV_POSITION;
                    float2 uvgrab : TEXCOORD0;
                };

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uvgrab = v.texcoord; // Use 2D texture coordinates directly
                    return o;
                }

                sampler2D _GrabTexture;
                float4 _GrabTexture_TexelSize;
                float _Radius;

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 sum = fixed4(0, 0, 0, 0);

                    #define GRABXYPIXEL(kernelx, kernely) tex2D(_GrabTexture, float2(i.uvgrab.x + _GrabTexture_TexelSize.x * kernelx, i.uvgrab.y + _GrabTexture_TexelSize.y * kernely))

                    sum += GRABXYPIXEL(0.0, 0.0);
                    int measurements = 1;

                    for (float range = 1.0; range <= _Radius; range += 1.0)
                    {
                        sum += GRABXYPIXEL(range, 0);
                        sum += GRABXYPIXEL(-range, 0);
                        sum += GRABXYPIXEL(0, range);
                        sum += GRABXYPIXEL(0, -range);
                        measurements += 4;
                    }

                    return sum / measurements;
                }
                ENDCG
            }
        }
    }
}