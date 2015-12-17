using UnityEngine;
using System.Collections;

public class ElephantNose : MonoBehaviour {

	public Transform[] followPoints;
	public GameObject noseEnd;
	public int nodesBetweenPoints = 3;
	public GameObject noseNodeObj;
	public GameObject[] nodes;
	LineRenderer noseLine;

	// Use this for initialization
	void Start () {

		noseLine = gameObject.GetComponent<LineRenderer>();
		nodes = new GameObject[ (followPoints.Length+1) * nodesBetweenPoints + 1 ];

		for( int i=0; i< nodes.Length; i++ )
		{
			nodes[i] = (GameObject)Instantiate( noseNodeObj );
			nodes[i].transform.parent = gameObject.transform;
			nodes[i].name = i.ToString();
		}
		// dont let water drop collide
		nodes[ nodes.Length-1 ].GetComponent<Collider>().isTrigger = true;


		noseLine.SetVertexCount( nodes.Length );
	}
	
	// Update is called once per frame
	void Update () {
	
		CalculateNodes();

		for( int i=0; i< nodes.Length; i++ )
		{
			noseLine.SetPosition( i, new Vector3( nodes[i].transform.position.x, nodes[i].transform.position.y, transform.position.z) );
		}
		noseLine.SetPosition (nodes.Length - 1, new Vector3 (noseEnd.transform.position.x, noseEnd.transform.position.y, transform.position.z) );
	}

	void CalculateNodes ()
	{
		for( int i=0; i< followPoints.Length; i++ )
		{
			Vector3 pos = followPoints[i].position;
			pos.z = 0.0f;

			nodes[i * (nodesBetweenPoints+1)].transform.position = pos;
			Vector3 from = followPoints[i].transform.position;

			Vector3 to;

			// is the last node
			if( i == followPoints.Length-1 )
				to = noseEnd.transform.position;
			else
				to = followPoints[i+1].transform.position;

			for( int n=1; n< nodesBetweenPoints+1; n++ )
			{
				float ratio = (float)n/(float)(nodesBetweenPoints+1);
				Vector3 newPos = Vector3.Lerp( from, to, ratio );
				newPos.z = 0.0f;

				nodes[i * (nodesBetweenPoints+1) + n].transform.position = newPos;
			}
		}

		Vector3 noseEndPos = noseEnd.transform.position;
		noseEndPos.z = 0.0f;

		nodes [nodes.Length - 1].transform.position = noseEndPos;
	}
}
