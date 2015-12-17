using UnityEngine;
using System.Collections;

[RequireComponent (typeof(LineRenderer))]

public class ShadeLine : MonoBehaviour {

	public Transform[] followPoints;
	public int nodesBetweenPoints = 3;
	public GameObject nodeObj;

	// change scale by length
	public bool scaleNodes = false;
	public AnimationCurve nodeScalingCurve;
	public float scaleMutiplier = 1.0f;

	GameObject[] nodes; // objects between each follow points, every follow points has a node also
	LineRenderer line;

	// Use this for initialization
	void Start () {

		line = gameObject.GetComponent<LineRenderer>();
		nodes = new GameObject[ (followPoints.Length-1) * (nodesBetweenPoints+1) + 1 ];
		line.SetVertexCount (nodes.Length);

		// init nodes
		for( int i=0; i< nodes.Length; i++ )
		{
			nodes[i] = (GameObject)Instantiate( nodeObj );
			nodes[i].transform.parent = gameObject.transform;
			nodes[i].name = "node"+i.ToString();
		}

		// change node scales ?
		if (scaleNodes)
		{
			for( int i=0; i< nodes.Length; i++ )
			{
				float t = (float)i/(float)(nodes.Length-1);
				float value = nodeScalingCurve.Evaluate( t ) * scaleMutiplier;

				nodes[i].transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * value;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		CalculateNodes ();
		UpdateLinePos ();

	}

	void CalculateNodes ()
	{
		for( int i=0; i< followPoints.Length-1; i++ )
		{
			Vector3 from = followPoints[i].position;
			Vector3 to = followPoints[i+1].position;

			for( int n=0; n< nodesBetweenPoints+1; n++ )
			{
				float ratio = (float)n/(float)(nodesBetweenPoints+1);
				Vector3 newPos = Vector3.Lerp( from, to, ratio );
				newPos.z = transform.position.z;
				
				nodes[i * (nodesBetweenPoints+1) + n].transform.position = newPos;
				nodes[i * (nodesBetweenPoints+1) + n].transform.rotation = Quaternion.Euler( (to - from).normalized );
			}
		}
		Vector3 lastPos = followPoints [followPoints.Length - 1].position;
		lastPos.z = transform.position.z;
		nodes [nodes.Length - 1].transform.position = lastPos;
	}

	void UpdateLinePos ()
	{
		for( int i=0; i< nodes.Length; i++ )
		{
			line.SetPosition( i, nodes[i].transform.position );
		}
	}
}
