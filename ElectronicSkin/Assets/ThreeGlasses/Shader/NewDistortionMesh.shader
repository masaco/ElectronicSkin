Shader "Custom/NewDistortionMesh" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
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

			float2 EyeToSourceUVScale;
			float2 EyeToSourceUVOffset;
			float4x4 EyeRotationStart;
			float4x4 EyeRotationEnd;

			float2 TimewarpTexCoordToWarpedPos(float2 inTexCoord, float4x4 rotMat)
			{
				// Vertex inputs are in TanEyeAngle space for the R,G,B channels (i.e. after chromatic aberration and distortion).
				// These are now "real world" vectors in direction (x,y,1) relative to the eye of the HMD.	
				// Apply the 3x3 timewarp rotation to these vectors.
				float3 transformed = float3( mul ( rotMat, float4(inTexCoord.xy, 1, 1) ).xyz);
				// Project them back onto the Z=1 plane of the rendered images.
				float2 flattened = transformed.xy / transformed.z;
				// Scale them into ([0,0.5],[0,1]) or ([0.5,0],[0,1]) UV lookup space (depending on eye)
				return flattened * EyeToSourceUVScale + EyeToSourceUVOffset;
			}
			
			struct appdata 
			{
		    	float4 Position		: POSITION;
				float4 Color		: COLOR0;
				float2 TexCoord0	: TEXCOORD0;
				float2 TexCoord1	: TEXCOORD1;
				float2 TexCoord2	: TEXCOORD2;
			};
			
			struct v2f 
			{
				float4 oPosition	: POSITION;
				float4 oColor		: COLOR;
				float3 oTexCoord0	: TEXCOORD0;
				float3 oTexCoord1	: TEXCOORD1;
				float3 oTexCoord2	: TEXCOORD2;
			};
			
			v2f vert( appdata v ) 
			{
				v2f o;

				oPosition.x = Position.x;
				oPosition.y = Position.y;
				oPosition.z = 0.5;
				oPosition.w = 1.0;

				float timewarpLerpFactor = Color.a;
				float4x4 lerpedEyeRot = lerp(EyeRotationStart, EyeRotationEnd, timewarpLerpFactor);	

				oTexCoord0 = float3(TimewarpTexCoordToWarpedPos(TexCoord0, lerpedEyeRot), 1);
				oTexCoord1 = float3(TimewarpTexCoordToWarpedPos(TexCoord1, lerpedEyeRot), 1);
				oTexCoord2 = float3(TimewarpTexCoordToWarpedPos(TexCoord2, lerpedEyeRot), 1);

				oColor = Color.r;
					
				return o;
			}	
			
			half4 frag(v2f i) : COLOR 
			{
				float tex_r   = tex2D (_MainTex, i.oTexCoord0).x;
				float tex_g   = tex2D (_MainTex, i.oTexCoord1).y;
				float tex_b   = tex2D (_MainTex, i.oTexCoord2).z;
		    	float alpha = 1;

		    	if (any(clamp(float2(i.oPosition.x,i.oPosition.y), float2(0, 0), float2(1, 1)) - float2(i.oPosition.x,i.oPosition.y)))
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
