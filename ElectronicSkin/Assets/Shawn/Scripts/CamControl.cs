using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour {

	public float MoveSpeed = 1.2f;
	public float RotateSpeed = 1.5f;
	private bool holdCam;


	private Camera cam;
	void Start () {
		cam = GetComponent<Camera> ();
		Debug.Log(cam);
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.Space))
			holdCam = !holdCam;

		if (!holdCam)
			return;


		transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * Time.deltaTime * MoveSpeed, Space.Self);
		cam.transform.localEulerAngles += Vector3.right*  Input.GetAxis("Mouse Y")*-1 * RotateSpeed;
		transform.localEulerAngles += Vector3.up * Input.GetAxis("Mouse X")* RotateSpeed;

		if (Input.GetKey (KeyCode.Q))
			transform.Translate ( Vector3.up* Time.deltaTime * MoveSpeed, Space.World );
		else if (Input.GetKey (KeyCode.E))
			transform.Translate ( Vector3.down* Time.deltaTime * MoveSpeed, Space.World );
	}
}
