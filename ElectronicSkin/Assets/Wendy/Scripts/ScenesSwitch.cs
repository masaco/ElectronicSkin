using UnityEngine;
using System.Collections;

public class ScenesSwitch : MonoBehaviour {

	public GameObject main_scenes;
	public GameObject main_other;
	public GameObject otherPlayer;

	public GameObject select_scenes;
	public GameObject select_other;

	// Use this for initialization
	void Start () {

		select_scenes.SetActive(true);
		select_other.SetActive(true);

		otherPlayer.SetActive(false);
		main_other.SetActive(false);
		main_scenes.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void SwitchScenes(bool isSwitch)
	{
		if(isSwitch)
		{
			main_other.SetActive(true);
			main_scenes.SetActive(true);
			otherPlayer.SetActive(true);

			select_other.SetActive(false);
			select_scenes.SetActive(false);

		}
	}
}
