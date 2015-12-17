using UnityEngine;
using System.Collections;

public class AroundFollower : MonoBehaviour {

	public Transform firstTarget;
	public Transform followTarget;
	public float speed = 5.0f;
	bool activate = false;
	bool arrived = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if( !activate ) return;

		Vector3 dir = Vector3.zero;

		if( !arrived )
		{
			dir = firstTarget.position - transform.position;
			if( dir.magnitude < 1.0f )
				arrived = true;
		}
		else
			dir = followTarget.position - transform.position;

		dir = dir.normalized * speed * Time.deltaTime;
		transform.position += dir;

	}

	void SetFollowPoint ( Transform target ) {

		followTarget = target;

	}

	void Go () {
		activate = true;
	}
}
