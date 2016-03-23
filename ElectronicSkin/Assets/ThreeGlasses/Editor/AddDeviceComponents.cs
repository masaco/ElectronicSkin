using UnityEngine;
using UnityEditor;

public class AddDeviceComponents : Editor
{
	[MenuItem ("ThreeGlasses/AddDevice")]
	static void AddDevice()
	{
		GameObject cam = Selection.activeGameObject;

		if(cam == null)
			cam = Camera.main.gameObject;

		if(cam.GetComponent<Camera>() != null)
		{
			Device (cam);
			CreateEye (cam);
			Canvas (cam);
		}
	}

	public static void Device (GameObject cam)
	{
		SZVRDevice device = cam.GetComponent<SZVRDevice>();
		if(device == null)
			cam.AddComponent<SZVRDevice>();
	}

	public static void CreateEye (GameObject cam)
	{
		EyeCamera(cam.transform, "LeftEye", -1);
		EyeCamera(cam.transform, "RightEye", -2);
	}

	public static void Canvas (GameObject cam)
	{
		SZVRCanvas canvas = cam.GetComponent<SZVRCanvas>();
		if(canvas == null)
			cam.AddComponent<SZVRCanvas>();
	}

	private static void EyeCamera(Transform parent, string name, int depth)
	{
		GameObject obj = new GameObject();
		obj.name = name;
		Camera cam = obj.AddComponent<Camera>();
		cam.transform.parent = parent;
		cam.depth = depth;
		cam.fieldOfView = 60.0f;
		cam.nearClipPlane = 0.03f;
		cam.farClipPlane = 1000.0f;
		if(name.Equals("LeftEye"))
			cam.rect = new Rect(0,0,0.5f,1);
		else
			cam.rect = new Rect(0.5f,0,0.5f,1);
	}
}
