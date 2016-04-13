using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]
public class SoundTrigger : MonoBehaviour {
	private AnimationCurve pitchCurve;
	private BoxCollider boxCol;
	private Transform target;
	private Transform preTarget;
	private AudioSource audio;
	private float audioTimer = 0f;
	private Vector3 detectNum;
	private BoxCollider targetCol;
	private string colliderID;
	private bool isReady;

	void Awake()
	{
		audio = GetComponent<AudioSource>();
		audio.playOnAwake = false;
		audio.loop = true;
		boxCol = GetComponent<BoxCollider>();
		Vector3 newScale = transform.localScale.normalized;
		float minValue = Mathf.Min (newScale.x, Mathf.Min (newScale.y, newScale.z));
		detectNum = new Vector3( (int)(newScale.x/minValue), (int)(newScale.y/minValue), (int)(newScale.z/minValue) );
	}

	public void Init( AudioClip clip, AnimationCurve curve, string ID ){
		audio.clip = clip;
		pitchCurve = curve;
		colliderID = ID;
		isReady = true;
	}

	void OnTriggerStay(Collider other)
	{
		if ( !isReady ) 
			return;
		if (other.name.Contains (colliderID) || other.tag.Contains ("Untagged"))
			return;

		if (other.tag.Contains ("CollisionBody")) 
		{
			if (audioTimer <= 0f) {
				
				if (!audio.isPlaying) {
					audio.Play ();
				}
				audioTimer = 0.5f;
				target = other.transform;

				Vector3 whichSide = Vector3.zero;
				Transform targetTrans = other.transform;
				if (target != preTarget)
				{
					targetCol = target.GetComponent<BoxCollider> ();


					Vector3 [] dir = { targetTrans.right*targetTrans.lossyScale.x, targetTrans.up*targetTrans.lossyScale.y, targetTrans.forward*targetTrans.lossyScale.z };

					whichSide = (targetTrans.lossyScale.x >= targetTrans.lossyScale.y) ? dir[0] : dir[1];
					if ( whichSide == dir[0] )
						whichSide = (targetTrans.lossyScale.x >= targetTrans.lossyScale.z) ? dir[0] : dir[2];
					else
						whichSide = (targetTrans.lossyScale.y >= targetTrans.lossyScale.z) ? dir[1] : dir[2];
				}
				else 
				{
					int Sum = 0;
					int depths = 0;
					for (int i = -(int)detectNum.x; i < (int)detectNum.x+1; i++)
					{
						for (int j = -(int)detectNum.y; j < (int)detectNum.y+1; j++)
						{
							for (int k = -(int)detectNum.z; k < (int)detectNum.z+1; k++)
							{
								Vector3 newPos = new Vector3( i* boxCol.size.x/2/detectNum.x, j*boxCol.size.y/2/detectNum.y, k* boxCol.size.z/2/detectNum.z);

								if (targetCol.bounds.Contains (transform.TransformPoint(newPos))) 
								{
									depths++;
								}
								Sum++;
							}
						}
					}
					float depthTestingValue = (float)depths/((float)Sum/2f);
					audio.volume = Mathf.Clamp01( depthTestingValue );

					Vector3 upPoint = targetTrans.localPosition + targetTrans.localScale.y / 4f * transform.up;
					Vector3 downPoint = targetTrans.localPosition - targetTrans.localScale.y / 4f * transform.up;
					Vector3 newTransPoint = targetTrans.InverseTransformPoint ( transform.position );
					float disToUp = upPoint.y - newTransPoint.y;
					float disToDown = newTransPoint.y - downPoint.y;
					float pitchValue = disToDown / (disToUp + disToDown)/3f;
					pitchValue = Mathf.Clamp ( pitchValue, 0.6f, 3f );
					Debug.Log ("pitchValue"+pitchValue);
					audio.pitch = pitchCurve.Evaluate( pitchValue );
				}

				preTarget = other.transform;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if ( !isReady ) 
			return;
		if (other.name.Contains (colliderID) || other.tag.Contains ("Untagged"))
			return;

		if (other.tag.Contains ("CollisionBody")) 
			target = null;
	}

	void Update()
	{
		if ( !isReady ) 
			return;

		if (audioTimer > 0f) 
		{
			audioTimer -= Time.deltaTime;
		}
		else 
		{
			audio.Pause ();
			audio.volume = 0f;
			audio.pitch = 0.4f;
		}
	}

}
