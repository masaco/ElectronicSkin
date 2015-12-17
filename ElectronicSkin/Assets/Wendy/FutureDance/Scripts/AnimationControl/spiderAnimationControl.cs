using UnityEngine;
using System.Collections;

public class spiderAnimationControl : MonoBehaviour {

	public Animation animation;
	public Transform animationPositionFollower;
	public Vector2 animationTimeRange;
	public Vector2 positionRange;

	public float time = 0.0f;

	// Use this for initialization
	void Start () {
		animation ["walk"].speed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		//animation ["walk"].time = time;
		if( !animation.isPlaying )
			animation.Play( "walk" );
		MapPosToAnimationTime ();
	}
	
	void MapPosToAnimationTime ()
	{
		float posDiff = positionRange.y - positionRange.x;
		float posRatio = (animationPositionFollower.position.x - positionRange.x) / posDiff;

		float timeDiff = animationTimeRange.y - animationTimeRange.x;
		float animationTime = timeDiff * posRatio + animationTimeRange.x;

		animation ["walk"].time = animationTime;
	}
}
