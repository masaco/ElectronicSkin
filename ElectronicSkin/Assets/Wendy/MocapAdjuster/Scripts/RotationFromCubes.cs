using UnityEngine;
using System.Collections;

public class RotationFromCubes : MonoBehaviour {

	public GameObject obj1;
	public GameObject obj2;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if( obj1 == null ) return;

		CalculateRotation ();
	}

	void CalculateRotation ()
	{
		if( obj2 == null )
			transform.rotation = obj1.transform.rotation;
		else
		{
		//Quaternion newRot = Quaternion.LookRotation( obj2.transform.position - obj1.transform.position, transform.rotation.eulerAngles );
		Vector3 dir = ( obj2.transform.position - obj1.transform.position );
		Quaternion newRot = Quaternion.FromToRotation( new Vector3( 0.0f, 1.0f, 0.0f ), dir );

		transform.rotation = newRot;
		//if( mappingTarget != null )
		//	mappingTarget.rotation = newRot;
		}
	}
}
