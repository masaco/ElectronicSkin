Shader "Effect/DistortionMesh"
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
			
			struct appdata 
			{
		    	float4 pos      : POSITION;
		    	float2 uv       : TEXCOORD0;
		    	float2 uv1      : TEXCOORD1;
			};
			
			struct v2f 
			{
				float4 pos 		: POSITION;
				float2 uv 		: TEXCOORD0;
				float2 uv1 		: TEXCOORD1;
				float4 c		: COLOR;
			};
			
			v2f vert( appdata v ) 
			{
				v2f o;
				
				o.pos = v.pos;
				o.uv  = v.uv;
				o.uv1 = v.uv1;
				o.c   = o.pos.z;
					
				return o;
			}	
			
			half4 frag(v2f i) : COLOR 
			{
				float tex_r   = tex2D (_MainTex, i.uv1).x;
				float tex_g   = tex2D (_MainTex, i.uv1).y;
				float tex_b   = tex2D (_MainTex, i.uv1).z;
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