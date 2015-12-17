using UnityEngine;
using System.Collections;

public class SpiderFeetControl : MonoBehaviour {

	private ArrayList rotationDatas = new ArrayList();
	public GameObject[] spiderFoots;
	public int framesDelay = 15;
	int totalFrames = 0;

	// Use this for initialization
	void Start () {
	
		totalFrames = framesDelay * spiderFoots.Length;

	}
	
	// Update is called once per frame
	void Update () {
	
		rotationDatas.Insert( 0, spiderFoots [0].transform.rotation);

		if( rotationDatas.Count > totalFrames )
		{
			rotationDatas.RemoveAt( rotationDatas.Count-1 );
		}

		ApplyFootsRotations ();
	}

	void ApplyFootsRotations ()
	{
		for( int i=1; i< spiderFoots.Length; i++ )
		{
			if( rotationDatas.Count > i*framesDelay )
				spiderFoots[i].transform.rotation = (Quaternion)rotationDatas[i*framesDelay];
			else
				break;
		}
	}
}
