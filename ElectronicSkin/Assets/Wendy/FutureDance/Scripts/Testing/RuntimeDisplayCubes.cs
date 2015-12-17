using UnityEngine;
using System.Collections;

public class RuntimeDisplayCubes : MonoBehaviour {

	public GameObject[] cubes;
	public bool status = false;

	public Vector3 positionBais;
	private Vector3 originalPos;
	public float scaleValue = 1.0f;

	// Use this for initialization
	void Start () {
		originalPos = transform.position;

		for( int i=0; i< cubes.Length; i++ )
		{
			cubes[i].GetComponent<Renderer>().enabled = status;
		}
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = originalPos + positionBais;
		transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f) * scaleValue;
	
		if (Input.GetKeyDown (KeyCode.E))
		{
			status = !status;
			for( int i=0; i< cubes.Length; i++ )
			{
				cubes[i].GetComponent<Renderer>().enabled = status;
			}
		}
	}
}
