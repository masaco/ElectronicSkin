using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CharacterColor
{
	RED,
	ORRANGE,
	YELLOW,
	GREEN,
	BLUE,
	VIOLET,
	WHITE
}

public enum MeshType {
	SkinMesh,
	StaticMesh,
}

public class MainEffectControl : MonoBehaviour {
	public CharacterColor MainColor = CharacterColor.WHITE;
	public GameObject Character;
	public MeshType BodyMeshType;
	[System.NonSerialized]
	public SkinnedMeshRenderer SkinMesh;
	public MeshFilter BodyMesh;
	private Transform meshTransform;

	public Color[] Colors;
	public int[] colorPercent = { 1,1,1,1 };
	[System.NonSerialized]
	public int totalPercent;

	public float reflashRate = 0.05f;
	public float SubReflashRate = 0.1f;
	public float FillRate = 0.1f;
	public float BlurRate = 0.01f;

	[System.NonSerialized]
	public Vector3[] meshPointPosition;	
	private ParticleEffectControl[] particleCtrls;
	[System.NonSerialized]
	public int ID;

	void Awake ()
	{
		ID = Random.Range( 100000, 999999 );
		FillRate = Mathf.Clamp01(FillRate);
		InitColor();
		foreach (int i in colorPercent)
		{
			totalPercent += i;
		}
			
		particleCtrls = Character.GetComponentsInChildren<ParticleEffectControl>();

		Mesh bake = new Mesh();
		if (BodyMeshType == MeshType.SkinMesh)
		{
			SkinMesh = GetComponentInChildren<SkinnedMeshRenderer>();
			SkinMesh.BakeMesh(bake);
		}
		else
		{
			bake = BodyMesh.mesh;
        }		
		meshPointPosition = bake.vertices;
		//meshPointPosition = new Vector3[bake.vertexCount];
		//      for ( int i = 0; i < bake.vertexCount; i++ )
		//{
		//	Vector3 tempV3 = bake.vertices[i];
		//	if (float.IsNaN(tempV3.x)) tempV3.x = 0f; 
		//	else if (float.IsInfinity(tempV3.x)) tempV3.x = 1f;
		//	if (float.IsNaN(tempV3.y)) tempV3.y = 0f;
		//	else if (float.IsInfinity(tempV3.y)) tempV3.y = 1f;
		//	if (float.IsNaN(tempV3.z)) tempV3.z = 0f;
		//	else if (float.IsInfinity(tempV3.z)) tempV3.z = 1f;

		//	meshPointPosition[i] = tempV3;
		//      }
	}

    void Start () {

		
		for ( int i = 0; i < meshPointPosition.Length; i++ )
		{
			foreach (ParticleEffectControl pCtrls in particleCtrls)
			{
				if (BodyMeshType == MeshType.SkinMesh)
				{
					if (pCtrls.bounds.Contains(SkinMesh.transform.TransformPoint(meshPointPosition[i])))
						pCtrls.meshID.Add(i);
				}					
				else
				{
					if (pCtrls.bounds.Contains(BodyMesh.transform.TransformPoint(meshPointPosition[i])))
						pCtrls.meshID.Add(i);
				}
					
			}
		}

		MainEffectControl self = GetComponent<MainEffectControl>();
		foreach (ParticleEffectControl pCtrls in particleCtrls)
		{
			pCtrls.name = ID + "_ParticleControl";
			pCtrls.Init(self);
        }

		StartCoroutine(reflash());

	}

	void set()
	{
		if (BodyMeshType == MeshType.SkinMesh)
		{
			Mesh bake = new Mesh();
			SkinMesh.BakeMesh(bake);
			meshPointPosition = bake.vertices;
		}			
	}

	public Vector3[] GetMeshPoint ( int[] meshID )
	{
		Vector3[] tempV3array = new Vector3[meshID.Length];

		for( int i = 0; i < meshID.Length; i++ )
		{
			tempV3array[i] = meshPointPosition[meshID[i]];
        }
		return tempV3array;
    }

	IEnumerator reflash()
	{
		while (true)
		{
			set();
			yield return new WaitForSeconds(reflashRate);
		}
	}

	void InitColor ()
	{
		if (Colors.Length < 4 )
		{
			Colors = new Color[4];
			Color mainColor = Color.white;
            switch (MainColor)
			{
				case CharacterColor.RED: mainColor		= Color.red;								break;
				case CharacterColor.ORRANGE: mainColor	= new Color(255f, 121f, 0f, 255f)/ 255f;	break;
				case CharacterColor.YELLOW: mainColor	= new Color(255f, 237f, 0f, 255f) / 255f;	break;
				case CharacterColor.GREEN: mainColor	= new Color(0f, 255f, 76f, 255f) / 255f;	break;
				case CharacterColor.BLUE: mainColor		= new Color(0f, 44f, 255f, 255f) / 255f;	break;
				case CharacterColor.VIOLET: mainColor	= new Color(251,0f, 255f, 255f) / 255f;		break;
			}

			for (int i = 0; i < 4; i++)
			{
				Colors[i] = mainColor;
            }
				
        }
		
	}
}
