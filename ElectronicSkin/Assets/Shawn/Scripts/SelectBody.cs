using UnityEngine;
using System.Collections;

public class SelectBody : MonoBehaviour {

	private int bodyID = 0;
	public GameObject[] Body;
	public Camera cam;

	public Light fadeLight;
	public Light fadeDirLight;
	public MeshRenderer fadeMirrorMat;
	public MeshRenderer fadeSceneMat;

	private Animator animator;
	private Vector3 StartPos;
	private Vector3 StartRot;
	

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
		
    }

	IEnumerator Start() {
		yield return new WaitForSeconds(3f);
		Body[bodyID].SetActive(true);
		animator = Body[bodyID].GetComponent<Animator>();
		StartCoroutine(Fade(1));
		isReady = true;
    }
	
	void Update () {
		if (!isReady)
			return;
		cam.transform.localEulerAngles += Vector3.right*  Input.GetAxis("Mouse Y")*-1;
		transform.localEulerAngles += Vector3.up * Input.GetAxis("Mouse X");

		if (Input.GetKeyDown(KeyCode.Alpha1))
			animator.SetTrigger("Act1");
		if (Input.GetKeyDown(KeyCode.Alpha2))
			animator.SetTrigger("Act2");
		if (Input.GetKeyDown(KeyCode.Alpha3))
			animator.SetTrigger("Act3");
		if (Input.GetKeyDown(KeyCode.Alpha4))
			animator.SetTrigger("Fly");
		if (Input.GetKeyDown(KeyCode.R))
		{
			transform.position = StartPos;
			transform.eulerAngles = StartRot;
		}

		if (Input.GetAxis("Horizontal")!=0f || Input.GetAxis("Vertical") != 0f)
			animator.SetBool("Walk", true);
		else
			animator.SetBool("Walk", false);

		transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * Time.deltaTime, Space.Self);

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

			if (Input.GetKeyDown(KeyCode.Home) && fadeState == FadeState.Ready)
				StartCoroutine(Fade(1));
			if (Input.GetKeyDown(KeyCode.End) && fadeState == FadeState.Ready)
				StartCoroutine(Fade(-1));
		}
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
					Body[bodyID].BroadcastMessage("FadeOutParticle", SendMessageOptions.DontRequireReceiver);
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
        fadeLight.intensity = Mathf.Lerp(0f, 6f, fastFade);
		fadeLight.spotAngle = Mathf.Lerp(45f, 90f, fastFade);
		fadeDirLight.intensity = Mathf.Lerp(0f, 0.2f, fastFade);
		RenderSettings.ambientIntensity = Mathf.Lerp(0f, 1.5f, fastFade);
		fadeMirrorMat.material.SetFloat("_Transparency", Mathf.Lerp(1f, 0f, fastFade));
		fadeMirrorMat.material.SetColor("_Color", Color.white * Mathf.Lerp(0f, 0.3f, slowFade));
		float brightness = Mathf.Lerp(0f, 0.1f, fastFade);
		fadeSceneMat.material.SetColor("_EmissionColor", new Color(brightness, brightness, brightness, 1f));
		cam.backgroundColor = Color.white * Mathf.Lerp(0f, 0.2f, fastFade);
	}

	IEnumerator FadeAndSwitchBody( int state ) {
		isFadeSwitch = true;
		yield return Fade( -1 );
		SwitchBody(state);
		yield return new WaitForSeconds(0.3f);
		yield return Fade(1);
		isFadeSwitch = false;
	}

	void SwitchBody( int state)
	{
		if (state == 1)
			bodyID++;
		else
			bodyID--;

		if (bodyID < 0)
			bodyID = 6;

		bodyID = bodyID % 7;

		foreach (GameObject body in Body)
			body.SetActive(false);

		Body[bodyID].SetActive(true);
		animator = Body[bodyID].GetComponent<Animator>();
		Body[bodyID].SendMessage("ReInit", SendMessageOptions.DontRequireReceiver);
    }
}
