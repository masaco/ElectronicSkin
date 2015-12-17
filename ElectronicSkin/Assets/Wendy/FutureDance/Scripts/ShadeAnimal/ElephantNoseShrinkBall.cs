using UnityEngine;
using System.Collections;

public class ElephantNoseShrinkBall : MonoBehaviour {

	public GameObject ballInTheScene;
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
		
		yield return new WaitForSeconds( 1.0f );
		
	}
	
	// Update is called once per frame
	void Update () {

		// water shrinking
		if( status )
		{
			Vector3 forceDir = ( shrinkPoint.position - ballInTheScene.transform.position );
			ballInTheScene.GetComponent<Rigidbody>().useGravity = false;
			ballInTheScene.GetComponent<Rigidbody>().AddForce( forceDir.normalized * shrinkPower );

			fireCounter++;
			if( fireCounter > 5 )
				status = false;

			blowPower++;
			wasShrinking = true;
		}
		else
		{
			ballInTheScene.GetComponent<Rigidbody>().useGravity = true;
			fireCounter = 0;

			if( wasShrinking )
			{
				Vector3 dir = ballInTheScene.transform.position - shrinkPoint.position;
				ballInTheScene.GetComponent<Rigidbody>().AddForce( dir.normalized * blowPower * blowMutiplier );
				wasShrinking = false;
				blowPower = 0;
			}
		}
	}
	
	void Fire ()
	{
		status = true;
		fireCounter = 0;
	}
}
