Shader "Custom/TestShader"
{
    SubShader
    {
      Pass
      {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"

        float4 vert(float4 vertex : POSITION) : SV_POSITION
        {
          return UnityObjectToClipPos(vertex);
        }

        float4 frag(float4 vertex : SV_POSITION) : SV_TARGET
        {
          return 1;
        }
        ENDCG
      }
    }
}