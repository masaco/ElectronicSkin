using UnityEngine;
using System.Collections;

public class RaycastMoveEyes : MonoBehaviour {

	public GameObject objToMove;
	public Transform lookAtPoint;
	public float distance = 50.0f;
	public float adjustDistance = 0.2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		CastRay ();

	}

	void CastRay ()
	{
		Vector3 dir = ( lookAtPoint.position - gameObject.transform.position);
		dir.z = 0.0f;

		Ray ray = new Ray (transform.position, dir);
		RaycastHit[] hitInfo = Physics.RaycastAll (ray);

		int shortestIndex = -1;
		float shortestDistance = 10000.0f;
		for( int i=0; i< hitInfo.Length; i++ )
		{
			if( hitInfo[i].collider.tag == "ShadeBody" )
			{
				if( hitInfo[i].distance <= shortestDistance )
				{
					shortestDistance = hitInfo[i].distance;
					shortestIndex = i;
				}
			}
		}

		if( shortestIndex != -1 )
			objToMove.transform.position = hitInfo [shortestIndex].point + hitInfo[ shortestIndex ].normal * adjustDistance;
	}
}
