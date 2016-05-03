using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {


	void Start () {
	
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			PlayerPrefs.SetInt ( "PlayerID", 1 );
			SceneManager.LoadScene (1);
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			PlayerPrefs.SetInt ( "PlayerID", 2 );
			SceneManager.LoadScene (1);
		}

		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			SceneManager.LoadScene (2);
		}
	}

	void OnGUI () {
		GUI.skin.button.fontSize = Screen.width / 40;
		GUILayout.BeginVertical ();
		GUILayout.Space ( Screen.height*3/7);
		GUILayout.BeginHorizontal ();
		GUILayout.Space ( Screen.width/5);
		if (GUILayout.Button ("Player 1",GUILayout.Width(Screen.width/5), GUILayout.Height(Screen.height/7))) {
			PlayerPrefs.SetInt ( "PlayerID", 1 );
			SceneManager.LoadScene (1);
		}
		if (GUILayout.Button ("Player 2",GUILayout.Width(Screen.width/5), GUILayout.Height(Screen.height/7))) {
			PlayerPrefs.SetInt ( "PlayerID", 2 );
			SceneManager.LoadScene (1);
		}
		if (GUILayout.Button ("Third Person",GUILayout.Width(Screen.width/5), GUILayout.Height(Screen.height/7))) {
			SceneManager.LoadScene (2);
		}
		GUILayout.Space ( Screen.width/5);
		GUILayout.EndHorizontal ();
		GUILayout.Space ( Screen.height*2/7);
		GUILayout.EndVertical ();
	}
}
