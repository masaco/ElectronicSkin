using UnityEngine;
using System.Collections;

public class ChangeScenes : MonoBehaviour {

	public GameObject[] main_scenes;
	public GameObject otherPlayer;
	public GameObject[] select_scenes;


	// Use this for initialization
	void Start () {

		for (int i =0 ; i<main_scenes.Length ; i++)
		{
			main_scenes[i].SetActive(false);
		}

		for(int i =0 ; i <select_scenes.Length ; i++ )
		{
			select_scenes[i].SetActive(true);
		}

		otherPlayer.SetActive(false);

	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Tab))
		{
			SwitchScenes(false);
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			SwitchScenes(true);
		}
	
	}
	void OnTriggerEnter(Collider other)
	{
//		if (other.tag.Contains ("CollisionBody")) Application.LoadLevelAsync("main");
		if (other.tag=="CollisionBody")
		{
			SwitchScenes(true);
		}

	}

	void SwitchScenes(bool isSwitch)
	{
		if(isSwitch)
		{
			for (int i =0 ; i<main_scenes.Length ; i++)
			{
				main_scenes[i].SetActive(true);
			}

			for(int i =0 ; i <select_scenes.Length ; i++ )
			{
				select_scenes[i].SetActive(false);
			}

			otherPlayer.SetActive(true);
		}
		else
		{
			for (int i =0 ; i<main_scenes.Length ; i++)
			{
				main_scenes[i].SetActive(false);
			}

			for(int i =0 ; i <select_scenes.Length ; i++ )
			{
				select_scenes[i].SetActive(true);
			}

			otherPlayer.SetActive(false);
		}
	}
}
