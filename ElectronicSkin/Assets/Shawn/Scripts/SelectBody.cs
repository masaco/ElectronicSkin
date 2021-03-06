﻿using UnityEngine;
using System.Collections;

public class SelectBody : MonoBehaviour {

	private int bodyID = 0;
	public GameObject Body;
	public Camera[] cam;

	public Color[] btnColor;

	public ParticleSystem[] SelectBtnsParticle;

	public Light fadeLight;
	public Light fadeDirLight;
	public MeshRenderer fadeMirrorMat;
	public MeshRenderer fadeSceneMat;

	public MQMapperManager roadSelfID;

//	private Animator animator;
	private Vector3 StartPos;
	private Vector3 StartRot;

	private int btnCount;

	enum FadeState {
		Fading,
		Ready
	}

	private FadeState fadeState = FadeState.Ready;
	private bool isFadeIn;
	private bool isFadeSwitch;
	private bool isFaceMirror = true;

	private bool isReady;

	void Awake () {
		foreach ( Camera tempCam in cam )
			tempCam.backgroundColor = Color.black;
	
		SelectBtnsParticle [0].startColor = btnColor [6];
		SelectBtnsParticle [1].startColor = btnColor [1];
		SelectBtnsParticle [2].startColor = btnColor [0];
		foreach (ParticleSystem ps in SelectBtnsParticle) {
			ps.emissionRate = 0f;
		}

    }

	IEnumerator Start() {		
//		animator = Body.GetComponent<Animator>();
		yield return new WaitForSeconds(3f);
		StartCoroutine(Fade(1));
		isReady = true;
		foreach (ParticleSystem ps in SelectBtnsParticle) {
			ps.emissionRate = 500f;
		}
    }
	
	void Update () {
		if (!isReady)
			return;

		#region Character Control
//		cam.transform.localEulerAngles += Vector3.right*  Input.GetAxis("Mouse Y")*-1;
		transform.localEulerAngles += Vector3.up * Input.GetAxis("Mouse X");

//		if (Input.GetKeyDown(KeyCode.Alpha1))
//			animator.SetTrigger("Act1");
//		if (Input.GetKeyDown(KeyCode.Alpha2))
//			animator.SetTrigger("Act2");
//		if (Input.GetKeyDown(KeyCode.Alpha3))
//			animator.SetTrigger("Act3");
//		if (Input.GetKeyDown(KeyCode.Alpha4))
//			animator.SetTrigger("Fly");
		if (Input.GetKeyDown(KeyCode.R))
		{
			transform.position = StartPos;
			transform.eulerAngles = StartRot;
		}

//		if (Input.GetAxis("Horizontal")!=0f || Input.GetAxis("Vertical") != 0f)
//			animator.SetBool("Walk", true);
//		else
//			animator.SetBool("Walk", false);

		transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * Time.deltaTime, Space.Self);
		#endregion

		#region Character Select
		if (!isFadeSwitch && isFaceMirror)
		{
			if (Input.GetKeyDown(KeyCode.O))
				StartCoroutine(FadeAndSwitchBody(1));

			if (Input.GetKeyDown(KeyCode.P))
				StartCoroutine(FadeAndSwitchBody(-1));

			if (Input.GetKeyDown(KeyCode.PageUp))
				SwitchBody(1);

			if (Input.GetKeyDown(KeyCode.PageDown))
				SwitchBody(-1);

//			if (Input.GetKeyDown(KeyCode.Home) && fadeState == FadeState.Ready)
//				StartCoroutine(Fade(1));
//			if (Input.GetKeyDown(KeyCode.End) && fadeState == FadeState.Ready)
//				StartCoroutine(Fade(-1));
		}
		#endregion
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name.Contains("Mirror"))
		{
			isFaceMirror = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.name.Contains("Mirror"))
		{
			isFaceMirror = false;
		}
	}

	IEnumerator Fade(int state)
	{
		float duringTime = 3f;
		fadeState = FadeState.Fading;
		if (state == 1)
		{
			if (!isFadeIn)
			{
				isFadeIn = true;
				float during = 0f;
				while (during < duringTime)
				{
					yield return new WaitForEndOfFrame();
					during += Time.deltaTime * 1.8f;
					fadeList(during, 1, duringTime);
				}
			} 
		}
		else
		{
			if (isFadeIn)
			{
				isFadeIn = false;
				float during = duringTime;
				
				while (during > 0f)
				{
					yield return new WaitForEndOfFrame();
					during -= Time.deltaTime*2f;
					fadeList(during, -1, duringTime);
					Body.BroadcastMessage("FadeOutParticle", SendMessageOptions.DontRequireReceiver);
				}
			}
		}			
		fadeState = FadeState.Ready;
	}

	void fadeList(float during, int state, float duringTime)
	{
		float fastFade = during / 2f;
		float slowFade = during / duringTime;
		if (state == -1)
			fastFade = (during - 1f) / 2f;
        fadeLight.intensity = Mathf.Lerp(0f, 3f, fastFade);
		fadeLight.spotAngle = Mathf.Lerp(45f, 90f, fastFade);
		fadeDirLight.intensity = Mathf.Lerp(0f, 0.1f, fastFade);
		RenderSettings.ambientIntensity = Mathf.Lerp(0f, 1.5f, fastFade);
		fadeMirrorMat.material.SetFloat("_Transparency", Mathf.Lerp(1f, 0f, fastFade));
		fadeMirrorMat.material.SetColor("_Color", Color.white * Mathf.Lerp(0f, 0.3f, slowFade));
		float brightness = Mathf.Lerp(0f, 0.1f, fastFade);
		fadeSceneMat.material.SetColor("_EmissionColor", new Color(brightness, brightness, brightness, 1f));
		foreach ( Camera tempCam in cam )
			tempCam.backgroundColor = Color.white * Mathf.Lerp(0f, 0.2f, fastFade);
	}

	void BtnDown( int num )
	{
		StartCoroutine(FadeAndSwitchBody(num));
	}

	IEnumerator FadeAndSwitchBody( int state ) {
		foreach (ParticleSystem ps in SelectBtnsParticle) {
			ps.emissionRate = 0f;
		}	

		isFadeSwitch = true;
		yield return Fade( -1 );
		SwitchBody(state);

		yield return new WaitForSeconds(0.3f);
		foreach (ParticleSystem ps in SelectBtnsParticle) {
			ps.emissionRate = 500f;
		}	
		yield return Fade(1);

		isFadeSwitch = false;
	}

	void SwitchBody( int state)
	{
		bodyID += state;
		bodyID = GetNum (bodyID);
		SelectBtnsParticle [0].startColor = btnColor [GetNum(bodyID-1)];
		SelectBtnsParticle [1].startColor = btnColor [GetNum(bodyID+1)];
		SelectBtnsParticle [2].startColor = btnColor [bodyID];


		if (roadSelfID.selfID == 1)RabbitMQColorMapper.colorIDP1 = GetNum(bodyID);
		else if (roadSelfID.selfID == 2)RabbitMQColorMapper.colorIDP2 = GetNum(bodyID);
		Debug.Log("getnum : " + bodyID);

		Body.SendMessage ("ChangeBody", GetNum(bodyID), SendMessageOptions.DontRequireReceiver);
		Body.SendMessage("ReInit", SendMessageOptions.DontRequireReceiver);
    }

	int GetNum( int num )
	{
		if (num > 6)
			num = 0;
		else if (num < 0)
			num = 6;
		return num;
	}
}
