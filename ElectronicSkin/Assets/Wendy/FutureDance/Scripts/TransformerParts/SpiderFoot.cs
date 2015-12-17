using UnityEngine;
using System.Collections;

public class SpiderFoot : MonoBehaviour {

	public Transform[] footPoints;
	LineRenderer line;

	// Use this for initialization
	void Start () {
	
		line = gameObject.GetComponent<LineRenderer> ();
		line.SetVertexCount (footPoints.Length);
	}
	
	// Update is called once per frame
	void Update () {
	
		for( int i=0; i< footPoints.Length; i++ )
		{
			line.SetPosition(i, footPoints[i].localPosition);
		}
	}
}
