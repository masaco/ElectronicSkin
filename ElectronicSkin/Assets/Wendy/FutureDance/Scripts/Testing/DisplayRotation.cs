using UnityEngine;
using System.Collections;

public class DisplayRotation : MonoBehaviour {

	Quaternion original;
	//public GameObject target;
	Quaternion targetOriginal;

	// Use this for initialization
	void Start () {
		Debug.Log ( gameObject.name + ":" + transform.rotation);

		//original = transform.rotation;
		//targetOriginal = target.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		//target.transform.rotation = targetOriginal * ( transform.rotation * Quaternion.Inverse(original) );
	}
}
