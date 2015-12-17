using UnityEngine;
using System.Collections;

public class NoseEndGetWater : MonoBehaviour {


	int counter = 5;
	public bool canGetWater = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if( counter <= 0 )
			canGetWater = false;
		else
			counter--;
	}

	void OnTriggerStay ( Collider collider )
	{
		if( collider.tag == "WaterArea" )
		{
			canGetWater = true;
			counter = 5;
		}
	}

	public bool GetWater ()
	{
		return canGetWater;
	}
}
