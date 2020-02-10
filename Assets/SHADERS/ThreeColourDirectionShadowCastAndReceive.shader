Shader "Custom/ThreeColourDirectionShadowCastAndReceive"
{
    Properties {
        _DarkColor("Light Color", Color) = (1,1,1,1)
        _MiddleColor("Middle Color", Color) = (1,1,1,1)
        _LightColor("Dark Color", Color) = (1,1,1,1) 
    }
    SubShader {
        Tags {"Queue" = "Geometry" "RenderType" = "Opaque"}
    
        Pass {
            Tags {"LightMode" = "ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #pragma fragmentoption ARB_fog_exp2
            #pragma fragmentoption ARB_precision_hint_fastest
            
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            
            struct v2f
            {
                float4	pos			: SV_POSITION;
                float3	uv			: TEXCOORD0;
                LIGHTING_COORDS(1,2)
            };

            float4 _MainTex_ST;
            fixed4 _LightColor, _MiddleColor, _DarkColor;

            v2f vert (appdata_tan v)
            {
                v2f o;
                
                o.pos = UnityObjectToClipPos( v.vertex);
                
                half3 normal = normalize(mul(unity_ObjectToWorld, half4(v.normal, 0))).xyz;
                half lightDot = clamp(dot(normal, _WorldSpaceLightPos0), -1.0, 1.0);
                if (lightDot > 0) {
                    o.uv = lerp(_MiddleColor, _DarkColor, lightDot);
                } else if (lightDot < 0) {
                    o.uv = lerp(_MiddleColor, _LightColor, abs(lightDot));
                } else {
                    o.uv = _MiddleColor;
                }

                TRANSFER_VERTEX_TO_FRAGMENT(o);
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f i) : COLOR
            {
                fixed atten = LIGHT_ATTENUATION(i);	// Light attenuation + shadows.
                //fixed atten = SHADOW_ATTENUATION(i); // Shadows ONLY.
                // return tex2D(_MainTex, i.uv) * atten;
                fixed4 col = fixed4(i.uv, 1);
                return col * atten;
            }
            ENDCG
        }
    
        Pass {
            Tags {"LightMode" = "ForwardAdd"}
            Blend One One
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fwdadd_fullshadows
                #pragma fragmentoption ARB_fog_exp2
                #pragma fragmentoption ARB_precision_hint_fastest
                
                #include "UnityCG.cginc"
                #include "AutoLight.cginc"
                
                struct v2f
                {
                    float4	pos			: SV_POSITION;
                    float2	uv			: TEXCOORD0;
                    LIGHTING_COORDS(1,2)
                };
    
                float4 _MainTex_ST;
    
                v2f vert (appdata_tan v)
                {
                    v2f o;
                    
                    o.pos = UnityObjectToClipPos( v.vertex);
                    o.uv = TRANSFORM_TEX (v.texcoord, _MainTex).xy;
                    TRANSFER_VERTEX_TO_FRAGMENT(o);
                    return o;
                }
    
                sampler2D _MainTex;
    
                fixed4 frag(v2f i) : COLOR
                {
                    fixed atten = LIGHT_ATTENUATION(i);	// Light attenuation + shadows.
                    //fixed atten = SHADOW_ATTENUATION(i); // Shadows ONLY.
                    return tex2D(_MainTex, i.uv) * atten;
                }
            ENDCG
        }
    }
    FallBack "VertexLit"
}

// THIS SHADER COBBLED TOGETHER FROM:
// Shader "Unlit/threecolourdirection"
// {
//     Properties
//     {
//         _LightColor("Light Color", Color) = (1,1,1,1)
//         _MiddleColor("Middle Color", Color) = (1,1,1,1)
//         _DarkColor("Dark Color", Color) = (1,1,1,1) 
//     }
//     SubShader
//     {
//         Tags {"Queue" = "Geometry" "RenderType" = "Opaque"}
//         LOD 100
 
//         Pass
//         {
//             Tags {"LightMode" = "ForwardBase"}

//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
//             #pragma multi_compile_fwdbase
             
//             #include "UnityCG.cginc"
//             #include "AutoLight.cginc"
 
//             struct appdata
//             {
//                 float4 vertex : POSITION;
//                 float3 normal : NORMAL;
//             };
 
//             struct v2f
//             {
//                 LIGHTING_COORDS(0,1)
//                 float4 vertex : SV_POSITION;
//                 float3 color : TEXCOORD0;
//             };
             
 
//             fixed4 _LightColor;
//             fixed4 _MiddleColor;
//             fixed4 _DarkColor;
 
//             v2f vert (appdata v)
//             {
//                 v2f o;
//                 o.vertex = UnityObjectToClipPos(v.vertex);
//                 half3 normal = normalize(mul(unity_ObjectToWorld, half4(v.normal, 0))).xyz;
//                 half lightDot = clamp(dot(normal, _WorldSpaceLightPos0), -1.0, 1.0);
//                 if (lightDot > 0) {
//                     o.color = lerp(_MiddleColor, _DarkColor, lightDot);
//                 } else if (lightDot < 0) {
//                     o.color = lerp(_MiddleColor, _LightColor, abs(lightDot));
//                 } else {
//                     o.color = _MiddleColor;
//                 }
//                 TRANSFER_VERTEX_TO_FRAGMENT(o);
//                 return o;
//             }
             
//             fixed4 frag (v2f i) : SV_Target
//             {
//                 fixed4 col = fixed4(i.color, 1);
//                 float attenuation = LIGHT_ATTENUATION(i);
//                 return col * attenuation;
//             }
//             ENDCG
//         }
//     }
//     Fallback "Diffuse"
// }
// AND
// Shader "Unlit With Shadows" {
//     Properties {
//         _Color ("Main Color", Color) = (1,1,1,1)
//         _MainTex ("Base (RGB)", 2D) = "white" {}
//     }
//     SubShader {
//         Tags {"Queue" = "Geometry" "RenderType" = "Opaque"}

//         Pass {
//             Tags {"LightMode" = "ForwardBase"}
//             CGPROGRAM
//                 #pragma vertex vert
//                 #pragma fragment frag
//                 #pragma multi_compile_fwdbase
//                 #pragma fragmentoption ARB_fog_exp2
//                 #pragma fragmentoption ARB_precision_hint_fastest
                
//                 #include "UnityCG.cginc"
//                 #include "AutoLight.cginc"
                
//                 struct v2f
//                 {
//                     float4	pos			: SV_POSITION;
//                     float2	uv			: TEXCOORD0;
//                     LIGHTING_COORDS(1,2)
//                 };

//                 float4 _MainTex_ST;

//                 v2f vert (appdata_tan v)
//                 {
//                     v2f o;
                    
//                     o.pos = mul( UNITY_MATRIX_MVP, v.vertex);
//                     o.uv = TRANSFORM_TEX (v.texcoord, _MainTex).xy;
//                     TRANSFER_VERTEX_TO_FRAGMENT(o);
//                     return o;
//                 }

//                 sampler2D _MainTex;

//                 fixed4 frag(v2f i) : COLOR
//                 {
//                     fixed atten = LIGHT_ATTENUATION(i);	// Light attenuation + shadows.
//                     //fixed atten = SHADOW_ATTENUATION(i); // Shadows ONLY.
//                     return tex2D(_MainTex, i.uv) * atten;
//                 }
//             ENDCG
//         }

//         Pass {
//             Tags {"LightMode" = "ForwardAdd"}
//             Blend One One
//             CGPROGRAM
//                 #pragma vertex vert
//                 #pragma fragment frag
//                 #pragma multi_compile_fwdadd_fullshadows
//                 #pragma fragmentoption ARB_fog_exp2
//                 #pragma fragmentoption ARB_precision_hint_fastest
                
//                 #include "UnityCG.cginc"
//                 #include "AutoLight.cginc"
                
//                 struct v2f
//                 {
//                     float4	pos			: SV_POSITION;
//                     float2	uv			: TEXCOORD0;
//                     LIGHTING_COORDS(1,2)
//                 };

//                 float4 _MainTex_ST;

//                 v2f vert (appdata_tan v)
//                 {
//                     v2f o;
                    
//                     o.pos = mul( UNITY_MATRIX_MVP, v.vertex);
//                     o.uv = TRANSFORM_TEX (v.texcoord, _MainTex).xy;
//                     TRANSFER_VERTEX_TO_FRAGMENT(o);
//                     return o;
//                 }

//                 sampler2D _MainTex;

//                 fixed4 frag(v2f i) : COLOR
//                 {
//                     fixed atten = LIGHT_ATTENUATION(i);	// Light attenuation + shadows.
//                     //fixed atten = SHADOW_ATTENUATION(i); // Shadows ONLY.
//                     return tex2D(_MainTex, i.uv) * atten;
//                 }
//             ENDCG
//         }
//     }
//     FallBack "VertexLit"
// }

