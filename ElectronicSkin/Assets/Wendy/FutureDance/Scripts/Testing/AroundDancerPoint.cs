using UnityEngine;
using System.Collections;

public class AroundDancerPoint : MonoBehaviour {


	public float angleValue = 0.0f;
	public float distance = 5;
	public float rotateSpeed = 10.0f;
	public float ratio = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		angleValue += rotateSpeed * Time.deltaTime * ( 1 - (ratio - 1) );
		if( angleValue > 360.0f ) angleValue -= 360.0f;

		PositionByAngleValue();
	}

	void PositionByAngleValue () {

		float x = Mathf.Sin( Mathf.Deg2Rad * angleValue ) * distance * ratio;
		float z = Mathf.Cos( Mathf.Deg2Rad * angleValue ) * distance * ratio;

		transform.position = new Vector3( x, 0.0f, z );
	}
}
