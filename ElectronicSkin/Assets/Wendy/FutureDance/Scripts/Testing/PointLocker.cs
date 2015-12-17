using UnityEngine;
using System.Collections;

public class PointLocker : MonoBehaviour {

	private LineRenderer line;
	public Transform hookPoint;
	public bool hookOn = false;

	// Use this for initialization
	void Start () {

		line = gameObject.GetComponent<LineRenderer> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
		line.SetPosition (0, transform.position);

		if (hookOn)
				line.SetPosition (1, hookPoint.position);
		else
				line.SetPosition (1, transform.position);
	}

	public void HookOn ()
	{
		hookOn = true;
	}

	public void HookOff ()
	{
		hookOn = false;
	}
}
