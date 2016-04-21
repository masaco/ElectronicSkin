using UnityEngine;
using System.Collections;

public class Unload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating("UnloadResources", 15f, 15f);
	}
	
	void UnloadResources()
	{
		Resources.UnloadUnusedAssets();
	}
}
