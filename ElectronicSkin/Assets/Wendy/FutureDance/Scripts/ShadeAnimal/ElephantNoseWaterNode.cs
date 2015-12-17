using UnityEngine;
using System.Collections;

public class ElephantNoseWaterNode : MonoBehaviour {
	
	public GameObject[] nodesToFollow;
	public int framesBetweenTwoNodes = 10;
	Transform from;
	Transform to;
	int nowFollowIndex = 0;
	int frameCounter = 0;

	bool followMode = true;

	bool shrinkMode = false;
	float sizeRatio = 1.0f;
	float originalSize;

	// Use this for initialization
	void Start () {
	
		from = nodesToFollow[ nodesToFollow.Length-1 ].transform;
		to = nodesToFollow[ nodesToFollow.Length-2 ].transform;

		originalSize = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
	
		// this obj will move from first point through last point
		// each point takes framesBetweenTwoNodes frames
		if( followMode )
		{
			float ratio = (float)frameCounter / (float)framesBetweenTwoNodes;
			transform.position = Vector3.Lerp( from.position, to.position, ratio );

			frameCounter++;

			if( frameCounter >= framesBetweenTwoNodes )
			{
				frameCounter = 0;
				NextNode();
			}
		}

		// it get to the last point, shrink itself
		if( shrinkMode )
		{
			transform.position = nodesToFollow[0].transform.position;
			transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * ( originalSize ) * sizeRatio;

			sizeRatio -= 0.03f;

			if( sizeRatio <= 0.0f )
				Destroy( gameObject );
		}
	}

	void NextNode ()
	{
		nowFollowIndex++;

		// follow to the end
		if( nowFollowIndex == nodesToFollow.Length-1 )
		{
			followMode = false;
			shrinkMode = true;
			return;
		}

		from = nodesToFollow[ nodesToFollow.Length - nowFollowIndex -1].transform;
		to = nodesToFollow[ nodesToFollow.Length - nowFollowIndex -2].transform;


	}
}
