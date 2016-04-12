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

	#region Base Variable
	public CharacterColor MainColor = CharacterColor.WHITE;
	public Mesh[] ManMeshs;
	public Material[] ManMaterials;
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

	private float reflashRate = 0.15f;
	//public float SubReflashRate = 0.05f;
	public float FillRate = 0.1f;
	public float BlurRate = 0.01f;

	[System.NonSerialized]
	public Vector3[] meshPointPosition;	
	private ParticleEffectControl[] particleCtrls;
	[System.NonSerialized]
	public int ID;

	public bool isPlayer;
	private bool isReInit;

	public AnimationCurve SFXCurve;
	public AudioClip SFXClip;
	private CharacterColor preColor;
	#endregion

	void Awake ()
	{

		if (!isPlayer)
		{
			GetComponentInChildren<SZVRDevice>().gameObject.SetActive(false);
		}


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
			SoundTrigger soundTrigger;
			#region Init Skeleton
			foreach (ParticleEffectControl peCtrl in GetComponentsInChildren<ParticleEffectControl>())
			{
				switch (peCtrl.name)
				{
					case "Head": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.Head).transform; break;
					case "Chest": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.Chest).transform; peCtrl.name = "spine3"; break;
					case "Spine": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.Spine).transform; peCtrl.name = "spine2"; break;
					case "Hips": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.Hips).transform; break;
					case "RightUpperArm": 
						peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.RightUpperArm).transform; 	
						peCtrl.name = "rightForeArm";
						peCtrl.transform.localEulerAngles += Vector3.forward*180f;
						break;
					case "RightLowerArm": 
						peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.RightLowerArm).transform; 
						peCtrl.name = "rightArm"; 
						peCtrl.transform.localEulerAngles += Vector3.forward*180f;
						break;
					case "RightHand": 
						peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.RightHand).transform;
						peCtrl.transform.localEulerAngles += Vector3.forward*180f;
//						soundTrigger = peCtrl.gameObject.AddComponent<SoundTrigger>();
//						soundTrigger.Init( SFXClip,SFXCurve, ID.ToString());
						break;
					case "LeftUpperArm": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.LeftUpperArm).transform; peCtrl.name = "leftForeArm"; break;
					case "LeftLowerArm": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.LeftLowerArm).transform; peCtrl.name = "leftArm"; break;
					case "LeftHand": 
						peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.LeftHand).transform;
//						soundTrigger = peCtrl.gameObject.AddComponent<SoundTrigger>();
//						soundTrigger.Init( SFXClip,SFXCurve, ID.ToString());
						break;
					case "RightUpperLeg": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.RightUpperLeg).transform; peCtrl.name = "rightUpLeg"; break;
					case "RightLowerLeg": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.RightLowerLeg).transform; peCtrl.name = "rightLeg";break;
					case "RightFoot": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.RightFoot).transform; break;
					case "LeftUpperLeg": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.LeftUpperLeg).transform; peCtrl.name = "leftUpLeg";break;
					case "LeftLowerLeg": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.LeftLowerLeg).transform; peCtrl.name = "leftLeg";break;
					case "LeftFoot": peCtrl.transform.parent = Anim.GetBoneTransform(HumanBodyBones.LeftFoot).transform; break;
				}
			}
			#endregion

		}
		else
		{
			bake = BodyMesh.mesh;
        }		
		meshPointPosition = bake.vertices;
	}

    void Start () {

		#region Switch SkinMesh or NormalMesh
		for ( int i = 0; i < 26938; i++ )//meshPointPosition.Length
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
//		SkinMesh.sharedMesh = ManMeshs [MainColor.GetHashCode ()];
		#endregion

		#region Rename Particle Control Object
		MainEffectControl self = GetComponent<MainEffectControl>();
		foreach (ParticleEffectControl pCtrls in particleCtrls)
		{
			pCtrls.name = ID + "_"+ pCtrls.name;
			pCtrls.Init(self);
        }
		#endregion

		//StartCoroutine(reflash());
		InvokeRepeating("Set", 0f, reflashRate);
		isReInit = true;

	}

	void Update ()
	{
		if (preColor != MainColor) 
		{
			preColor = MainColor;
			ChangeBody (preColor.GetHashCode());
		}
	}

	#region Base Function
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

	public void ChangeBody ( int type)
	{
		SkinMesh.sharedMesh = ManMeshs [type];
		MainColor = (CharacterColor)type;
		Colors = new Color [1];
		InitColor ();
		Material[] tempMat = SkinMesh.materials;

		tempMat[0] = ManMaterials [type];
		tempMat[1] = ManMaterials [type];
		SkinMesh.materials = tempMat;
		foreach (ParticleEffectControl pCtrls in particleCtrls)
			pCtrls.ChangeColor ();
	}

	void Set()
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
	
//	IEnumerator reflash()
//	{
//		while (true)
//		{
//			set();
//			yield return new WaitForSeconds(reflashRate);
//		}
//	}

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
				Colors[i] = new Color(  
					mainColor.r+Random.Range( -0.1f, 0.1f ),
					mainColor.g+Random.Range( -0.1f, 0.1f ),
					mainColor.b+Random.Range( -0.1f, 0.1f ),
					1f
				);
            }
				
        }
		
	}
	#endregion	
}