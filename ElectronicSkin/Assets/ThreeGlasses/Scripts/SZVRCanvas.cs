using UnityEngine;
using System.Collections;
using SZVR_SDK;
using System;

public class SZVRCanvas : MonoBehaviour 
{
	private SZVRDevice device;

	private Mesh leftMesh = null;
	private Mesh rightMesh = null;

	private bool create = true;

	private Material leftMat;
	private Material rightMat;

	public bool useCameraTexture = false;
    //[HideInInspector]
	public RenderTexture cameraTexture;

	public RenderingPath cameraRenderingPath = RenderingPath.UsePlayerSettings;


	void Awake ()
	{
		create = true;
	}

	void Start () 
	{
        Screen.fullScreen = true;

		Camera camera = gameObject.GetComponent<Camera>();

		if(camera != null)
		{
			camera.clearFlags = CameraClearFlags.Nothing;
			camera.cullingMask = 0;
			camera.depth = 0;
			camera.orthographic = true;
			camera.renderingPath = RenderingPath.Forward;
			camera.useOcclusionCulling = false;
		}
		else
			Debug.Log("Please add this component to the game camera!");

		Material distortionMaterial = Resources.Load("Effects/DistortionMesh") as Material;
		if(distortionMaterial != null)
		{
#if UNITY_5
			leftMat = Instantiate<Material>(distortionMaterial);
			rightMat = Instantiate<Material>(distortionMaterial);
#else
			leftMat = Instantiate(distortionMaterial) as Material;
			rightMat = Instantiate(distortionMaterial) as Material;
#endif
		}
        if (leftMat == null)
            leftMat = new Material(Shader.Find("Effect/DistortionMesh"));
        if (rightMat == null)
            rightMat = new Material(Shader.Find("Effect/DistortionMesh"));
	}

	void Update ()
	{
		if(create)
		{
			if(useCameraTexture)
			{
				leftMesh = DistortionMesh.GenerateMesh(true, true);
				rightMesh = DistortionMesh.GenerateMesh(false, true);
			}
			else
			{
				leftMesh = DistortionMesh.GenerateMesh(true, false);
				rightMesh = DistortionMesh.GenerateMesh(false, false);
			}
		}
		create = false;
	}

	void OnRenderImage (RenderTexture src, RenderTexture dest)
	{
		RenderTexture.active = dest;

		GL.Clear (false, true, Color.black);
		DistortEye (false, src);
		DistortEye (true, src);

		GL.IssuePluginEvent(1);
	}

	void DistortEye (bool rightEye, RenderTexture src)
	{
		Material mat = rightEye ? rightMat : leftMat;
        mat.mainTexture = useCameraTexture ? cameraTexture : src;

		if(rightMesh == null || leftMesh == null)
			return;

		Mesh eyeMesh = rightEye ? rightMesh : leftMesh;

		float halfWidth = 0.5f * Screen.width;

		GL.Viewport (new Rect(rightEye ? halfWidth : 0f, 0f, halfWidth, Screen.height));

		GL.PushMatrix ();
		GL.LoadOrtho ();

		for(int i = 0; i < mat.passCount; i++)
		{
			mat.SetPass (i);
			if(eyeMesh != null) Graphics.DrawMeshNow (eyeMesh, Matrix4x4.identity);
		}

		GL.PopMatrix ();
	}

	public void CreateRenderTexture()
	{
		if(useCameraTexture)
		{
			cameraTexture = new RenderTexture(2560,1440,24,RenderTextureFormat.ARGB32);
			cameraTexture.antiAliasing = 8;
			cameraTexture.Create();
		}
	}
}
