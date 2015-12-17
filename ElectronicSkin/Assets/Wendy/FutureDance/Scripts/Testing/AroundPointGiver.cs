using UnityEngine;
using System.Collections;

public class AroundPointGiver : MonoBehaviour {

	public Transform[] followPoints;
	int nowIndex = 0;
	bool available = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter ( Collider collider ) {
		//if( !available ) return;

		Debug.Log( "GIVE ! " + nowIndex );
		collider.SendMessage( "SetFollowPoint", followPoints[ nowIndex ] );

		nowIndex++;
		if( nowIndex >= followPoints.Length )
			available = false;
	}
}
