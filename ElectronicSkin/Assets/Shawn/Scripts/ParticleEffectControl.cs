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

	string keyWord = "0";
	MainEffectControl meshCtrl;
	[System.NonSerialized]
	public List<int> meshID = new List<int>();
	[System.NonSerialized]
	public Bounds bounds;

	[System.NonSerialized]
	public Color[] Colors;
	private int[] colorPercent;
	private int totalPercent;
    private float reflashRate = 0.1f;
	private float fillRate = 0.1f;
    private float blurRate = 0.01f;
	private ParticleSystem ps;
	private ParticleSystem.Particle[] particle;

	private float EffectDistance = 1f;

	private int Count;

	private Vector3 direction;
	private Dictionary<Transform, Vector3> collisionObjsDict = new Dictionary<Transform, Vector3>();
	private List<ParticleInfo> collsionObjList = new List<ParticleInfo>();

	[System.NonSerialized]
	public int ColorID;

	void Awake () {
		bounds = GetComponent<BoxCollider>().bounds;
		Destroy(GetComponent<MeshRenderer>());
		Destroy(GetComponent<MeshFilter>());
		float size = Vector3.Magnitude( transform.localScale );
		EffectDistance = size*1.5f;
    }

	public void Init (MainEffectControl mCtrl )
	{
		meshCtrl = mCtrl;
		keyWord			= meshCtrl.ID.ToString();
        Colors			= meshCtrl.Colors;
		colorPercent	= meshCtrl.colorPercent;
		totalPercent	= meshCtrl.totalPercent;
		fillRate		= meshCtrl.FillRate;
		blurRate		= meshCtrl.BlurRate;
		reflashRate		= meshCtrl.SubReflashRate;
		ColorID			= Mathf.FloorToInt( Mathf.Pow(2f ,meshCtrl.MainColor.GetHashCode()));
        ps = GetComponent<ParticleSystem>();
		particle = new ParticleSystem.Particle[ps.maxParticles];
		if (meshCtrl.isPlayer)
			ps.startSize = 0.003f;
		Count = Mathf.FloorToInt(meshID.Count * fillRate);
		ps.startSize = 0.0001f;
		StartCoroutine(reflash());
		StartCoroutine(FadeInParticle());
    }

	IEnumerator FadeInParticle()
	{
		float during = 0f;
		while (during < 1f)
		{
			yield return new WaitForEndOfFrame();
			during += Time.deltaTime;
			ps.startSize = 0.0005f + 0.0045f * during/1f;
		}
	}

	void FadeOutParticle( )
	{
		ps.startSize *= 0.8f;
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.name.Contains(keyWord) || other.tag == "Untagged")
		{
			return;
		}

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

	void OnTriggerExit(Collider other)
	{
		if (other.name.Contains(keyWord))
		{
			return;
		}
			
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

	void set()
	{
        int lastCount = ps.particleCount;

		ps.Emit(Count);
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
			selectedID[i] = meshID[Random.Range(0, meshID.Count)];

		Vector3[] selectVecter3 = meshCtrl.GetMeshPoint(selectedID);
		
        for (int i = 0; i < Count; i++)
		{
			if(meshCtrl.BodyMeshType == MeshType.SkinMesh)
				particle[i + lastCount].position = meshCtrl.SkinMesh.transform.TransformPoint(selectVecter3[i])
			+ new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * blurRate;
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

		ps.SetParticles(particle, ps.particleCount);
	}

	Color RandColor(Color[] color)
	{
		Color result = color[Random.Range(0, color.Length)];
		return result;
	}

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

	IEnumerator reflash()
	{
		while (true)
		{
			set();
			yield return new WaitForSeconds(reflashRate);
		}
	}
}
