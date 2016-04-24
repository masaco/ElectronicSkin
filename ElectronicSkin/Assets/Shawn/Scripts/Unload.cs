using UnityEngine;
using System.Collections;

public class Unload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating("UnloadResources", 35f, 35f);
	}
	
	void UnloadResources()
	{
		Resources.UnloadUnusedAssets();
	}
}
