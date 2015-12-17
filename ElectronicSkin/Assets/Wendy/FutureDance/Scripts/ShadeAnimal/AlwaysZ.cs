using UnityEngine;
using System.Collections;

public class AlwaysZ : MonoBehaviour {

	public Vector3 alwaysValue;
	public bool isX = false;
	public bool isY = false;
	public bool isZ = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 newPos = transform.position;

		if (isX)
			newPos.x = alwaysValue.x;

		if (isY)
			newPos.y = alwaysValue.y;

		if (isZ)
			newPos.z = alwaysValue.z;

		transform.position = newPos;
	}
}
