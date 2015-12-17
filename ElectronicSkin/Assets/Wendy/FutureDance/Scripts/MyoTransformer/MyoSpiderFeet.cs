using UnityEngine;
using System.Collections;
using Pose = Thalmic.Myo.Pose;
//using HandType = MyoWings.HandType;

public class MyoSpiderFeet : MonoBehaviour {
	
	public GetArmRotation myoData;
	public HandType hand;
	
	public Pose gesture = Pose.Unknown;
	
	// use to edit wing size
	public Vector2 sizeRange;
	float originalSize = 0.0f;
	float lastRot = -100.0f;
	bool justSwitched = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 rot = transform.rotation.eulerAngles;
		rot.z = myoData.comparerAngle;
		transform.rotation = Quaternion.Euler(rot);
		
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


