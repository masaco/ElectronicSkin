using UnityEngine;
using UnityEditor;

/*
 * VLight
 * Copyright Brian Su 2011-2014
*/
#pragma warning disable 0618
public class VolumeLightCreator : EditorWindow
{

	[MenuItem("GameObject/Create Other/V-Lights Spot", false, 100)]
	public static void StandardLight()
	{
		if(ShowWarning())
		{
#if !UNITY_4_2 && !UNITY_4_3 && !UNITY_4_4
			Undo.RegisterSceneUndo("V-Lights Create Light");
#endif
			GameObject volumeLightContainer = CreateVolumeLight(VLight.LightTypes.Spot);
			Selection.activeGameObject = volumeLightContainer;

#if UNITY_4_2 || UNITY_4_3 || UNITY_4_4
			Undo.RegisterCreatedObjectUndo(volumeLightContainer, "V-Lights Create Light");
#endif
		}
	}

	[MenuItem("GameObject/Create Other/V-Lights Spot With Light", false, 100)]
	public static void SpotWithLight()
	{
		if(ShowWarning())
		{

#if !UNITY_4_2 && !UNITY_4_3 && !UNITY_4_4
			Undo.RegisterSceneUndo("V-Lights Create Light");
#endif
			GameObject volumeLightContainer = CreateVolumeLight(VLight.LightTypes.Spot);
			GameObject pointLight = new GameObject("Spot light");
			Light light = pointLight.AddComponent<Light>();
			light.shadows = LightShadows.Soft;
			light.type = LightType.Spot;
			light.spotAngle = 45;
			light.range = 6;
			pointLight.transform.parent = volumeLightContainer.transform;
			pointLight.transform.localPosition = Vector3.zero;
			pointLight.transform.Rotate(90, 0, 0);
			Selection.activeGameObject = volumeLightContainer;

#if UNITY_4_2 || UNITY_4_3 || UNITY_4_4
			Undo.RegisterCreatedObjectUndo(volumeLightContainer, "V-Lights Create Light");
			Undo.RegisterCreatedObjectUndo(pointLight, "V-Lights Create Light");
#endif
		}
	}

	[MenuItem("GameObject/Create Other/V-Lights Point", false, 100)]
	public static void PointLight()
	{
		if(ShowWarning())
		{
#if !UNITY_4_2 && !UNITY_4_3 && !UNITY_4_4
			Undo.RegisterSceneUndo("V-Lights Create Light");
#endif
			GameObject volumeLightContainer = CreateVolumeLight(VLight.LightTypes.Point);
			Selection.activeGameObject = volumeLightContainer;

#if UNITY_4_2 || UNITY_4_3 || UNITY_4_4
			Undo.RegisterCreatedObjectUndo(volumeLightContainer, "V-Lights Create Light");
#endif
		}
	}

	[MenuItem("GameObject/Create Other/V-Lights Point With Light", false, 100)]
	public static void PointWithLight()
	{
		if(ShowWarning())
		{
#if !UNITY_4_2 && !UNITY_4_3 && !UNITY_4_4
			Undo.RegisterSceneUndo("V-Lights Create Light");
#endif
			GameObject volumeLightContainer = CreateVolumeLight(VLight.LightTypes.Point);
			GameObject pointLight = new GameObject("Point light");
			Light light = pointLight.AddComponent<Light>();
			light.shadows = LightShadows.Soft;
			light.type = LightType.Point;
			light.spotAngle = 45;
			light.range = 6;
			pointLight.transform.parent = volumeLightContainer.transform;
			pointLight.transform.localPosition = Vector3.zero;
			Selection.activeGameObject = volumeLightContainer;

#if UNITY_4_2 || UNITY_4_3 || UNITY_4_4
			Undo.RegisterCreatedObjectUndo(volumeLightContainer, "V-Lights Create Light");
			Undo.RegisterCreatedObjectUndo(pointLight, "V-Lights Create Light");
#endif
		}
	}

	private static GameObject CreateVolumeLight(VLight.LightTypes type)
	{
		
		VLight[] otherLights = GameObject.FindObjectsOfType(typeof(VLight)) as VLight[];
		GameObject volumeLightContainer = new GameObject("V-Light " + otherLights.Length);
		if(SceneView.lastActiveSceneView != null)
		{
			SceneView.lastActiveSceneView.MoveToView(volumeLightContainer.transform);
		}
		VLight light = volumeLightContainer.AddComponent<VLight>();

		volumeLightContainer.GetComponent<Camera>().enabled = false;
		
#if UNITY_4_2 || UNITY_4_3 || UNITY_4_4
		volumeLightContainer.camera.fieldOfView = 45;
		volumeLightContainer.camera.nearClipPlane = 0.1f;
		volumeLightContainer.camera.farClipPlane = 1;
#else
		volumeLightContainer.GetComponent<Camera>().fieldOfView = 45;
		volumeLightContainer.GetComponent<Camera>().nearClipPlane = 0.1f;
		volumeLightContainer.GetComponent<Camera>().farClipPlane = 1;		
#endif
		volumeLightContainer.GetComponent<Camera>().renderingPath = RenderingPath.VertexLit;
		volumeLightContainer.GetComponent<Camera>().orthographicSize = 2.5f;

		switch(type)
		{
			case VLight.LightTypes.Spot:
				light.lightType = VLight.LightTypes.Spot;
				break;
			case VLight.LightTypes.Point:
				volumeLightContainer.GetComponent<Camera>().orthographic = true;
#if UNITY_4_2 || UNITY_4_3 || UNITY_4_4			
				volumeLightContainer.camera.nearClipPlane = -volumeLightContainer.camera.farClipPlane;
				volumeLightContainer.camera.orthographicSize = volumeLightContainer.camera.farClipPlane * 2;
#else
				volumeLightContainer.GetComponent<Camera>().nearClipPlane = -volumeLightContainer.GetComponent<Camera>().farClipPlane;
				volumeLightContainer.GetComponent<Camera>().orthographicSize = volumeLightContainer.GetComponent<Camera>().farClipPlane * 2;			
#endif
				light.lightType = VLight.LightTypes.Point;
				break;
		}

		int layer = LayerMask.NameToLayer(VLightManager.VOLUMETRIC_LIGHT_LAYER_NAME);
		if(layer != -1)
		{
			volumeLightContainer.layer = layer;
			volumeLightContainer.GetComponent<Camera>().cullingMask = ~(1 << layer);
		}

		volumeLightContainer.transform.Rotate(90, 0, 0);
		return volumeLightContainer;
	}

	private static bool ShowWarning()
	{
		bool continueAfterWarning = true;
		if(LayerMask.NameToLayer(VLightManager.VOLUMETRIC_LIGHT_LAYER_NAME) == -1)
		{
			continueAfterWarning = EditorUtility.DisplayDialog("Warning",
                "You don't have a layer in your project called\n\"" + VLightManager.VOLUMETRIC_LIGHT_LAYER_NAME + "\".\n" +
				"Without this layer realtime shadows, interleaved sampling and high speed off screen rendering will not work. Continue using volumetric lights?", "Continue", "Cancel");
		}
		return continueAfterWarning;
	}
}
