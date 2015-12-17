using UnityEngine;
using System.Collections;

public class HorrorCircle : MonoBehaviour {

	public float rotSpeed = 1.0f;
	float zValue = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		zValue += rotSpeed;

		if( zValue > 360.0f )
			zValue -= 360.0f;

		Vector3 newRot = new Vector3 (0.0f, 0.0f, zValue);
		transform.rotation = Quaternion.Euler (newRot);
	}
}
