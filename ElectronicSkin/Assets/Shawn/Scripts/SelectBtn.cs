using UnityEngine;
using System.Collections;

public class SelectBtn : MonoBehaviour {
	public int num = 1;
	void OnTriggerEnter(Collider other)
	{
		
		if (other.tag.Contains ("CollisionBody") ) {
			transform.root.SendMessage("BtnDown", num, SendMessageOptions.DontRequireReceiver );
		}

	}
}
