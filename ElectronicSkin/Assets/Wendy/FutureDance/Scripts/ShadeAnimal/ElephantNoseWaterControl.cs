using UnityEngine;
using System.Collections;

public class ElephantNoseWaterControl : MonoBehaviour {

	// use to shoot water
	public GameObject waterDropObj;
	public GameObject elephantWaterNodeObj;

	public NoseEndGetWater WaterDrinker;
	public Transform firePoint;
	public float firePower = 300.0f;
	public float energy = 100.0f;

	public bool autoDrink = false;


	ElephantNose nose;
	bool status = false;
	// prevent keep shooting
	int fireCounter = 0;

	int drinkCounter = 0;

	// Use this for initialization
	IEnumerator Start () {
	
		yield return new WaitForSeconds( 1.0f );

		nose = gameObject.GetComponent<ElephantNose>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if( Input.GetKey( KeyCode.D ) || autoDrink )
		{
			if( WaterDrinker.GetWater() )
			{
				energy++;

				drinkCounter++;
				if( drinkCounter > 30 )
				{
					drinkCounter = 0;
					GameObject waterNode = (GameObject)Instantiate( elephantWaterNodeObj, firePoint.position, Quaternion.identity );
					ElephantNoseWaterNode nodeScript = waterNode.GetComponent<ElephantNoseWaterNode>();
					nodeScript.nodesToFollow = nose.nodes;
				}
			}

		}



		// water shooting
		if( status )
		{
			if( energy > 0 )
			{
				Vector3 forceDir = firePoint.up + new Vector3( Random.Range( -0.5f, 0.5f ), 0.0f, 0.0f );
				forceDir.z = 0.0f;
				Vector3 firePos = firePoint.position;
				firePos.z = 0.0f;

				GameObject drop = (GameObject)Instantiate( waterDropObj, firePos, Quaternion.identity );
				drop.GetComponent<Rigidbody>().AddForce( forceDir.normalized * firePower );

				energy--;
			}

			fireCounter++;
			if( fireCounter > 5 )
				status = false;
		}
		else
		{
			fireCounter = 0;
		}
	}

	void Fire ()
	{
		status = true;
		fireCounter = 0;
	}
}
