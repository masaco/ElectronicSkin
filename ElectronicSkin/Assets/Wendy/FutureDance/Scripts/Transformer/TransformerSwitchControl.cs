using UnityEngine;
using System.Collections;

public class TransformerSwitchControl : MonoBehaviour {

	public GetArmRotation leftMyo;
	public GetArmRotation rightMyo;

	public GameObject[] transformTypes;

	int typeIndex = 0;

	public int counter = 0;

	// Use this for initialization
	void Start () {
	
		StartCoroutine ("SwitchType");
	}
	
	// Update is called once per frame
	void Update () {

		// myo control
		if( leftMyo != null && rightMyo != null )
		{
			if( leftMyo._lastPose == Thalmic.Myo.Pose.Fist && rightMyo._lastPose == Thalmic.Myo.Pose.Fist )
				counter++;
			else
				counter = 0;
		}

		
		if( counter > 120 )
		{
			StartCoroutine( "SwitchType" );
			counter = 0;
		}



		// keyboard control
		if( Input.GetKeyDown( KeyCode.S ) )
			StartCoroutine( "SwitchType" );

	}


	IEnumerator SwitchType ()
	{
		// shrink
		for( int i=0; i<=30; i++)
		{
			float size = (30.0f - (float)i) / 30.0f;
			transformTypes[ typeIndex ].transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * size;
			yield return new WaitForSeconds( 0.03f );
		}
		transformTypes [typeIndex].SetActive (false);

		typeIndex = (typeIndex+1)%transformTypes.Length;

		// enlarge
		transformTypes [typeIndex].SetActive (true);
		for( int i=0; i<=30; i++)
		{
			float size = (float)i/ 30.0f;
			transformTypes[ typeIndex ].transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * size;
			yield return new WaitForSeconds( 0.03f );
		}

	}


}
