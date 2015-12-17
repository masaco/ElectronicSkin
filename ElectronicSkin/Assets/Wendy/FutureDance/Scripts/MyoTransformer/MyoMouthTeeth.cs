using UnityEngine;
using System.Collections;
using Pose = Thalmic.Myo.Pose;

[RequireComponent (typeof(MonsterMouthControl))]

public class MyoMouthTeeth : MonoBehaviour {

	public GetArmRotation myoData;
	private MonsterMouthControl mouth;
	public float biteSpeed = 1.0f;

	public Pose gesture = Pose.Unknown;
	public HandType hand;

	// use to edit wing size
	public Vector2 sizeRange;
	float originalSize = 0.0f;
	float lastRot = -100.0f;
	bool justSwitched = false;


	// Use this for initialization
	void Start () {
		mouth = gameObject.GetComponent<MonsterMouthControl> ();
	}
	
	// Update is called once per frame
	void Update () {


		// controlling mouth open
		float newValue = -1.0f;

		if( myoData._lastPose == Thalmic.Myo.Pose.WaveIn )
		{
			newValue = mouth.openValue - biteSpeed * Time.deltaTime;
		}
		else if( myoData._lastPose == Thalmic.Myo.Pose.WaveOut )
		{
			newValue = mouth.openValue + biteSpeed * Time.deltaTime;
		}

		if( newValue > -0.5f )
		{
			if( newValue < 0.0f )
				newValue = 0.0f;
			else if ( newValue > 1.0f )
				newValue = 1.0f;

			mouth.openValue = newValue;
		}


		// controlling size scaling
		gesture = myoData._lastPose;
		if (gesture == Pose.Fist)
		{
			if( justSwitched )
			{
				justSwitched = false;
				lastRot = myoData.comparerAngle;
				originalSize = transform.localScale.x;
			}
			
			float newSize = 0.0f;
			if( hand == HandType.RightHand )
				newSize = (originalSize + (myoData.comparerAngle - lastRot )*0.01f );
			else if( hand == HandType.LeftHand )
				newSize = (originalSize - (myoData.comparerAngle - lastRot )*0.01f );
			
			newSize = Mathf.Max( sizeRange.x , Mathf.Min( sizeRange.y, newSize ) );
			transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * newSize;
		}
		else
			justSwitched = true;
	}
}
