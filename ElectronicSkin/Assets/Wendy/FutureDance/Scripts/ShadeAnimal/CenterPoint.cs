using UnityEngine;
using System.Collections;

public class CenterPoint : MonoBehaviour {

	public Transform point1;
	public Transform point2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		gameObject.transform.position = (point1.position + point2.position) / 2.0f;
	}
}
