using UnityEngine;
using System.Collections;

public class EyeballControl : MonoBehaviour {

	public Transform positionToFollow;
	public float gravityPower = 10.0f;
	public float resetDistance;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if( Vector3.Distance( positionToFollow.position, transform.position ) > resetDistance )
		{
			transform.position = positionToFollow.position;
		}

		Vector3 goDir = positionToFollow.position - transform.position;
		transform.GetComponent<Rigidbody>().AddForce (goDir.normalized * gravityPower);
	}
}
