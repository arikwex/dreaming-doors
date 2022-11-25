Shader "Unlit/portal-shader"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "Queue"="Transparent-100" }
        LOD 100

        Pass
        {
            blend zero one
            zwrite off
            Stencil
            {
                Ref 1
                Comp always
                Pass replace
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = fixed4(1,1,1,0);
                return col;
            }
            ENDCG
        }
    }
}
