using UnityEngine;
using System.Collections;

public class ScaleControl : MonoBehaviour {

	public float scaleValue = 1.0f;
	private float lastScaleValue = 0.0f;
	// Use this for initialization
	void Start () {
	
		transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f) * scaleValue;
		lastScaleValue = scaleValue;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (scaleValue != lastScaleValue)
		{
			transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f) * scaleValue;
			lastScaleValue = scaleValue;
		}

	}
}
