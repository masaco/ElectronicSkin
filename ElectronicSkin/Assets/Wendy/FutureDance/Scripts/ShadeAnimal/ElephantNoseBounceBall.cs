using UnityEngine;
using System.Collections;

public class ElephantNoseBounceBall : MonoBehaviour {

	public float movePower;
	public float powerMutiplier = 100.0f;
	public Vector3 lastPos;

	// Use this for initialization
	void Start () {
	
		lastPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		movePower = Vector3.Distance(transform.position, lastPos);
		lastPos = transform.position;
	}

	void OnCollisionEnter ( Collision collision )
	{
		Debug.Log ("HIT!");

		if (collision.gameObject.tag == "Ball")
		{
			Debug.Log( "GetBall" );
			GameObject collider = collision.gameObject;
			collider.GetComponent<Rigidbody>().AddForce( (collider.transform.position - transform.position)*movePower*powerMutiplier );
		}
	}
	
}
