using UnityEngine;
using System.Collections;

public class testDir : MonoBehaviour {

	public Transform posToLook;
	public Vector3 dir;
	public GameObject createObj;
	public float moveSpeed = 1.0f;
	public int delayFrames = 5;
	int delaycounter = 0;
	Vector3 createPoint;

	// Use this for initialization
	void Start () {
	
		dir = posToLook.position - transform.position;
		dir = dir.normalized;
		createPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		if( delaycounter++ == delayFrames )
		{
			delaycounter = 0;
			Instantiate (createObj, createPoint, Quaternion.identity);
			createPoint += dir * moveSpeed * Time.deltaTime;
		}
	}
}
