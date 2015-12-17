using UnityEngine;
using System.Collections;

public class FrameRateSetting : MonoBehaviour {

	public int frameRateNum = 30;

	// Use this for initialization
	void Start () {
	
		Application.targetFrameRate = frameRateNum;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
