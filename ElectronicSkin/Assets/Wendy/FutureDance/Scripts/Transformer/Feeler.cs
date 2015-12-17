using UnityEngine;
using System.Collections;

[RequireComponent (typeof(LineRenderer))]

public class Feeler : MonoBehaviour {

	public GameObject[] nodes;
	LineRenderer line;

	// Use this for initialization
	void Start () {
	
		line = gameObject.GetComponent<LineRenderer> ();
		line.SetVertexCount (nodes.Length+1);
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 localPosAdder = Vector3.zero;

		line.SetPosition ( 0, Vector3.zero );
		for( int i=0; i< nodes.Length; i++ )
		{
			line.SetPosition( i+1, nodes[i].transform.position + localPosAdder );
			localPosAdder += nodes[i].transform.localPosition;
		}

	}
}
