using UnityEngine;
using System.Collections;

public class createTexture : MonoBehaviour {

	public int resolution = 256;

	private Texture2D texture;

	public AnimationCurve myAc;

	public Transform centerPt;

	private void Awake () {
		texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
		texture.name = "Procedural Texture";
		GetComponent<MeshRenderer>().material.mainTexture = texture;
		FillTexture();
	}

	private void OnEnable () {
		if (texture == null) {
			texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
			texture.name = "Procedural Texture";
			texture.wrapMode = TextureWrapMode.Clamp;
			texture.filterMode = FilterMode.Trilinear;
			texture.anisoLevel = 9;
			GetComponent<MeshRenderer>().material.mainTexture = texture;
		}
		FillTexture();
	}


	private void FillTexture () {
		for (int y = 0; y < resolution; y++) {
			for (int x = 0; x < resolution; x++) {

				Vector3 myPt = UvTo3D (gameObject, new Vector2 (x * 1f / resolution, y * 1f / resolution));
				Color myColor = Color.white * myAc.Evaluate ((myPt-centerPt.position).magnitude -centerPt.localScale.x);
				texture.SetPixel(x, y, myColor);

//				if (Random.value < 0.01f)
//					Debug.Log (x + " " + y + " " +myPt.magnitude+" "+ myColor);
			}
		}
		texture.Apply();
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		FillTexture();
//		if (transform.hasChanged) {
//			transform.hasChanged = false;
//			FillTexture();
//		}
	}

	Vector3 UvTo3D(GameObject g, Vector2 uv)
	{
		Mesh mesh = g.GetComponent<MeshFilter>().mesh;
		int[] tris = mesh.triangles;
		Vector2[] uvs = mesh.uv;
		Vector3[] verts = mesh.vertices;
		for (int i = 0; i < tris.Length; i += 3)
		{
			Vector2 u1  = uvs[tris[i]]; // get the triangle UVs
			Vector2 u2  = uvs[tris[i+1]];
			Vector2 u3  = uvs[tris[i+2]];
			// calculate triangle area - if zero, skip it
			float a = Area(u1, u2, u3); if (a == 0) continue;
			// calculate barycentric coordinates of u1, u2 and u3
			// if anyone is negative, point is outside the triangle: skip it
			float a1  = Area(u2, u3, uv)/a; if (a1 < 0f) continue;
			float a2  = Area(u3, u1, uv)/a; if (a2 < 0f) continue;
			float a3  = Area(u1, u2, uv)/a; if (a3 < 0f) continue;
			// point inside the triangle - find mesh position by interpolation...
			Vector3 p3D  = a1*verts[tris[i]]+a2*verts[tris[i+1]]+a3*verts[tris[i+2]];
			// and return it in world coordinates:
			return transform.TransformPoint(p3D);
		}
		// point outside any uv triangle: return Vector3.zero
		return Vector3.zero;
	}

	// calculate signed triangle area using a kind of "2D cross product":
	float Area(Vector2 p1, Vector2 p2 , Vector2 p3) {
		Vector2  v1 = p1 - p3;
		Vector2  v2 = p2 - p3;
		return (v1.x * v2.y - v1.y * v2.x)/2f;
	}

}
