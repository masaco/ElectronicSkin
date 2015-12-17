using UnityEngine;
using System.Collections;

public class RotateCopy : MonoBehaviour {

	public Transform from;
	public Transform to;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		CopyRotation();
	}

	void CopyRotation ()
	{
		Quaternion rot = from.localRotation;
		rot = rot * transform.localRotation;

		to.localRotation = rot;
	}
}
