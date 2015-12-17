using UnityEngine;
using System.Collections;

public class DancerHeavyTest : MonoBehaviour {

	public GameObject[] handPoints;
	public GameObject[] footPoints;
	public GameObject HeadPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Alpha0))
		{
			handPoints[0].SendMessage( "Grow", 0.0f );
			handPoints[1].SendMessage( "Grow", 0.0f );
		}

		if (Input.GetKeyDown (KeyCode.Alpha1))
		{
			handPoints[0].SendMessage( "Grow", 0.2f );
			handPoints[1].SendMessage( "Grow", 0.2f );
		}

		if (Input.GetKeyDown (KeyCode.Alpha2))
		{
			handPoints[0].SendMessage( "Grow", 0.35f );
			handPoints[1].SendMessage( "Grow", 0.35f );
		}

		if (Input.GetKeyDown (KeyCode.Alpha3))
		{
			handPoints[0].SendMessage( "Grow", 0.5f );
			handPoints[1].SendMessage( "Grow", 0.5f );
		}

		if (Input.GetKeyDown (KeyCode.V))
		{
			footPoints[0].SendMessage( "Grow", 0.0f );
			footPoints[1].SendMessage( "Grow", 0.0f );
		}
		
		if (Input.GetKeyDown (KeyCode.Z))
		{
			footPoints[0].SendMessage( "Grow", 0.3f );
			footPoints[1].SendMessage( "Grow", 0.3f );
		}
		
		if (Input.GetKeyDown (KeyCode.X))
		{
			footPoints[0].SendMessage( "Grow", 0.5f );
			footPoints[1].SendMessage( "Grow", 0.5f );
		}
		
		if (Input.GetKeyDown (KeyCode.C))
		{
			footPoints[0].SendMessage( "Grow", 0.7f );
			footPoints[1].SendMessage( "Grow", 0.7f );
		}

		if (Input.GetKeyDown (KeyCode.H))
		{
			HeadPoint.SendMessage( "Grow", 0.5f );
		}

		if (Input.GetKeyDown (KeyCode.J))
		{
			HeadPoint.SendMessage( "Grow", 0.0f );
		}
	}
}
