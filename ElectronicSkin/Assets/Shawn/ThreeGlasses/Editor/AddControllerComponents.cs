using UnityEngine;
using UnityEditor;

public class AddControllerComponents : Editor
{
	[MenuItem ("ThreeGlasses/AddController")]

	static void AddController()
	{
		GameObject player = Selection.activeGameObject;

		if(!player.CompareTag("Player"))
			Debug.Log("Please give the object labeling!");

		if(player != null)
		{
			if(player.GetComponent<CharacterController>() == null)
			{
				CharacterController controller = player.AddComponent<CharacterController>();
				controller.height = 1.7f;
				controller.radius = 0.4f;
			}
			if(player.GetComponent<SZVRController>() == null)
				player.AddComponent<SZVRController>();
		}
	}

    [MenuItem ("ThreeGlasses/AddNewController")]
    static void AddNewController()
    {
        GameObject player = Selection.activeGameObject;

        if (!player.CompareTag("Player"))
            Debug.Log("Please give the object labeling!");

        if (player != null)
        {
            if (player.GetComponent<CapsuleCollider>() == null)
            {
                CapsuleCollider capsuleCollider = player.AddComponent<CapsuleCollider>();
                capsuleCollider.height = 1.7f;
                capsuleCollider.radius = 0.4f;
            }
            if (player.GetComponent<SZVRFirstPersonController>() == null)
                player.AddComponent<SZVRFirstPersonController>();

            SZVRDevice device = player.GetComponentInChildren<SZVRDevice>();
            if (device != null)
                device.followTarget = player.transform;
        }
    }
}
