using UnityEngine;
using System.Collections;

public class PosControlEyebrow : MonoBehaviour {

	public CrocodileEyebrow eyebrow;
	public Transform posTarget;
	public Vector2 posRange;
	float diff;

	// Use this for initialization
	void Start () {
	
		diff = posRange.y - posRange.x;
	}
	
	// Update is called once per frame
	void Update () {
	
		float heightValue = posTarget.position.y - posRange.x;

		if (heightValue < 0.0f)
			heightValue = 0.0f;
		else if( heightValue > diff )
			heightValue = diff;

		float ratio = heightValue / diff;

		eyebrow.statusValue = ratio;
	}
}
