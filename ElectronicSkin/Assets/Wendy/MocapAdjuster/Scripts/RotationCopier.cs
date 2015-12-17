using UnityEngine;
using System.Collections;
using CopyType = MocapMapper.CopyType;


public class RotationCopier : MonoBehaviour {

	public Transform source;
	public Transform target;
	private Transform adjustNode;

	public CopyType copyType;

	private bool firstKey = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if( source == null ) return;
		if( target == null ) return;

		if( firstKey )
		{
			firstKey = false;
			GameObject obj = new GameObject( "copierNode" );
			obj.transform.position = target.position;

			adjustNode = obj.transform;

			adjustNode.parent = target.parent;
			target.parent = adjustNode;
		}
		ApplyNewRotation ();
	}


	void ApplyNewRotation ()
	{
		if( copyType == CopyType.None )
			return;
		else if( copyType == CopyType.LocalRotation )
		{
			target.localRotation = source.localRotation;

			adjustNode.transform.rotation = transform.rotation;
		}
		else if( copyType == CopyType.Rotation )
		{
			target.rotation = source.rotation * transform.rotation;
		}

		//adjustNode.transform.rotation = transform.rotation;
		adjustNode.localScale = transform.localScale;
	}
}
