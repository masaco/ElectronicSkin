using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleInfo
{
	public Transform TargetTransform;
	public Vector3 CollisionCenter;
	public Color[] TargetMainColor;
	public int ColorID;
}

#pragma warning disable 0414, 0618
public class ParticleEffectControl : MonoBehaviour {

	#region BaseVariable
	string colliderID = "0";
	MainEffectControl meshCtrl;
//	[System.NonSerialized]

//	public Dictionary< int, List<int> >MeshNewID = new Dictionary<int, List<int>>();
	public List<int> meshID = new List<int>();
	[System.NonSerialized]
	public Bounds bounds;

	[System.NonSerialized]
	public Color[] Colors;
	private int[] colorPercent;
	private int totalPercent;
	private bool isParticleReady;
    private float reflashRate = 0.15f;
	private float fillRate = 0.1f;
	private float blurRate = 0.01f;
	private ParticleSystem ps;
	private Renderer psRenderer;
	private ParticleSystem.Particle[] particle;
	private Shader origShader;
	private Shader tranShader;

	private float EffectDistance = 1f;

	private int Count;

	
	[System.NonSerialized]
	public int ColorID;
	#endregion

	#region Calculate Direction Variable
	private Vector3 direction;
	private Dictionary<Transform, Vector3> collisionObjsDict = new Dictionary<Transform, Vector3>();
	private List<ParticleInfo> collsionObjList = new List<ParticleInfo>();

	private bool isCollsionObj;
	private Vector3 collisionObjDir;
	private BoxCollider boxCollider;
	private ColliderManager collliManager;
	private BoxCollider headBox;
	public bool IsDetectObj;
	#endregion

	void Awake() {
		bounds = GetComponent<BoxCollider>().bounds;
		if (GetComponent<MeshRenderer>())
			Destroy(GetComponent<MeshRenderer>());
		if (GetComponent<MeshFilter>())
			Destroy(GetComponent<MeshFilter>());
		boxCollider = GetComponent<BoxCollider>();
		if (GetComponent<ColliderManager>())
			collliManager = GetComponent<ColliderManager>();
        float size = Vector3.Magnitude(transform.localScale);
		EffectDistance = size * 1.5f;
		
	}

	public void Init(MainEffectControl mCtrl)
	{
		origShader = Shader.Find("Particles/Additive (Soft)");
		tranShader = Shader.Find("Particles/Additive");
		#region InitMainEffectControl
		meshCtrl = mCtrl;
		colliderID = meshCtrl.ID.ToString();
		if ( collliManager )
			collliManager.colliderID = colliderID;
		Colors = meshCtrl.Colors;
		colorPercent = meshCtrl.colorPercent;
		totalPercent = meshCtrl.totalPercent;
		fillRate = meshCtrl.FillRate;
		blurRate = meshCtrl.BlurRate;
		headBox = meshCtrl.HeadBox;
		//reflashRate = meshCtrl.SubReflashRate;
		ColorID = Mathf.FloorToInt(Mathf.Pow(2f, meshCtrl.MainColor.GetHashCode()));
		#endregion
		ps = GetComponent<ParticleSystem>();
		particle = new ParticleSystem.Particle[ps.maxParticles];
		if (meshCtrl.isPlayer)
			ps.startSize = 0.003f;
		if (meshCtrl.isPlayer && name.Contains (colliderID + "_Head")) {
			return;
			tag = "Untagged";
		}
			
		Count = Mathf.FloorToInt(meshID.Count * fillRate);
		ps.startSize = 0.0001f;
		psRenderer = ps.GetComponent<Renderer>();
		psRenderer.material.shader = tranShader;
		Color newColor = new Color(Colors[0].r, Colors[0].g, Colors[0].b, 1f);
		psRenderer.material.SetColor("_TintColor", newColor);
		psRenderer.material.shader = origShader;

		InvokeRepeating("Set", 0f, reflashRate);
		//StartCoroutine(reflash());
		StartCoroutine(FadeInParticle());

	}

	public void ChangeColor()
	{
		Colors = meshCtrl.Colors;
		ColorID = Mathf.FloorToInt(Mathf.Pow(2f, meshCtrl.MainColor.GetHashCode()));
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name.Contains(colliderID) || other.tag.Contains( "Untagged" ))
		{
			return;
		}

		if (other.tag.Contains( "CollisionBody" ))
		{
			collisionObjsDict.Add(other.transform, (transform.position - other.transform.position));
			StartCoroutine(ImpulseCalculate(other.transform));

			bool isTargetInList = false;
			foreach (ParticleInfo tempInfo in collsionObjList)
			{
				if (tempInfo.TargetTransform == other.transform)
				{
					isTargetInList = true;
					break;
				}
			}

			if (!isTargetInList)
			{
				ParticleInfo tempInfo = new ParticleInfo();
				ParticleEffectControl peCtrl = other.GetComponent<ParticleEffectControl>();
				tempInfo.TargetTransform = other.transform;
				tempInfo.TargetMainColor = peCtrl.Colors;
				tempInfo.CollisionCenter = other.transform.position * 0.5f + transform.position * 0.5f;
				tempInfo.ColorID = peCtrl.ColorID;
				collsionObjList.Add(tempInfo);
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag.Contains( "CollisionObj" ) && isParticleReady)
		{
			isCollsionObj = true;
			#region detectDirectionSwich
			Vector3[] detectDir = new Vector3[27];

			if (IsDetectObj)
			{
				int detectValue = 1;				
				int detectCount = 0;				
				for (int i = -detectValue; i < detectValue+1; i++)
				{
					for (int j = -detectValue; j < detectValue+1; j++)
					{
						for (int k = -detectValue; k < detectValue+1; k++)
						{
							detectDir[detectCount] = new Vector3( i* boxCollider.size.x, j*boxCollider.size.y, k* boxCollider.size.z);
                            detectCount++;
                        }
					}
				}
			}
			#endregion

			float nearistDis = 100f;
			Vector3 hitPoint = Vector3.zero;
			Vector3 hitNormal = Vector3.zero;
			for (int i = 0; i < 27; i++)
			{
				RaycastHit hit;
				Vector3 origin = transform.TransformPoint(detectDir[i] * -0.8f);
				Vector3 to = transform.TransformPoint(detectDir[i] * 0.5f);
                Vector3 direction = (to-origin);
				
				
				//Debug.DrawLine(origin, to, Color.green);
				if (Physics.Raycast(origin, direction, out hit, 5f, 1))
				{
					float Dis = Vector3.Distance(hit.point, origin);
					if (Dis < nearistDis)
					{
						nearistDis = Dis;
						hitPoint = hit.point;
						hitNormal = hit.normal;
					}
					//Debug.DrawLine(hit.point, origin, Color.blue);
				}							
				
			}
			//Debug.DrawLine(hitPoint, hitPoint + hitNormal * 3f, Color.yellow);
			collisionObjDir = hitNormal;
        }

	}

	void OnTriggerExit(Collider other)
	{
		if (other.name.Contains(colliderID) || other.tag.Contains("Untagged"))
		{
			return;
		}

		if (other.tag.Contains( "CollisionBody") )
		{
			collisionObjsDict.Remove(other.transform);
			foreach (ParticleInfo tempInfo in collsionObjList)
			{
				if (tempInfo.TargetTransform == other.transform)
				{
					collsionObjList.Remove(tempInfo);
					break;
				}
			}
		}
		else if (other.tag.Contains( "CollisionObj"))
		{
			isCollsionObj = false;
		}
	}
	
	void Set()
	{
		#region InitParticle
		int lastCount = ps.particleCount;
		
		ps.Emit(Count);
		ps.startLifetime = 0.8f;
		ps.GetParticles(particle);

		foreach (ParticleInfo tempInfo in collsionObjList)
		{
			Vector3 collisionCenter = (tempInfo.TargetTransform.position + transform.position) * 0.5f;
			if (Vector3.Distance(tempInfo.TargetTransform.position, transform.position) < EffectDistance)
			{
				if (collsionObjList.Contains(tempInfo))
					collsionObjList[collsionObjList.IndexOf(tempInfo)].CollisionCenter = collisionCenter;
			}
		}

		int[] selectedID = new int[Count];
		for (int i = 0; i < Count; i++)
		{
			selectedID[i] = meshID[Random.Range(0, meshID.Count)];
		}

		Vector3[] selectVecter3 = meshCtrl.GetMeshPoint(selectedID);
		
		#endregion

		for (int i = 0; i < Count; i++)
		{
			#region CreateParticle & SetColor
			if (meshCtrl.BodyMeshType == MeshType.SkinMesh)
			{				
				particle[i + lastCount].position = meshCtrl.SkinMesh.transform.TransformPoint(selectVecter3[i])
				+ new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * blurRate;
				if ( meshCtrl.isPlayer )
					if ( meshCtrl.HeadBox.bounds.Contains(particle[i + lastCount].position) )
						continue;
			}				
			else
				particle[i + lastCount].position = meshCtrl.BodyMesh.transform.TransformPoint(selectVecter3[i])
			+ new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * blurRate;

			Vector3 OutwardV3 = particle[i + lastCount].position - transform.position;

			if (Colors.Length > 0 && Colors.Length == colorPercent.Length)
			{
				int colorchoice = Random.Range(0, totalPercent);
				int percentRange = 0;
				for (int j = 0; j < colorPercent.Length; j++)
				{
					percentRange += colorPercent[j];
					if (colorchoice < percentRange)
					{
						colorchoice = j;
						break;
					}
				}
				particle[i + lastCount].color = Colors[colorchoice];
			}
			#endregion

			if ( isCollsionObj )
			{
				if (IsDetectObj)
				{
					Vector3 impulseDir = Random.onUnitSphere;
					if (Vector3.Angle(collisionObjDir, impulseDir) < 50f)
						particle[i + lastCount].velocity = impulseDir * 0.8f;
					particle[i + lastCount].startSize = 0.007f;
					if (psRenderer.material.shader != tranShader)
						psRenderer.material.shader = tranShader;
				}				
            }
			else
			{
				if ( IsDetectObj )
				{
					if (psRenderer.material.shader != origShader)
						psRenderer.material.shader = origShader;
				}

				#region Set Particle Velocity & BlendColor
				if (collsionObjList.Count != 0)
				{				
					foreach (ParticleInfo tempInfo in collsionObjList)
					{
						float disToCollisionCenter = Vector3.Distance(tempInfo.CollisionCenter, particle[i + lastCount].position);
						float MaxArea = EffectDistance / 2f;
						float Area = Mathf.Clamp(MaxArea - disToCollisionCenter, 0, MaxArea);
						if (disToCollisionCenter < Area)
						{
							float fadeValue = 3f;
							Color targetColor = ColorConvert.HSVBlend(RandColor(Colors), RandColor(tempInfo.TargetMainColor));
							//Color blendColor = (RandColor(Colors) * disToCollisionCenter / fadeValue
							//	+ ColorConvert.ColorBlend(RandColor(Colors), RandColor(tempInfo.TargetMainColor)) * (Area - disToCollisionCenter) * fadeValue)
							//	/ MaxArea;
							Color blendColor = (RandColor(Colors) * disToCollisionCenter / fadeValue
								+ ColorConvert.HSVBlend(RandColor(Colors), RandColor(tempInfo.TargetMainColor)) * (Area - disToCollisionCenter) * fadeValue)
								/ MaxArea;

							//float blendAlpha = 1f;
							//if (collisionObjsDict.ContainsKey(tempInfo.TargetTransform))
							//	blendAlpha -= collisionObjsDict[tempInfo.TargetTransform].magnitude / Area;
							particle[i + lastCount].color = new Color(blendColor.r, blendColor.g, blendColor.b, 1f);

							particle[i + lastCount].velocity = GetCompositionVector() * Random.Range(0.4f, 0.8f)
								+ OutwardV3 * Random.Range(1.5f, 2.5f) * Mathf.Clamp01(GetCompositionVector().magnitude);
						}
					}
				}
				#endregion
			}

			particle[i + lastCount].velocity += Vector3.up*0.02f;
		}

		ps.SetParticles(particle, ps.particleCount);
	}	

	#region Calculate Impulse

	Vector3 GetCompositionVector()
	{
		Vector3 compositionVector = Vector3.zero;
		foreach (KeyValuePair<Transform, Vector3> keyValue in collisionObjsDict)
			compositionVector += keyValue.Value;
		return compositionVector;
	}

	IEnumerator ImpulseCalculate(Transform objTransform)
	{
		float distance = 0f;
		if (collisionObjsDict.ContainsKey(objTransform))
		{
			Vector3 v3 = collisionObjsDict[objTransform];
			collisionObjsDict[objTransform] *= 0.8f;
			collisionObjsDict[objTransform] -= v3 * 0.1f;
			distance = collisionObjsDict[objTransform].magnitude;
		}
		yield return new WaitForSeconds(0.2f);
		if (distance > 0.1f)
		{
			StartCoroutine(ImpulseCalculate(objTransform));
		}
		else
		{
			collisionObjsDict.Remove(objTransform);
		}
	}
	#endregion

	#region Base Function

	Color RandColor(Color[] color)
	{
		Color result = color[Random.Range(0, color.Length)];
		return result;
	}

	IEnumerator FadeInParticle()
	{
		float during = 0f;
		while (during < 1f)
		{
			yield return new WaitForEndOfFrame();
			during += Time.deltaTime;
			ps.startSize = 0.0005f + 0.0035f * during / 1f;
		}
		isParticleReady = true;
    }

	void FadeOutParticle()
	{
		ps.startSize *= 0.8f;
	}

	IEnumerator reflash()
	{
		while (true)
		{
			Set();
			yield return new WaitForSeconds(reflashRate);
		}
	}
	#endregion
//	void OnDrawGizmosSelected() {
//		Gizmos.color = Color.red;
//		Gizmos.DrawSphere ( transform.position +transform.lossyScale.y/2f*transform.up, 0.05f);
//		Gizmos.color = Color.blue;
//		Gizmos.DrawSphere ( transform.position -transform.lossyScale.y/2f*transform.up, 0.05f);
//	}
}
