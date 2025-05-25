Shader "Custom/CasualFlatShader"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _AmbientColor ("Ambient Color", Color) = (0.5,0.5,0.5,1)

        [Header(Cel Shading Options)]
        // Reduced cel shading steps to a maximum of 2.
        _CelShadingSteps ("Cel Shading Steps", Range(1, 2)) = 2
        _StepColor1 ("Step Color 1 (Light)", Color) = (0.8,0.8,0.8,1) // Used for light step
        _StepColor5 ("Step Color 5 (Shadow)", Color) = (0,0,0,1) // Used for shadow step

        [Header(Shadow Options)]
        _ShadowStrength ("Shadow Strength", Range(0.0, 1.0)) = 1.0 // New property for shadow intensity

        [Header(Outline Options)]
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0.0, 0.1)) = 0.005
        [Toggle(_OUTLINE_ENABLED)] _EnableOutline ("Enable Outline", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" } 
        LOD 100

        //#pragma shader_feature_local _OUTLINE_ENABLED 

        // Pass for the main flat / cel-shaded color
        Pass
        {
            Tags { "LightMode"="ForwardBase" } 

            CGPROGRAM
            #pragma target 3.0 
            
            #pragma vertex vert 
            #pragma fragment frag 

            #include "UnityCG.cginc" 
            #include "Lighting.cginc" 
            #include "AutoLight.cginc" // Required for shadow macros

            struct appdata
            {
                float4 vertex : POSITION; 
                float3 normal : NORMAL;   
                float4 color : COLOR;     
            };

            struct v2f
            {
                float4 vertex : SV_POSITION; 
                float3 worldNormal : TEXCOORD0; 
                float3 worldPos : TEXCOORD1;    
                float4 vertexColor : TEXCOORD2; 
                SHADOW_COORDS(3)
            };

            fixed4 _MainColor;
            fixed4 _AmbientColor;
            
            float _CelShadingSteps;
            fixed4 _StepColor1, _StepColor2, _StepColor3, _StepColor4, _StepColor5;

            float _ShadowStrength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.vertexColor = v.color; 

                TRANSFER_SHADOW(o);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 worldNormal = normalize(i.worldNormal);
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz); 
                float NdotL = dot(worldNormal, lightDir);

                fixed4 finalColor = _MainColor; 
                fixed4 baseColor = _MainColor * i.vertexColor;

                if (_CelShadingSteps == 1) {
                    finalColor = baseColor; 
                } else { 
                    float stepValue = floor(NdotL * _CelShadingSteps);
                    stepValue = saturate(stepValue); 

                    if (stepValue == 0) finalColor = _StepColor5; 
                    else finalColor = _StepColor1; 
                }

                // --- Shadow Calculation ---
                // Get shadow attenuation (0 = full shadow, 1 = full light)
                float shadowAttenuation = SHADOW_ATTENUATION(i); 

                // Blend the final color with the shadow attenuation based on _ShadowStrength
                // If _ShadowStrength is 1.0, full shadow is applied.
                // If _ShadowStrength is 0.0, no shadow is applied (shadowAttenuation is ignored).
                finalColor = lerp(finalColor, finalColor * shadowAttenuation, _ShadowStrength);

                finalColor += _AmbientColor;

                return finalColor; 
            }
            ENDCG
        }

        // Pass for the Outline (Inverted Hull Method)
        //#ifdef _OUTLINE_ENABLED
        Pass
        {
            Cull Front 
            ZWrite Off 
            ZTest LEqual 
            Blend SrcAlpha OneMinusSrcAlpha 

            CGPROGRAM
            #pragma target 3.0 
            #pragma vertex vert 
            #pragma fragment frag 

            #include "UnityCG.cginc" 

            struct appdata
            {
                float4 vertex : POSITION; 
                float3 normal : NORMAL;   
            };

            struct v2f
            {
                float4 vertex : SV_POSITION; 
            };

            fixed4 _OutlineColor;
            float _OutlineThickness;

            v2f vert (appdata v)
            {
                v2f o;
                float4 pos = v.vertex + float4(v.normal, 0) * _OutlineThickness;
                o.vertex = UnityObjectToClipPos(pos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _OutlineColor; 
            }
            ENDCG
        }
        //#endif 
    }
    FallBack "Diffuse" 
}
