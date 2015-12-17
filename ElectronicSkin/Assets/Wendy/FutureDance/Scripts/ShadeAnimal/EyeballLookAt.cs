using UnityEngine;
using System.Collections;

public class EyeballLookAt : MonoBehaviour {

	public Transform lookatPoint;
	public float velocityPower = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 dir = lookatPoint.position - transform.position;
		dir.z = 0.0f;
		GetComponent<Rigidbody>().AddForce (dir.normalized * velocityPower);
	
	}
}
