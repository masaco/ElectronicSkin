using UnityEngine;
using System.Collections;

public class Unload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating("UnloadResources", 25f, 25f);
	}
	
	void UnloadResources()
	{
		Resources.UnloadUnusedAssets();
	}
}
