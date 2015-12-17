using UnityEngine;
using System.Collections;

public class FollowTargetRot : MonoBehaviour {

	public Transform objToFollow;
	public bool followX = false;
	public bool followY = false;
	public bool followZ = false;

	public Vector3 rotBais;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 newRot = transform.rotation.eulerAngles;

		if( followX )
			newRot.x = objToFollow.rotation.eulerAngles.x + rotBais.x;

		if( followY )
			newRot.y = objToFollow.rotation.eulerAngles.y + rotBais.y;

		if( followZ )
			newRot.z = objToFollow.rotation.eulerAngles.z + rotBais.z;

		transform.rotation = Quaternion.Euler( newRot );
	
	}
}
