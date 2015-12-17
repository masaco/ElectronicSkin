using UnityEngine;
using System.Collections;

public class FollowXY : MonoBehaviour {

	public Transform objToFollow;
	public Vector3 positionBais;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 pos = transform.position;
		pos.x = objToFollow.position.x;
		pos.y = objToFollow.position.y;
		pos += positionBais;
		transform.position = pos;
	}
}
