using UnityEngine;
using System.Collections;

public class CrocTeethControl : MonoBehaviour {

	LineRenderer line;
	public float height = 0.0f;

	// Use this for initialization
	void Start () {
	
		line = gameObject.GetComponent<LineRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TeethUp ( float teethHeight )
	{
		height = teethHeight;
		line.SetPosition (1, new Vector3( 0.0f, height, 0.0f ));
	}

	public void TeethBottom ( float teethHeight )
	{
		height = teethHeight;
		line.SetPosition (1, new Vector3( 0.0f, -1* height, 0.0f ));
	}
}
