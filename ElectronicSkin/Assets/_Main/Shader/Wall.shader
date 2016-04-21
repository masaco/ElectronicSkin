// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|diff-7733-OUT,spec-2046-OUT,gloss-1003-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:32099,y:32897,varname:node_6343,prsc:2|A-7736-RGB,B-6665-RGB;n:type:ShaderForge.SFN_Color,id:6665,x:31839,y:33054,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7736,x:31839,y:32869,ptovrint:True,ptlb:Main Texture,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5a11645cedaf3f14c829deb722aa44ba,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5964,x:32328,y:32996,ptovrint:True,ptlb:Normal Map,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4f713790195a5084481c6b7f67fb0da5,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Vector1,id:2046,x:32507,y:32753,varname:node_2046,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:1003,x:32507,y:32808,varname:node_1003,prsc:2,v1:0;n:type:ShaderForge.SFN_Time,id:8221,x:29766,y:32240,varname:node_8221,prsc:2;n:type:ShaderForge.SFN_Vector1,id:501,x:30295,y:32093,varname:node_501,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Tex2dAsset,id:2103,x:30544,y:32341,ptovrint:False,ptlb:Effect Texture,ptin:_EffectTexture,varname:node_2103,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e49fd0b4279e3d441af485ecbf83191a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9334,x:30994,y:32196,varname:node_9334,prsc:2,tex:e49fd0b4279e3d441af485ecbf83191a,ntxv:0,isnm:False|UVIN-4771-UVOUT,TEX-2103-TEX;n:type:ShaderForge.SFN_UVTile,id:4771,x:30544,y:32139,varname:node_4771,prsc:2|WDT-501-OUT,HGT-5019-OUT,TILE-8679-OUT;n:type:ShaderForge.SFN_Tex2d,id:3129,x:30994,y:32398,varname:node_3129,prsc:2,tex:e49fd0b4279e3d441af485ecbf83191a,ntxv:0,isnm:False|UVIN-9309-UVOUT,TEX-2103-TEX;n:type:ShaderForge.SFN_UVTile,id:9309,x:30544,y:32537,varname:node_9309,prsc:2|WDT-501-OUT,HGT-5019-OUT,TILE-9157-OUT;n:type:ShaderForge.SFN_Vector1,id:7766,x:30142,y:32528,varname:node_7766,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:9157,x:30319,y:32508,varname:node_9157,prsc:2|A-8221-TSL,B-7766-OUT,C-2248-OUT;n:type:ShaderForge.SFN_Add,id:5096,x:31714,y:32258,varname:node_5096,prsc:2|A-6009-OUT,B-3871-OUT;n:type:ShaderForge.SFN_Color,id:9075,x:30994,y:32606,ptovrint:False,ptlb:EffectColor1,ptin:_EffectColor1,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.125,c2:0.3120689,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:3871,x:31548,y:32466,varname:node_3871,prsc:2|A-1361-OUT,B-9075-RGB;n:type:ShaderForge.SFN_Add,id:4788,x:32422,y:32624,varname:node_4788,prsc:2|A-743-OUT,B-6343-OUT;n:type:ShaderForge.SFN_Multiply,id:6009,x:31544,y:32050,varname:node_6009,prsc:2|A-488-RGB,B-7898-OUT;n:type:ShaderForge.SFN_Color,id:488,x:30994,y:32013,ptovrint:False,ptlb:EffectColor2,ptin:_EffectColor2,varname:_EffectColor_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.8014706,c3:0.2376775,c4:1;n:type:ShaderForge.SFN_RemapRange,id:743,x:31995,y:32315,varname:node_743,prsc:2,frmn:0,frmx:1,tomn:0,tomx:0.8|IN-5096-OUT;n:type:ShaderForge.SFN_Slider,id:2248,x:29723,y:32453,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_2248,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:2;n:type:ShaderForge.SFN_Multiply,id:8679,x:30141,y:32223,varname:node_8679,prsc:2|A-8221-TSL,B-2248-OUT;n:type:ShaderForge.SFN_Vector1,id:5019,x:30295,y:32161,varname:node_5019,prsc:2,v1:0.25;n:type:ShaderForge.SFN_Lerp,id:7733,x:32222,y:32624,varname:node_7733,prsc:2|A-743-OUT,B-6343-OUT,T-7390-OUT;n:type:ShaderForge.SFN_Vector1,id:8173,x:31879,y:32554,varname:node_8173,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Slider,id:7390,x:31840,y:32742,ptovrint:False,ptlb:EffectLerp,ptin:_EffectLerp,varname:node_7390,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Power,id:7898,x:31314,y:32181,varname:node_7898,prsc:2|VAL-9334-RGB,EXP-1930-OUT;n:type:ShaderForge.SFN_Vector1,id:1930,x:31143,y:32340,varname:node_1930,prsc:2,v1:2;n:type:ShaderForge.SFN_Power,id:1361,x:31314,y:32358,varname:node_1361,prsc:2|VAL-3129-RGB,EXP-1930-OUT;proporder:7736-6665-5964-2103-9075-488-2248-7390;pass:END;sub:END;*/

Shader "Shader Forge/Wall" {
    Properties {
        _MainTex ("Main Texture", 2D) = "white" {}
        _MainColor ("MainColor", Color) = (1,1,1,1)
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _EffectTexture ("Effect Texture", 2D) = "white" {}
        _EffectColor1 ("EffectColor1", Color) = (0.125,0.3120689,1,1)
        _EffectColor2 ("EffectColor2", Color) = (0,0.8014706,0.2376775,1)
        _Speed ("Speed", Range(0, 2)) = 0
        _EffectLerp ("EffectLerp", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _MainColor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _EffectTexture; uniform float4 _EffectTexture_ST;
            uniform float4 _EffectColor1;
            uniform float4 _EffectColor2;
            uniform float _Speed;
            uniform float _EffectLerp;
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
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
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
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
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
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float node_501 = 0.5;
                float node_5019 = 0.25;
                float4 node_8221 = _Time + _TimeEditor;
                float node_8679 = (node_8221.r*_Speed);
                float2 node_4771_tc_rcp = float2(1.0,1.0)/float2( node_501, node_5019 );
                float node_4771_ty = floor(node_8679 * node_4771_tc_rcp.x);
                float node_4771_tx = node_8679 - node_501 * node_4771_ty;
                float2 node_4771 = (i.uv0 + float2(node_4771_tx, node_4771_ty)) * node_4771_tc_rcp;
                float4 node_9334 = tex2D(_EffectTexture,TRANSFORM_TEX(node_4771, _EffectTexture));
                float node_1930 = 2.0;
                float node_9157 = (node_8221.r*(-1.0)*_Speed);
                float2 node_9309_tc_rcp = float2(1.0,1.0)/float2( node_501, node_5019 );
                float node_9309_ty = floor(node_9157 * node_9309_tc_rcp.x);
                float node_9309_tx = node_9157 - node_501 * node_9309_ty;
                float2 node_9309 = (i.uv0 + float2(node_9309_tx, node_9309_ty)) * node_9309_tc_rcp;
                float4 node_3129 = tex2D(_EffectTexture,TRANSFORM_TEX(node_9309, _EffectTexture));
                float3 node_743 = (((_EffectColor2.rgb*pow(node_9334.rgb,node_1930))+(pow(node_3129.rgb,node_1930)*_EffectColor1.rgb))*0.8+0.0);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_6343 = (_MainTex_var.rgb*_MainColor.rgb);
                float3 diffuseColor = lerp(node_743,node_6343,_EffectLerp); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, 0.0, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
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
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _MainColor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _EffectTexture; uniform float4 _EffectTexture_ST;
            uniform float4 _EffectColor1;
            uniform float4 _EffectColor2;
            uniform float _Speed;
            uniform float _EffectLerp;
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
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 0.0;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float node_501 = 0.5;
                float node_5019 = 0.25;
                float4 node_8221 = _Time + _TimeEditor;
                float node_8679 = (node_8221.r*_Speed);
                float2 node_4771_tc_rcp = float2(1.0,1.0)/float2( node_501, node_5019 );
                float node_4771_ty = floor(node_8679 * node_4771_tc_rcp.x);
                float node_4771_tx = node_8679 - node_501 * node_4771_ty;
                float2 node_4771 = (i.uv0 + float2(node_4771_tx, node_4771_ty)) * node_4771_tc_rcp;
                float4 node_9334 = tex2D(_EffectTexture,TRANSFORM_TEX(node_4771, _EffectTexture));
                float node_1930 = 2.0;
                float node_9157 = (node_8221.r*(-1.0)*_Speed);
                float2 node_9309_tc_rcp = float2(1.0,1.0)/float2( node_501, node_5019 );
                float node_9309_ty = floor(node_9157 * node_9309_tc_rcp.x);
                float node_9309_tx = node_9157 - node_501 * node_9309_ty;
                float2 node_9309 = (i.uv0 + float2(node_9309_tx, node_9309_ty)) * node_9309_tc_rcp;
                float4 node_3129 = tex2D(_EffectTexture,TRANSFORM_TEX(node_9309, _EffectTexture));
                float3 node_743 = (((_EffectColor2.rgb*pow(node_9334.rgb,node_1930))+(pow(node_3129.rgb,node_1930)*_EffectColor1.rgb))*0.8+0.0);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_6343 = (_MainTex_var.rgb*_MainColor.rgb);
                float3 diffuseColor = lerp(node_743,node_6343,_EffectLerp); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, 0.0, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
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
            uniform float4 _MainColor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _EffectTexture; uniform float4 _EffectTexture_ST;
            uniform float4 _EffectColor1;
            uniform float4 _EffectColor2;
            uniform float _Speed;
            uniform float _EffectLerp;
            struct VertexInput {
                float4 vertex : POSITION;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float node_501 = 0.5;
                float node_5019 = 0.25;
                float4 node_8221 = _Time + _TimeEditor;
                float node_8679 = (node_8221.r*_Speed);
                float2 node_4771_tc_rcp = float2(1.0,1.0)/float2( node_501, node_5019 );
                float node_4771_ty = floor(node_8679 * node_4771_tc_rcp.x);
                float node_4771_tx = node_8679 - node_501 * node_4771_ty;
                float2 node_4771 = (i.uv0 + float2(node_4771_tx, node_4771_ty)) * node_4771_tc_rcp;
                float4 node_9334 = tex2D(_EffectTexture,TRANSFORM_TEX(node_4771, _EffectTexture));
                float node_1930 = 2.0;
                float node_9157 = (node_8221.r*(-1.0)*_Speed);
                float2 node_9309_tc_rcp = float2(1.0,1.0)/float2( node_501, node_5019 );
                float node_9309_ty = floor(node_9157 * node_9309_tc_rcp.x);
                float node_9309_tx = node_9157 - node_501 * node_9309_ty;
                float2 node_9309 = (i.uv0 + float2(node_9309_tx, node_9309_ty)) * node_9309_tc_rcp;
                float4 node_3129 = tex2D(_EffectTexture,TRANSFORM_TEX(node_9309, _EffectTexture));
                float3 node_743 = (((_EffectColor2.rgb*pow(node_9334.rgb,node_1930))+(pow(node_3129.rgb,node_1930)*_EffectColor1.rgb))*0.8+0.0);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_6343 = (_MainTex_var.rgb*_MainColor.rgb);
                float3 diffColor = lerp(node_743,node_6343,_EffectLerp);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0.0, specColor, specularMonochrome );
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
