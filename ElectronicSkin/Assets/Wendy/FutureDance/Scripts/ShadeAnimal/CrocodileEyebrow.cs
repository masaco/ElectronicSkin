using UnityEngine;
using System.Collections;

public class CrocodileEyebrow : MonoBehaviour {

	public Vector2 posValueRange;
	public Vector2 rotValueRange;
	public float statusValue = 0.0f;
	float posDiff;
	float rotDiff;

	// Use this for initialization
	void Start () {
	
		posDiff = posValueRange.y - posValueRange.x;
		rotDiff = rotValueRange.y - rotValueRange.x;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 newPos = transform.localPosition;
		newPos.x = posValueRange.x + posDiff * statusValue;
		transform.localPosition = newPos;
		transform.localRotation = Quaternion.Euler( new Vector3( 0.0f, 0.0f, rotValueRange.x + rotDiff * statusValue ) );
	
	}
}
