using UnityEngine;
using System.Collections;

public class AutoRoter : MonoBehaviour {

	public AnimationCurve curve;
	public float angle = 90.0f;
	public float speed = 0.01f;
	int ratio = 1;

	float t = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		t += speed * Time.deltaTime * ratio;

		if( ratio == 1 )
		{
			if( t > 1.0f )
			{
				t = 1.0f;
				ratio = -1;
			}
		}
		else if( ratio == -1 )
		{
			if( t < 0.0f )
			{
				t = 0.0f;
				ratio = 1;
			}
		}

		Vector3 rot = new Vector3();
		rot.x = curve.Evaluate( t ) * angle;
		transform.localRotation = Quaternion.Euler( rot );
	}
}
