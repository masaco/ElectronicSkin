using UnityEngine;
using System.Collections;

public class FollowTargetPos : MonoBehaviour {

	public Transform objToFollow;
	public bool followX = false;
	public bool followY = false;
	public bool followZ = false;

	public Vector3 posBais;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 newPos = transform.position;

		if( followX )
			newPos.x = objToFollow.position.x + posBais.x;

		if( followY )
			newPos.y = objToFollow.position.y + posBais.y;

		if( followZ )
			newPos.z = objToFollow.position.z + posBais.z;

		transform.position = newPos;
	
	}
}
