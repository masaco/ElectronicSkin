using UnityEngine;
using System.Collections;

public class ChangeScenes : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Contains ("CollisionBody")) Application.LoadLevelAsync("main");

	}
}
