using UnityEngine;
using System.Collections;
using Pose = Thalmic.Myo.Pose;

public class TransformerControl : MonoBehaviour {

	public GetArmRotation myoData;
	public GameObject[] transformerParts;

	int partIndex = 0;

	bool tapped = false;
	int counter = 60;

	// Use this for initialization
	void Start () {
	
		transformerParts [partIndex].SetActive (true);

	}
	
	// Update is called once per frame
	void Update () {
	
		// waiting for tapped
		if( !tapped )
		{
			if( myoData._lastPose == Pose.DoubleTap )
			{
				tapped = true;
				counter = 60;
			}
		}
		else
		{
			if( counter-- > 0 )
			{
				if( myoData._lastPose == Pose.FingersSpread )
				{
					transformerParts[partIndex].SetActive( false );
					partIndex = (partIndex+1) % transformerParts.Length;
					transformerParts[ partIndex ].SetActive( true );

					counter = 0;
					tapped = false;
				}
			}
			else
				tapped = false;
		}

	}
}
