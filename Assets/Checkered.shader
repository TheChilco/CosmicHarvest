Shader "Custom/Checkered"
{
    Properties
    {
        _Color1 ("Color 1", Color) = (1, 0, 0, 1)
        _Color2 ("Color 2", Color) = (0, 0, 1, 1)
        _Tile ("Tile", Range(1, 20)) = 10
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
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float3 worldPos : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _Color1;
            float4 _Color2;
            float _Tile;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed3 pos = i.worldPos * _Tile;
                fixed2 grid = fmod(floor(pos.xz), 2.0);
                fixed4 col = lerp(_Color1, _Color2, grid.x * grid.y + (1 - grid.x) * (1 - grid.y));
                return col;
            }
            ENDCG
        }
    }
}
