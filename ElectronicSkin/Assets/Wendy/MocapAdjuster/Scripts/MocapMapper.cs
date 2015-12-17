using UnityEngine;
using System.Collections;



/*
 * 	You can adjust the rotation by simple rotate the gameObject
 */


public class MocapMapper : MonoBehaviour {

	public enum CopyType
	{
		None = 0,
		Rotation = 1,
		LocalRotation = 2
	}

	public Transform monitorSource;
	public Transform monitorTarget;
	// add a empty obj as parent, use to adjust rotation
	private Transform adjustNode;

	public CopyType copyType;

	// add an rotation on
	public Vector3 RotationAddOn;


	// some node need apply position, like Hips
	public bool applyPosition = false;
	public Vector3 positionBais;

	private Quaternion localRot;
	private Quaternion absRot;

	// use for mirror
	private Vector3 originScale;
	private bool firstKey = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		if( monitorSource == null ) return;
		if( monitorTarget == null ) return;

		if( firstKey )
		{
			firstKey = false;
			originScale = monitorTarget.localScale;

			// create parent for adjust rotation
			GameObject node = new GameObject( "parentNode" );
			node.transform.position = monitorTarget.position;
			adjustNode = node.transform;

			adjustNode.parent = monitorTarget.parent;
			monitorTarget.parent = adjustNode;
		}


		if( copyType == CopyType.LocalRotation )
		{
			/* old ad
			Quaternion newRot = monitorSource.localRotation;
			newRot = transform.localRotation * monitorSource.localRotation;
			//monitorTarget.localRotation = monitorSource.localRotation * transform.localRotation;
			monitorTarget.localRotation = newRot;
			*/

			Quaternion newRot = monitorSource.transform.localRotation * Quaternion.Euler( RotationAddOn );
			monitorTarget.localRotation = newRot;
			adjustNode.localRotation = transform.localRotation;
			adjustNode.localScale = transform.localScale;


			/*
			// mirror function
			Vector3 newScale = originScale;
			newScale.x *= transform.localScale.x;
			newScale.y *= transform.localScale.y;
			newScale.z *= transform.localScale.z;

			monitorTarget.localScale = newScale;
			*/
		}
		else if( copyType == CopyType.Rotation )
		{
			Quaternion newRot = transform.rotation * monitorSource.transform.rotation * Quaternion.Euler( RotationAddOn );
			monitorTarget.rotation = newRot;
			adjustNode.localScale = transform.localScale;
		}

		if( applyPosition )
			monitorTarget.localPosition = monitorSource.localPosition + positionBais;
	}
}
