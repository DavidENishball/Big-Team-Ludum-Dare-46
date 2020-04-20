// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Glass"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
        Pass {
          CGPROGRAM
  #pragma vertex vert
  #pragma fragment frag
  #include "UnityCG.cginc"

          struct appdata {
            float4 vertex: POSITION;
            float2 uv: TEXCOORD0;
          };

          struct v2f
          {
            float2 uv: TEXCOORD0;
            float4 vertex: SV_POSITION;
          };
          v2f vert(appdata v)
          {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.uv;
            return o;
          }

          fixed4 _Color;

          fixed4 frag(v2f i) : SV_Target
          {
            fixed4 col = _Color;
            col.a = 0.2;
            return col;
          }
          ENDCG
        }
    }
    FallBack "Diffuse"
}
