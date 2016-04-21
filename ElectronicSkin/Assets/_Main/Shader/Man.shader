// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:0,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|diff-20-OUT,spec-2046-OUT,gloss-1003-OUT,alpha-9075-A;n:type:ShaderForge.SFN_Vector1,id:2046,x:32507,y:32753,varname:node_2046,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:1003,x:32507,y:32808,varname:node_1003,prsc:2,v1:0;n:type:ShaderForge.SFN_Time,id:8221,x:29588,y:32282,varname:node_8221,prsc:2;n:type:ShaderForge.SFN_Vector1,id:501,x:30117,y:32135,varname:node_501,prsc:2,v1:0.08;n:type:ShaderForge.SFN_Tex2dAsset,id:2103,x:30366,y:32383,ptovrint:False,ptlb:Effect Texture,ptin:_EffectTexture,varname:node_2103,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:82e0bb45501a9cf41a574dfe56ba2b7f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9334,x:30812,y:32077,varname:node_9334,prsc:2,tex:82e0bb45501a9cf41a574dfe56ba2b7f,ntxv:0,isnm:False|UVIN-4771-UVOUT,TEX-2103-TEX;n:type:ShaderForge.SFN_UVTile,id:4771,x:30366,y:32181,varname:node_4771,prsc:2|WDT-501-OUT,HGT-5019-OUT,TILE-8679-OUT;n:type:ShaderForge.SFN_Tex2d,id:3129,x:30816,y:32440,varname:node_3129,prsc:2,tex:82e0bb45501a9cf41a574dfe56ba2b7f,ntxv:0,isnm:False|UVIN-9309-UVOUT,TEX-2103-TEX;n:type:ShaderForge.SFN_UVTile,id:9309,x:30366,y:32579,varname:node_9309,prsc:2|WDT-501-OUT,HGT-5019-OUT,TILE-9157-OUT;n:type:ShaderForge.SFN_Vector1,id:7766,x:29964,y:32570,varname:node_7766,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:9157,x:30141,y:32550,varname:node_9157,prsc:2|A-8221-TSL,B-7766-OUT,C-2248-OUT;n:type:ShaderForge.SFN_Add,id:5096,x:31714,y:32258,varname:node_5096,prsc:2|A-6009-OUT,B-3871-OUT;n:type:ShaderForge.SFN_Color,id:9075,x:31089,y:32244,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.125,c2:0.3120689,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:3871,x:31421,y:32326,varname:node_3871,prsc:2|A-942-OUT,B-9075-RGB;n:type:ShaderForge.SFN_Multiply,id:6009,x:31421,y:32091,varname:node_6009,prsc:2|A-9075-RGB,B-8244-OUT;n:type:ShaderForge.SFN_RemapRange,id:743,x:31995,y:32315,varname:node_743,prsc:2,frmn:0,frmx:1,tomn:0,tomx:10|IN-5096-OUT;n:type:ShaderForge.SFN_Slider,id:2248,x:29545,y:32495,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_2248,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:8679,x:29963,y:32265,varname:node_8679,prsc:2|A-8221-TSL,B-2248-OUT;n:type:ShaderForge.SFN_Vector1,id:5019,x:30117,y:32203,varname:node_5019,prsc:2,v1:0.08;n:type:ShaderForge.SFN_Power,id:942,x:31083,y:32418,varname:node_942,prsc:2|VAL-3129-RGB,EXP-5509-OUT;n:type:ShaderForge.SFN_Vector1,id:5509,x:30799,y:32594,varname:node_5509,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Power,id:8244,x:31089,y:32029,varname:node_8244,prsc:2|VAL-9334-RGB,EXP-9952-OUT;n:type:ShaderForge.SFN_Vector1,id:9952,x:30797,y:32263,varname:node_9952,prsc:2,v1:1.2;n:type:ShaderForge.SFN_NormalVector,id:6895,x:31792,y:32556,prsc:2,pt:True;n:type:ShaderForge.SFN_Lerp,id:20,x:32250,y:32425,varname:node_20,prsc:2|A-743-OUT,B-8037-OUT,T-1701-OUT;n:type:ShaderForge.SFN_Vector1,id:1825,x:31995,y:32663,varname:node_1825,prsc:2,v1:0.2;n:type:ShaderForge.SFN_ComponentMask,id:8037,x:31995,y:32501,varname:node_8037,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-6895-OUT;n:type:ShaderForge.SFN_Slider,id:1701,x:32053,y:32713,ptovrint:False,ptlb:LerpValue,ptin:_LerpValue,varname:node_1701,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:2103-9075-2248-1701;pass:END;sub:END;*/

Shader "Shader Forge/Wall" {
    Properties {
        _EffectTexture ("Effect Texture", 2D) = "white" {}
        _MainColor ("MainColor", Color) = (0.125,0.3120689,1,1)
        _Speed ("Speed", Range(0, 1)) = 0
        _LerpValue ("LerpValue", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _EffectTexture; uniform float4 _EffectTexture_ST;
            uniform float4 _MainColor;
            uniform float _Speed;
            uniform float _LerpValue;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                UNITY_FOG_COORDS(7)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD8;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.0;
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_2046 = 0.0;
                float3 specularColor = float3(node_2046,node_2046,node_2046);
                float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 indirectSpecular = (gi.indirect.specular)*specularColor;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float node_501 = 0.08;
                float node_5019 = 0.08;
                float4 node_8221 = _Time + _TimeEditor;
                float node_8679 = (node_8221.r*_Speed);
                float2 node_4771_tc_rcp = float2(1.0,1.0)/float2( node_501, node_5019 );
                float node_4771_ty = floor(node_8679 * node_4771_tc_rcp.x);
                float node_4771_tx = node_8679 - node_501 * node_4771_ty;
                float2 node_4771 = (i.uv0 + float2(node_4771_tx, node_4771_ty)) * node_4771_tc_rcp;
                float4 node_9334 = tex2D(_EffectTexture,TRANSFORM_TEX(node_4771, _EffectTexture));
                float node_9157 = (node_8221.r*(-1.0)*_Speed);
                float2 node_9309_tc_rcp = float2(1.0,1.0)/float2( node_501, node_5019 );
                float node_9309_ty = floor(node_9157 * node_9309_tc_rcp.x);
                float node_9309_tx = node_9157 - node_501 * node_9309_ty;
                float2 node_9309 = (i.uv0 + float2(node_9309_tx, node_9309_ty)) * node_9309_tc_rcp;
                float4 node_3129 = tex2D(_EffectTexture,TRANSFORM_TEX(node_9309, _EffectTexture));
                float node_8037 = normalDirection.r;
                float3 diffuseColor = lerp((((_MainColor.rgb*pow(node_9334.rgb,1.2))+(pow(node_3129.rgb,1.5)*_MainColor.rgb))*10.0+0.0),float3(node_8037,node_8037,node_8037),_LerpValue);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,_MainColor.a);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _EffectTexture; uniform float4 _EffectTexture_ST;
            uniform float4 _MainColor;
            uniform float _Speed;
            uniform float _LerpValue;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.0;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_2046 = 0.0;
                float3 specularColor = float3(node_2046,node_2046,node_2046);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float node_501 = 0.08;
                float node_5019 = 0.08;
                float4 node_8221 = _Time + _TimeEditor;
                float node_8679 = (node_8221.r*_Speed);
                float2 node_4771_tc_rcp = float2(1.0,1.0)/float2( node_501, node_5019 );
                float node_4771_ty = floor(node_8679 * node_4771_tc_rcp.x);
                float node_4771_tx = node_8679 - node_501 * node_4771_ty;
                float2 node_4771 = (i.uv0 + float2(node_4771_tx, node_4771_ty)) * node_4771_tc_rcp;
                float4 node_9334 = tex2D(_EffectTexture,TRANSFORM_TEX(node_4771, _EffectTexture));
                float node_9157 = (node_8221.r*(-1.0)*_Speed);
                float2 node_9309_tc_rcp = float2(1.0,1.0)/float2( node_501, node_5019 );
                float node_9309_ty = floor(node_9157 * node_9309_tc_rcp.x);
                float node_9309_tx = node_9157 - node_501 * node_9309_ty;
                float2 node_9309 = (i.uv0 + float2(node_9309_tx, node_9309_ty)) * node_9309_tc_rcp;
                float4 node_3129 = tex2D(_EffectTexture,TRANSFORM_TEX(node_9309, _EffectTexture));
                float node_8037 = normalDirection.r;
                float3 diffuseColor = lerp((((_MainColor.rgb*pow(node_9334.rgb,1.2))+(pow(node_3129.rgb,1.5)*_MainColor.rgb))*10.0+0.0),float3(node_8037,node_8037,node_8037),_LerpValue);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * _MainColor.a,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _EffectTexture; uniform float4 _EffectTexture_ST;
            uniform float4 _MainColor;
            uniform float _Speed;
            uniform float _LerpValue;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float node_501 = 0.08;
                float node_5019 = 0.08;
                float4 node_8221 = _Time + _TimeEditor;
                float node_8679 = (node_8221.r*_Speed);
                float2 node_4771_tc_rcp = float2(1.0,1.0)/float2( node_501, node_5019 );
                float node_4771_ty = floor(node_8679 * node_4771_tc_rcp.x);
                float node_4771_tx = node_8679 - node_501 * node_4771_ty;
                float2 node_4771 = (i.uv0 + float2(node_4771_tx, node_4771_ty)) * node_4771_tc_rcp;
                float4 node_9334 = tex2D(_EffectTexture,TRANSFORM_TEX(node_4771, _EffectTexture));
                float node_9157 = (node_8221.r*(-1.0)*_Speed);
                float2 node_9309_tc_rcp = float2(1.0,1.0)/float2( node_501, node_5019 );
                float node_9309_ty = floor(node_9157 * node_9309_tc_rcp.x);
                float node_9309_tx = node_9157 - node_501 * node_9309_ty;
                float2 node_9309 = (i.uv0 + float2(node_9309_tx, node_9309_ty)) * node_9309_tc_rcp;
                float4 node_3129 = tex2D(_EffectTexture,TRANSFORM_TEX(node_9309, _EffectTexture));
                float node_8037 = normalDirection.r;
                float3 diffColor = lerp((((_MainColor.rgb*pow(node_9334.rgb,1.2))+(pow(node_3129.rgb,1.5)*_MainColor.rgb))*10.0+0.0),float3(node_8037,node_8037,node_8037),_LerpValue);
                float node_2046 = 0.0;
                float3 specColor = float3(node_2046,node_2046,node_2046);
                float roughness = 1.0 - 0.0;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
