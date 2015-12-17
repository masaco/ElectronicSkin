using UnityEngine;
using System.Collections;

public class FollowX : MonoBehaviour {

	public Transform objToFollow;
	public float xBias = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 newPos = transform.position;
		newPos.x = objToFollow.position.x + xBias;

		transform.position = newPos;
	
	}
}
