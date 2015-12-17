using UnityEngine;
using System.Collections;

public class MothWings : MonoBehaviour {

	// myo data
	public GetArmRotation myoData;

	// follow target rotation
	public Transform armNode;

	public GameObject wingUp;
	public GameObject wingDown;
	public ParticleSystem redParticle;
	public ParticleSystem greenParticle;

	public int bottomWingDelayFrames = 10;

	ArrayList rotationDatas;

	Quaternion lastRotation;

	// Use this for initialization
	void Start () {
		rotationDatas = new ArrayList ();

		if( myoData != null )
			lastRotation = myoData.gameObject.transform.rotation;
		else if( armNode != null )
			lastRotation = armNode.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	
		// myo control
		if( myoData != null )
		{
			// make wings rotate by myo
			rotationDatas.Add (myoData.gameObject.transform.rotation);
			wingUp.transform.rotation = myoData.transform.rotation;
			if( rotationDatas.Count > bottomWingDelayFrames )
			{
				wingDown.transform.rotation = (Quaternion)rotationDatas[0];
				rotationDatas.RemoveAt(0);
			}

			// make particle emmit

			// calculate move amount
			float moveAmount = Quaternion.Angle ( myoData.gameObject.transform.rotation , lastRotation);
			int particleEmitAmount = (int)(moveAmount * 0.3f);

			if( myoData._lastPose == Thalmic.Myo.Pose.WaveOut )
				greenParticle.Emit( particleEmitAmount );

			if( myoData._lastPose == Thalmic.Myo.Pose.WaveIn )
				redParticle.Emit( particleEmitAmount );

			lastRotation = myoData.gameObject.transform.rotation;
		}
		// keyboard control
		else
		{
			rotationDatas.Add( armNode.rotation );
			wingUp.transform.rotation = armNode.rotation;
			if( rotationDatas.Count > bottomWingDelayFrames )
			{
				wingDown.transform.rotation = (Quaternion)rotationDatas[0];
				rotationDatas.RemoveAt(0);
			}

			// calculate move amount
			float moveAmount = Quaternion.Angle ( armNode.rotation , lastRotation);
			int particleEmitAmount = (int)(moveAmount * 0.3f);
			
			if( Input.GetKey( KeyCode.DownArrow ) )
				greenParticle.Emit( particleEmitAmount );
			
			if( Input.GetKeyDown( KeyCode.UpArrow ) )
				redParticle.Emit( particleEmitAmount );
			
			lastRotation = armNode.rotation;
		}
	}
}
