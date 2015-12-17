using UnityEngine;
using System.Collections;

public class CrockMouthTeethControl : MonoBehaviour {

	// use to caculate use up teeth or down teeth
	public GameObject otherMouth;
	public GetArmRotation myoData;
	public float maxTeethLength;
	public float teethGrowSpeed = 0.5f;
	public float nowTeethValue = 0.0f;

	// for keyboard control tooth
	int growDir = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// myo control
		if( myoData != null )
		{
			if( myoData._lastPose == Thalmic.Myo.Pose.WaveOut )
			{
				nowTeethValue -= teethGrowSpeed * Time.deltaTime;
				if( nowTeethValue < 0.0f )
					nowTeethValue = 0.0f;

				SetTeethLength();
			}
			else if( myoData._lastPose == Thalmic.Myo.Pose.WaveIn )
			{
				nowTeethValue += teethGrowSpeed * Time.deltaTime;
				if( nowTeethValue > maxTeethLength )
					nowTeethValue = maxTeethLength;

				SetTeethLength();
			}
		}

		//  keyboard control
		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			if( growDir == 1 )
			{
				if( nowTeethValue == maxTeethLength )
					growDir = -1;
			}
			else
			{
				if( nowTeethValue == 0 )
					growDir = 1;
			}
		}

		if( Input.GetKey( KeyCode.Space ) )
		{
			nowTeethValue += teethGrowSpeed * Time.deltaTime * growDir;
			if( nowTeethValue > 1.0f )
				nowTeethValue = 1.0f;
			else if( nowTeethValue < 0.0f )
				nowTeethValue = 0.0f;

			SetTeethLength ();
		}
	}

	void SetTeethLength ()
	{
		CrocTeethControl[] childTeeth = gameObject.GetComponentsInChildren<CrocTeethControl> ();

		// teeth goo up or bottom
		bool isUp = false;
		if( otherMouth.transform.position.y > transform.position.y )
			isUp = true;


		foreach( CrocTeethControl tooth in childTeeth )
		{

			if( isUp )
				tooth.TeethUp( nowTeethValue );
			else
				tooth.TeethBottom( nowTeethValue );
		}
	}
}
