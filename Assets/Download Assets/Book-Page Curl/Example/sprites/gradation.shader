Shader "gradation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Metallic ("Metallic", Range(0, 1)) = 0.5
        _GradientColorOne("Gradient Color One", color) = (1,1,1,1)
        _GradientColorTwo("Gradient Color Two", color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        
        Blend One OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : TEXCOORD1;
            };
            
            sampler2D _MainTex;
            float _Metallic;
            fixed4 _GradientColorOne;
            fixed4 _GradientColorTwo;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                
                float sinX = 1; 
                float cosX = 0; 
                float2x2 rotationMatrix = float2x2(cosX, -sinX, sinX, cosX);
                
                o.uv = mul(o.uv - float2(0.5, 0.5), rotationMatrix) + float2(0.5, 0.5);
                o.color = lerp(_GradientColorTwo, _GradientColorOne, o.uv.y);
                
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed3 albedo = texColor.rgb * (1.0 - _Metallic);
                fixed3 metalness = texColor.rgb * _Metallic;
                
                return float4(albedo + metalness, texColor.a) * i.color;
            }
            ENDCG
        }
    }
}
