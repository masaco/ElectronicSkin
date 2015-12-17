using UnityEngine;
using System.Collections;

[RequireComponent (typeof( AudioSource ))]
public class MicrophoneRecorder : MonoBehaviour {
	

	AudioSource player;
	public int maxLengthSec = 50;
	public int frequency = 44100;
	public int recordDeviceId = 0;
	// counting the real sound time
	float timeCounter = 0.0f;

	// name
	string newClipName = "undefined";

	// Use this for initialization
	void Start () {

		for( int i=0; i< Microphone.devices.Length; i++ )
			Debug.Log( Microphone.devices[i] );

		player = gameObject.GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if( Microphone.IsRecording( Microphone.devices[ recordDeviceId ] ) )
			timeCounter += Time.deltaTime;

	}

	public void StartRecording ( string clipName = "newClip" )
	{
		newClipName = clipName;
		timeCounter = 0.0f;
		player.clip = Microphone.Start( Microphone.devices[recordDeviceId], false, maxLengthSec, frequency );
	}

	// cut the blank parts
	public void EndRecording ()
	{
		// if not started yet
		if( !Microphone.IsRecording( Microphone.devices[recordDeviceId] ) ) return;

		Microphone.End( Microphone.devices[recordDeviceId] );
		int totalSampleLength = (int)((float)frequency * timeCounter);

		AudioClip newClip = AudioClip.Create( newClipName, totalSampleLength, 1, frequency, false, false );

		// get clip data
		float[] clipSamples = new float[ player.clip.samples * player.clip.channels ];
		player.clip.GetData( clipSamples, 0 );

		// only put need length of data
		float[] neededSamples = new float[ totalSampleLength ];
		for( int i=0; i< totalSampleLength; i++ )
		{
			neededSamples[i] = clipSamples[i];
		}

		newClip.SetData( neededSamples, 0 );
		player.clip = newClip;
	}

	public AudioClip GetClip ()
	{
		return player.clip;
	}

}
