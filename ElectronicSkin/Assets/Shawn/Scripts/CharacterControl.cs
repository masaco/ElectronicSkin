using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {

	public bool Rest;
	public bool Control;
	private Animator animator;
	private Vector3 StartPos;
	private Vector3 StartRot;
	void Start () {
		animator = GetComponent<Animator>();
		StartPos = transform.position;
		StartRot = transform.eulerAngles;
	}

	void Update () {
		if (Rest)
			return;
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
			transform.position		= StartPos;
			transform.eulerAngles	= StartRot;
        }

		if (Control)
		{
			transform.Translate( new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * Time.deltaTime);
		}
			

	}
}
