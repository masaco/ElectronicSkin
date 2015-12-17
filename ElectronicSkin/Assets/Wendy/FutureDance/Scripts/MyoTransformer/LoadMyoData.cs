using UnityEngine;
using System.Collections;

public class LoadMyoData : MonoBehaviour {

	public ThalmicMyo myoSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.rotation = myoSource.gameObject.transform.rotation;
	}
}
