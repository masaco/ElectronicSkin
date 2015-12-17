using UnityEngine;
using System.Collections;

public class BubbleControl : MonoBehaviour {
	
	public int bubblePoints = 15;
	public float superTime = 2.0f;
	LineRenderer line;
	Vector3[] points;

	// control the movement
	Transform followPoint;
	bool isFollowing = false;

	// audio playing
	bool startPlaying = false;

	// Use this for initialization
	void Start () {

		points = new Vector3[ bubblePoints ];
		line = gameObject.GetComponent<LineRenderer> ();
		line.SetVertexCount (bubblePoints+1);
	}
	
	// Update is called once per frame
	void Update () {
	
		for( int i=0; i< bubblePoints; i++ )
		{
			float value = (float)i/(float)bubblePoints * 360.0f * Mathf.Deg2Rad;

			float x = Mathf.Sin( value );
			float y = Mathf.Cos( value );
			float z = transform.position.z;

			points[i] = new Vector3( x,y,z );

			line.SetPosition( i, points[i] );
		}
		line.SetPosition (bubblePoints, points [0]);

		// follow target
		if( isFollowing )
			transform.position = followPoint.position;

		// check is finish playing
		if( startPlaying )
		{
			if( gameObject.GetComponent<AudioSource>().isPlaying == false )
				Destroy ( gameObject );
		}
	}

	void StartFollow ( Transform point )
	{
		isFollowing = true;
		followPoint = point;
	}

	IEnumerator StartMoving ()
	{
		isFollowing = false;
		yield return new WaitForSeconds( superTime );

		gameObject.GetComponent<Collider>().enabled = true;
	}

	void OnCollisionEnter ( Collision collision )
	{
		// prevent mutiple triggers
		if( startPlaying ) return;

		gameObject.GetComponent<AudioSource>().Play ();
		line.enabled = false;
		startPlaying = true;
	}

	void OnTriggerEnter ( Collider collider )
	{
		// prevent mutiple triggers
		if( startPlaying ) return;
		
		gameObject.GetComponent<AudioSource>().Play ();
		line.enabled = false;
		startPlaying = true;
	}
}
