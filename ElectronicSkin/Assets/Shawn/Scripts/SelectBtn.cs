using UnityEngine;
using System.Collections;

public class SelectBtn : MonoBehaviour {
	public int num = 1;
	public GameObject GO;
	void OnTriggerEnter(Collider other)
	{
		
		if (other.tag.Contains ("CollisionBody") ) {
			GO.SendMessage("BtnDown", num, SendMessageOptions.DontRequireReceiver );
			Debug.Log("is trigger");
		}

	}
}
