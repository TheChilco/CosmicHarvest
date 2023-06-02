Shader "Custom/CelWaterWithEdgeFoam" 
{
    Properties 
    { 
        _BaseColor ("Base color", COLOR)  = ( .54, .95, .99, 0.5)
        _FoamColor ("Foam color", COLOR) = (1, 1, 1, 1)
        _EdgeThreshold ("Edge Threshold", Range(0, 1)) = 0.5
    }
    
    SubShader 
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        
        LOD 100

        Pass 
        {
            Blend SrcAlpha OneMinusSrcAlpha 
            ZWrite Off
            Cull Back
            
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform float4 _BaseColor;
            uniform float4 _FoamColor;
            uniform float _EdgeThreshold;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos);
                float ndotv = abs(dot(normalize(i.worldNormal), viewDir));
                fixed4 baseColor = _BaseColor;
                fixed4 foamColor = _FoamColor;
                fixed4 finalColor = (ndotv < _EdgeThreshold) ? foamColor : baseColor;

                return finalColor;
            }

            ENDCG
        }
    }
}
