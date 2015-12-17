using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]

public class SpiderLeg : MonoBehaviour {

	LineRenderer line;
	public float legLength = 1.0f;
	public GameObject leg1;
	public GameObject leg2;

	// Use this for initialization
	void Start () {

		line = gameObject.GetComponent<LineRenderer> ();
		line.SetVertexCount (3);
	
	}
	
	// Update is called once per frame
	void Update () {
	
		float distance = Vector3.Distance (leg1.transform.position, leg2.transform.position);
		float ratio = distance / (legLength * 2);

		// angle from distance
		float addAngle = (ratio - 1) * 90.0f;
		float twoCircleAngle = Vector3.Angle(leg1.transform.up, leg2.transform.position - leg1.transform.position);

		float sinValue = Mathf.Sin ( (addAngle + twoCircleAngle) * Mathf.Deg2Rad );
		float cosValue = Mathf.Cos ( (addAngle + twoCircleAngle) * Mathf.Deg2Rad );

		Vector3 midPoint = Vector3.zero;
		midPoint.x = leg1.transform.position.x + legLength * sinValue;
		midPoint.y = leg1.transform.position.y + legLength * cosValue;
		midPoint.z = leg1.transform.position.z;

		if( leg2.transform.position.x < leg1.transform.position.x )
		{
			midPoint.x *= -1.0f;
		}

		line.SetPosition (0, leg1.transform.position);
		line.SetPosition (1, midPoint);
		line.SetPosition (2, leg2.transform.position);

	}
}
