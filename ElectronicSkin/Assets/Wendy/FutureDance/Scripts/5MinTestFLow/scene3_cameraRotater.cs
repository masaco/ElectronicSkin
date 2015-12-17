using UnityEngine;
using System.Collections;

public class scene3_cameraRotater : MonoBehaviour {

	public float rotateTime = 3.0f;
	public AnimationCurve rotateCurve;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator RotateXTo ( float toValue ) {

		int frames = (int)(30.0f * rotateTime);
		float fromX = transform.rotation.eulerAngles.x;
		float diff = toValue - fromX;

		for (int i=0; i< frames; i++)
		{
			float xValue = fromX + rotateCurve.Evaluate( (float)i/(float)frames ) * diff;
			Quaternion newRot = Quaternion.Euler( new Vector3( xValue, 0.0f, 0.0f ) );
			transform.rotation = newRot;

			yield return new WaitForSeconds( 0.03f );
		}
		transform.rotation = Quaternion.Euler( new Vector3( toValue, 0.0f, 0.0f ) );
	}
}
