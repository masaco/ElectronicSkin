using UnityEngine;

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

	public static Color ColorBlend(Color colorA, Color colorB)
	{
		Color tempColor = (colorA + colorB)/2f;
		return tempColor;
    }
}