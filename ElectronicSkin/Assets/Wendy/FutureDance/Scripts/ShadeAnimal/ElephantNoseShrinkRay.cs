using UnityEngine;
using System.Collections;

public class ElephantNoseShrinkRay : MonoBehaviour {

	public GetArmRotation myoData;
	GameObject target;

	public Transform rayCastStart;
	public Transform rayCastEnd;

	public Transform shrinkPoint;
	public float shrinkPower = 300.0f;
	public float blowPower = 100.0f;
	public float blowMutiplier = 10.0f;

	bool status = false;
	// prevent keep shooting
	int fireCounter = 0;

	bool wasShrinking = false;

	// Use this for initialization
	IEnumerator Start () {

		target = null;
		yield return new WaitForSeconds( 1.0f );
		
	}
	
	// Update is called once per frame
	void Update () {

		// get arm gesture
		if( myoData._lastPose == Thalmic.Myo.Pose.WaveIn )
			status = true;
		else if( myoData._lastPose == Thalmic.Myo.Pose.WaveOut )
			status = false;

		// water shrinking
		if( status )
		{
			// havent get target, get target
			if( !wasShrinking )
			{
				Ray ray = new Ray( rayCastStart.transform.position, rayCastStart.transform.position - rayCastEnd.transform.position );
				RaycastHit[] info;
				info = Physics.RaycastAll( ray, 20.0f);

				int nearestId = -1;
				float nearestDistance = 100.0f;

				for( int i=0; i< info.Length; i++ )
				{
					if( info[i].collider.tag == "Shrinkable" )
					{
						if( info[i].distance < nearestDistance )
						{
							nearestId = i;
							nearestDistance = info[i].distance;
						}
					}
				}

				if( nearestId != -1 )
					target = info[nearestId].collider.gameObject;

			}

			// if got no target, return
			if( !target )
				return;

			Vector3 forceDir = ( shrinkPoint.position - target.transform.position );
			target.GetComponent<Rigidbody>().useGravity = false;
			target.GetComponent<Rigidbody>().AddForce( forceDir.normalized * shrinkPower );

			fireCounter++;
			if( fireCounter > 5 )
				status = false;

			blowPower++;
			wasShrinking = true;
		}
		else
		{
			if( wasShrinking )
			{
				Vector3 dir = target.transform.position - shrinkPoint.position;
				target.GetComponent<Rigidbody>().AddForce( dir.normalized * blowPower * blowMutiplier );
				wasShrinking = false;
				target.GetComponent<Rigidbody>().useGravity = true;
				fireCounter = 0;
				blowPower = 0;
				target = null;
			}
		}
	}
	
	void Fire ()
	{
		status = true;
		fireCounter = 0;
	}
}
