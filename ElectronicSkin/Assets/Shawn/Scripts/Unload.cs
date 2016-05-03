using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Unload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating("UnloadResources", 35f, 35f);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Alpha0))
			SceneManager.LoadScene (0);
	}

	void UnloadResources()
	{
		Resources.UnloadUnusedAssets();
	}
}
