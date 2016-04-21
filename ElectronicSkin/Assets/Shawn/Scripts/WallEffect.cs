using UnityEngine;
using System.Collections;

public class WallEffect : MonoBehaviour {

	public AnimationCurve LerpAnimCurve;
	private MeshRenderer meshR;
	void Start () {
		meshR = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		meshR.material.SetFloat ( "_EffectLerp", Mathf.Lerp( 0.42f, 0.85f, LerpAnimCurve.Evaluate(Mathf.PingPong( Time.time/3f, 1 ) )) );
	}
}
