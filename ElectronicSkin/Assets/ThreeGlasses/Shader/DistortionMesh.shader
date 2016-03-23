Shader "Effect/DistortionMesh"
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
//		_TimeWarpConstants ("Time Warp Constants", 2D) = "" {}
	}
	
	Subshader 
	{
		Pass 
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }      
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			sampler2D _MainTex;
//			sampler2D _TimeWarpConstants;

//			float4x4  _TimeWarpStart;
//			float4x4  _TimeWarpEnd;
			
			struct appdata 
			{
		    	float4 pos      : POSITION;
		    	float2 uv       : TEXCOORD0;
//		    	float2 uv1      : TEXCOORD1;
				float2 uvR      : TEXCOORD1;
		    	float4 uvGB     : TANGENT;
			};
			
			struct v2f 
			{
				float4 pos 		: POSITION;
				float2 uv 		: TEXCOORD0;
//				float2 uv1 		: TEXCOORD1;
				float2 uvR 		: TEXCOORD1;
				float2 uvG 		: TEXCOORD2;
				float2 uvB 		: TEXCOORD3;
				float4 c		: COLOR;
			};
			
			v2f vert( appdata v ) 
			{
				v2f o;
				
				o.pos	= v.pos;
//				o.uv	= v.uv;
//				o.uv1	= v.uv1;

				o.uv	= v.uv;
				o.uvR	= v.uvR;
				o.uvG.x = v.uvGB.x;
				o.uvG.y = v.uvGB.y;
				o.uvB.x = v.uvGB.z;
				o.uvB.y = v.uvGB.w;
				o.c		= o.pos.z;

//				float twLerpEnd       = v.uvB.z;
//				float twLerpStart     = 1.0f - v.uvB.z;
//   			float4x4 lerpedEyeRot = (_TimeWarpStart * twLerpStart) + (_TimeWarpEnd * twLerpEnd);

//				o.uvR = float3(TimewarpTexCoordToWarpedPos(v.uvR.xy, lerpedEyeRot), 1);
//				o.uvG = float3(TimewarpTexCoordToWarpedPos(v.uvG.xy, lerpedEyeRot), 1);
//				o.uvB = float3(TimewarpTexCoordToWarpedPos(v.uvB.xy, lerpedEyeRot), 1);
					
				return o;
			}	
			
			half4 frag(v2f i) : COLOR 
			{
//				float tex_r   = tex2D (_MainTex, i.uv1).x;
//				float tex_g   = tex2D (_MainTex, i.uv1).y;
//				float tex_b   = tex2D (_MainTex, i.uv1).z;
				float tex_r   = tex2D (_MainTex, i.uvR).x;
				float tex_g   = tex2D (_MainTex, i.uvG).y;
				float tex_b   = tex2D (_MainTex, i.uvB).z;
		    	float alpha = 1;

		    	if (any(clamp(i.uv, float2(0, 0), float2(1, 1)) - i.uv))
		    	{
		        	tex_r  = 0;
		    		tex_g  = 0;
		    		tex_b  = 0;
				}
				
		    	return half4(tex_r, tex_g, tex_b, alpha);
			}
			
			ENDCG 
		}
	}
	Fallback off
}