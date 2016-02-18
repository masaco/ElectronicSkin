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
	PURPLE,
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

	public bool isPlayer;

	private bool isReInit;

	void Awake ()
	{
		
		if (GetComponentInChildren<Camera>())
			isPlayer = true;

		if (isPlayer)
			InvokeRepeating("Unload", 25f, 25f);

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
			
			Animator Anim = GetComponent<Animator>();
			foreach (ParticleEffectControl peCtrl in GetComponentsInChildren<ParticleEffectControl>())
			{
				switch (peCtrl.name)
				{
					case "Head": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.Head).transform; break;
					case "Chest": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.Chest).transform; break;
					case "Spine": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.Spine).transform; break;
					case "Hips": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.Hips).transform; break;
					case "RightUpperArm": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.RightUpperArm).transform; break;
					case "RightLowerArm": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.RightLowerArm).transform; break;
					case "RightHand": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.RightHand).transform; break;
					case "LeftUpperArm": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.LeftUpperArm).transform; break;
					case "LeftLowerArm": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.LeftLowerArm).transform; break;
					case "LeftHand": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.LeftHand).transform; break;
					case "RightUpperLeg": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.RightUpperLeg).transform; break;
					case "RightLowerLeg": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.RightLowerLeg).transform; break;
					case "RightFoot": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.RightFoot).transform; break;
					case "LeftUpperLeg": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.LeftUpperLeg).transform; break;
					case "LeftLowerLeg": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.LeftLowerLeg).transform; break;
					case "LeftFoot": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.LeftFoot).transform; break;
				}
			}
					
		}
		else
		{
			bake = BodyMesh.mesh;
        }		
		meshPointPosition = bake.vertices;
	}

	void Unload()
	{
		Resources.UnloadUnusedAssets();
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
		isReInit = true;

	}

	void ReInit()
	{
		if (isReInit)
		{
			MainEffectControl self = GetComponent<MainEffectControl>();
			foreach (ParticleEffectControl pCtrls in particleCtrls)
			{
				pCtrls.Init(self);
			}
		}
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
				case CharacterColor.PURPLE: mainColor	= new Color(251,0f, 255f, 255f) / 255f;		break;
			}

			for (int i = 0; i < 4; i++)
			{
				Colors[i] = mainColor;
            }
				
        }
		
	}
}
