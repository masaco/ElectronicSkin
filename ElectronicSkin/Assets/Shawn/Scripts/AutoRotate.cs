using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

	public float RotateSpeed = 0.05f;
	
	void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime * RotateSpeed, Space.World);
	}

}
