using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MicrophoneRecorder))]
public class BubbleShooter : MonoBehaviour {

	public GetArmRotation myoData;

	MicrophoneRecorder recorder;
	public GameObject bubbleObj;
	int bubbleNameIndex = 0;

	// for blowing bubble
	bool isBlowing = false;
	GameObject nowBubble;
	float nowSize = 0.0f;
	
	// Use this for initialization
	void Start () {
	
		recorder = gameObject.GetComponent<MicrophoneRecorder> ();
	}
	
	// Update is called once per frame
	void Update () {

		if( myoData._lastPose == Thalmic.Myo.Pose.Fist && !isBlowing )
		{
			StartBubble();
		}
		else if( myoData._lastPose == Thalmic.Myo.Pose.WaveOut && isBlowing )
		{
			EndBubble ();
		}
		else if( isBlowing )
		{
			nowSize += 0.001f;
			nowBubble.transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * nowSize;
		}
	
	}

	void StartBubble ()
	{
		recorder.StartRecording ("buuble_" + bubbleNameIndex);
		nowBubble = (GameObject)Instantiate (bubbleObj, transform.position, Quaternion.identity);
		nowBubble.SendMessage ("StartFollow", transform);
		nowSize = 0.0f;
		isBlowing = true;
	}

	void EndBubble ()
	{
		recorder.EndRecording ();
		nowBubble.GetComponent<AudioSource>().clip = recorder.GetClip ();
		nowBubble.SendMessage ("StartMoving");
		isBlowing = false;
	}
}
