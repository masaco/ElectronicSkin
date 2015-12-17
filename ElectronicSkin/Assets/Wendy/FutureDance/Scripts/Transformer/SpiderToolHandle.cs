using UnityEngine;
using System.Collections;

public class SpiderToolHandle : MonoBehaviour {

	public GetArmRotation myoData;
	// if no myo
	public Transform armNode;

	public GameObject[] tools;

	int toolIndex = 0;
	int switchDelayCounter = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if( myoData != null )
		{
			// rotate by myo
			transform.rotation = Quaternion.Euler( new Vector3( 0.0f, 0.0f, myoData.comparerAngle ) );

			if( switchDelayCounter > 0 )
				switchDelayCounter--;
			else
			{
				if( myoData._lastPose == Thalmic.Myo.Pose.WaveOut )
				{
					tools[toolIndex].SetActive(false);

					toolIndex = (toolIndex+1)%tools.Length;

					tools[toolIndex].SetActive(true);

					switchDelayCounter = 60;
				}
			}
		}
		else if( armNode != null )
		{
			transform.rotation = Quaternion.Euler( new Vector3( 0.0f, 0.0f, armNode.rotation.z ) );

			if( Input.GetKeyDown( KeyCode.Space ) )
			{
				tools[toolIndex].SetActive(false);
				
				toolIndex = (toolIndex+1)%tools.Length;
				
				tools[toolIndex].SetActive(true);
			}
		}
	}
}
