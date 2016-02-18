using UnityEngine;

public class CMYK
{
	public float C;
	public float M;
    public float Y;
	public float K;
	
	public void Show()
	{
		Debug.Log( "C:"+C+" M:"+M+" Y:"+Y+" K:"+K ); 
	}
} 

public static class ColorConvert {

	public static Color HSV2RGB(float H, float S, float V)
	{
		if (S == 0f)
			return new Color(V,V,V);
		else if (V == 0f)
			return Color.black;
		else
		{
			Color col = Color.black;
			float Hval = H * 6f;
			int sel = Mathf.FloorToInt(Hval);
			float mod = Hval - sel;
			float v1 = V * (1f - S);
			float v2 = V * (1f - S * mod);
			float v3 = V * (1f - S * (1f - mod));
			switch (sel + 1)
			{
			case 0:
				col.r = V;
				col.g = v1;
				col.b = v2;
				break;
			case 1:
				col.r = V;
				col.g = v3;
				col.b = v1;
				break;
			case 2:
				col.r = v2;
				col.g = V;
				col.b = v1;
				break;
			case 3:
				col.r = v1;
				col.g = V;
				col.b = v3;
				break;
			case 4:
				col.r = v1;
				col.g = v2;
				col.b = V;
				break;
			case 5:
				col.r = v3;
				col.g = v1;
				col.b = V;
				break;
			case 6:
				col.r = V;
				col.g = v1;
				col.b = v2;
				break;
			case 7:
				col.r = V;
				col.g = v3;
				col.b = v1;
				break;
			}
			col.r = Mathf.Clamp(col.r, 0f, 1f);
			col.g = Mathf.Clamp(col.g, 0f, 1f);
			col.b = Mathf.Clamp(col.b, 0f, 1f);
			return col;
		}
	}


	public static Vector3 RGB2HSV( Color myColor )
	{
		float h, s,v;
		float min, max, delta;
		
		min = Mathf.Min( myColor.r, myColor.g, myColor.b );
		max = Mathf.Max( myColor.r, myColor.g, myColor.b );
		v = max;				// v
		
		delta = max - min;

		if (delta==0f) return new Vector3(0f,0f,1f);
		
		if( max != 0f )
			s = delta / max;		// s
		else {
			// r = g = b = 0		// s = 0, v is undefined
			s = 0f;
			h = -1f;
			return new Vector3(h,s,v);
		}
		
		if( myColor.r == max )
			h = ( myColor.g - myColor.b ) / delta;		// between yellow & magenta
		else if( myColor.g == max )
			h = 2 + ( myColor.b - myColor.r ) / delta;	// between cyan & yellow
		else
			h = 4 + ( myColor.r - myColor.g ) / delta;	// between magenta & cyan
		
		h *= 60f;				// degrees
		if( h < 0f )
			h += 360f;

		return new Vector3(h,s,v);
	}

	public static Color HSVBlend(Color colorA, Color colorB)
	{
		Vector3 co1 = RGB2HSV(colorA);
		Vector3 co2 = RGB2HSV(colorB);

		float H = 0;
		if (Mathf.Abs(co1.x - co2.x) <= 180f)
			H = (co1.x + co2.x) / 2f / 360f;
		else
			H = (co1.x + co2.x + 360f) / 2f / 360f;

		return HSV2RGB( H, 1f, 0.8f );
	}

	public static Color ColorBlend(Color colorA, Color colorB)
	{
		Color tempColor = (colorA + colorB)/2f;
		return tempColor;
    }

	//public enum CharacterColor
	//{
	//	RED, 1
	//	ORRANGE, 2
	//	YELLOW, 4
	//	GREEN, 8
	//	BLUE, 16
	//	PURPLE, 32
	//	WHITE 64
	//}

	public static CMYK RGBtoCMYK( Color rgb )
	{
		CMYK cmyk = new CMYK();
		float C = 1f - rgb.r;
		float M = 1f - rgb.g;
		float Y = 1f - rgb.b;
		float K = 1f;

		K = Mathf.Min(Mathf.Min(C, M), Y);

		if (K != 1)
		{
			cmyk.C = (C - K) / (1f - K);
			cmyk.M = (M - K) / (1f - K);
			cmyk.Y = (Y - K) / (1f - K);
		}

		cmyk.K = K;

		return cmyk;
	}

	public static Color CMYKtoRGB(CMYK cmyk)
	{
		Color rgb = Color.white;

		float C = 1f;
		float M = 1f;
		float Y = 1f;

        float K = cmyk.K;

		if (K < 1f)
		{
			C = cmyk.C * (1 - K) + K;
			M = cmyk.M * (1 - K) + K;
			Y = cmyk.Y * (1 - K) + K;
		}

		rgb = new Color( 1f-C, 1f-M, 1f-Y, 1f );

		return rgb; 
	}

	public static Color CMYKBlend( Color co1, Color co2)
	{
		CMYK cmyk1 = RGBtoCMYK(co1);
		CMYK cmyk2 = RGBtoCMYK(co2);
		CMYK cmyk = new CMYK();

		cmyk.C = (cmyk1.C + cmyk2.C)/2f;
		cmyk.M = (cmyk1.M + cmyk2.M)/2f;
		cmyk.Y = (cmyk1.Y + cmyk2.Y)/2f;
		cmyk.K = (cmyk1.K + cmyk2.K)/2f;

		cmyk.Show();

		return CMYKtoRGB(cmyk);	 
	}

	public static Color PreColorBlend( int numA, int numB )
	{
		Color tempColor = Color.white;
		int sum = numA + numB;

		switch (sum)
		{
			#region red
			//red+orange
			case 3:
				tempColor = new Color( 255f, 50f, 0f, 255f) / 255f;
                break;
			//red+yellow
			case 5:
				tempColor = new Color(255f, 128f, 0f, 255f) / 255f;
				break;
			//red+green
			case 9:
				tempColor = new Color(128f, 128f, 0f, 255f) / 255f;
				break;
			//red+blue
			case 17:
				tempColor = new Color(128f, 0f, 128f, 255f) / 255f;
				break;
			//red+purple
			case 33:
				tempColor = new Color(230f, 0f, 102f, 255f) / 255f;
				break;
			//red+white
			case 65:
				tempColor = new Color(255f, 100f, 100f, 255f) / 255f;
				break;
			#endregion
			#region orange
			//orange+yellow
			case 6:
				tempColor = new Color(255f, 179f, 0f, 255f) / 255f;
				break;
			//orange+green
			case 10:
				tempColor = new Color(128f, 179f, 0f, 255f) / 255f;
				break;
			//orange+blue
			case 18:
				tempColor = new Color(128f, 50f, 128f, 255f) / 255f;
				break;
			//orange+purple
			case 34:
				tempColor = new Color(205f, 50f, 128f, 255f) / 255f;
				break;
			//orange+white
			case 66:
				tempColor = new Color(255f, 179f, 128f, 255f) / 255f;
				break;
			#endregion
			#region yellow
			//yellow+green
			case 12:
				tempColor = new Color(128f, 255f, 0f, 255f) / 255f;
				break;
			//yellow+blue
			case 20:
				tempColor = new Color(128f, 128f, 128f, 255f) / 255f;
				break;
			//yellow+purple
			case 36:
				tempColor = new Color(255f, 179f, 128f, 255f) / 255f;
				break;
			//yellow+white
			case 68:
				tempColor = new Color(255f, 179f, 128f, 255f) / 255f;
				break;
			#endregion
			#region green
			//green+blue
			case 24:
				tempColor = new Color(255f, 179f, 128f, 255f) / 255f;
				break;
			//green+purple
			case 40:
				tempColor = new Color(255f, 179f, 128f, 255f) / 255f;
				break;
			//green+white
			case 72:
				tempColor = new Color(255f, 179f, 128f, 255f) / 255f;
				break;
			#endregion
			#region blue
			//blue+purple
			case 48:
				tempColor = new Color(255f, 179f, 128f, 255f) / 255f;
				break;
			//blue+white
			case 80:
				tempColor = new Color(255f, 179f, 128f, 255f) / 255f;
				break;
			#endregion
			#region purple
			//purple+white
			case 96:
				tempColor = new Color(255f, 179f, 128f, 255f) / 255f;
				break;
			#endregion
		}
		return tempColor;
	}
}