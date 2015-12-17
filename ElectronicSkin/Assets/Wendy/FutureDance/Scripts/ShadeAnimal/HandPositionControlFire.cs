using UnityEngine;
using System.Collections;

public class HandPositionControlFire : MonoBehaviour {

	public Transform compareElement;
	public GameObject fireEventCatcher;

	bool fireStatus = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.y >= compareElement.position.y)
			fireStatus = true;
		else
			fireStatus = false;

		if (fireStatus)
			fireEventCatcher.SendMessage ("Fire");
	}
}
