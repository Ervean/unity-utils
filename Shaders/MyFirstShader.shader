Shader "Introduction/MyFirstShader"
{
    Properties
    {
        _Color("Test Color", color) = (1,1,1,1)   // {name }("attribute name", typeEnum) = type
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert // Runs on every vert
            #pragma fragment frag // Runs on every single pixel
         

            #include "UnityCG.cginc"

            struct appdata // Object data or mesh
            {
                float4 vertex : POSITION;      
            };

            struct v2f // vertex to frag, can pass vertex data to frag
            {
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color;
                return col;
            }
            ENDCG
        }
    }
}
