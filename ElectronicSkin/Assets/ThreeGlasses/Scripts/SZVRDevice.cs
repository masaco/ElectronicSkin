/* */

using System;
using System.Collections;
using UnityEngine;
using SZVR_SDK;

public class SZVRDevice : MonoBehaviour
{
	[HideInInspector]
	public Camera leftEyeCamera;
	[HideInInspector]
	public Camera rightEyeCamera;

	public float nearClipPlane = 0.03f;
	public float farClipPlane = 1000.0f;

	private float rotationY = 0.0f;
	private Quaternion orentationOffset;
	private Vector3 positionOffset;

	//Pupillary distance.
	private float ipd;

	public static Quaternion direction = Quaternion.identity;

	public Transform followTarget;
	public float eyeToNeckHeight;

	private SZVRCanvas canvas;

    private float updateInterval = 0.5f;
    private float accum = 0.0f;
    private float frames = 0;
    private float timeleft;   

	void Start ()
	{
		if(!DeviceInterface.isInitFinish)
			DeviceInterface.Initialize();
		DeviceInterface.ResetOrientation();

		canvas = gameObject.GetComponent<SZVRCanvas>();
		if(canvas == null)
			canvas = gameObject.AddComponent<SZVRCanvas>();
		canvas.CreateRenderTexture();

		if(DeviceInterface.isInitFinish)
		{
			ipd = DeviceInterface.ipd;
			//You can manually add two names were LeftEye and RightEye camera.
			leftEyeCamera = SimulationEye("LeftEye", -ipd, -1);
			rightEyeCamera = SimulationEye("RightEye", ipd, -2);
		}

		if(followTarget != null)
			orentationOffset = Quaternion.Euler(transform.localEulerAngles);
		else
			orentationOffset = transform.rotation;

		if(leftEyeCamera != null && rightEyeCamera != null)
		{
			leftEyeCamera.transform.localPosition += new Vector3(0.0f, eyeToNeckHeight, 0.0f);
			rightEyeCamera.transform.localPosition += new Vector3(0.0f, eyeToNeckHeight, 0.0f);
		}

        timeleft = updateInterval;
	}

	private Camera SimulationEye(string name, float ipd, int depth)
	{
		Transform tran = transform.FindChild(name);

		if(tran == null)
		{
			tran = new GameObject(name).transform;
#if UNITY_5
			tran.SetParent(transform);
#else
			tran.parent = transform;
#endif
		}
		tran.localPosition = new Vector3(0.5f*ipd,0,0);
		Camera cam = tran.GetComponent<Camera>();
		if(cam == null)
			cam = tran.gameObject.AddComponent<Camera>();
		cam.depth = depth;
        cam.fieldOfView = DistortionMesh.GetFOV().y;
		cam.nearClipPlane = nearClipPlane;
		cam.farClipPlane = farClipPlane;
		if(name.Equals("LeftEye"))
			cam.rect = new Rect(0,0,0.5f,1);
		else
			cam.rect = new Rect(0.5f,0,0.5f,1);

		cam.renderingPath = canvas.cameraRenderingPath;
		cam.targetTexture = canvas.cameraTexture;

		return cam;
	}

	void Update ()
	{
		DeviceInterface.UpdateDeviceTesting();

		if(DeviceInterface.sensorAttached)
		{
			SetModel(true);

			Quaternion cameraOrientation = DeviceInterface.GetCameraOrientation();

			if(followTarget != null)
				direction = followTarget.rotation * orentationOffset * cameraOrientation;
			else
				direction = orentationOffset * cameraOrientation;

			transform.rotation = direction;
			leftEyeCamera.transform.localEulerAngles = rightEyeCamera.transform.localEulerAngles = Vector3.zero;

			ResetOrientation ();
		}
		else
		{
			Debug.LogError("Not found the device sensors");

			SetModel(false);
		}

		if(Input.GetKeyDown(KeyCode.F5))
			StartCoroutine(ReloadScene ());
	}

	void SetModel(bool isVirtualReality)
	{
		if(canvas != null)
			canvas.enabled = isVirtualReality;
	}

	void ResetOrientation ()
	{
		if(Input.GetKeyUp(KeyCode.R))
			DeviceInterface.ResetOrientation();
	}

	IEnumerator ReloadScene ()
	{
		AsyncOperation async = Application.LoadLevelAsync(0);
		yield return async;
	}

    public string ShowFps()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft <= 0.0)
        {
            return (accum / frames).ToString("f2");
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
        else
        {
            return "0";
        }
    }

    void OnDestroy()
    {
        DeviceInterface.DeleteDevice();
    }
}
