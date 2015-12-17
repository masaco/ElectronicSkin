using UnityEngine;
using System.Collections;

public class ElephantShrinking : MonoBehaviour {

	public Transform[] partsToShrink;
	Vector3[] originalScales;
	bool shrinkKey = false;

	// Use this for initialization
	void Start () {
	
		originalScales = new Vector3[ partsToShrink.Length ];
		for( int i=0; i< partsToShrink.Length; i++ )
		{
			originalScales[i] = partsToShrink[i].localScale;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			if( !shrinkKey )
				StartCoroutine( "ShrinkStart" );
		}
	
	}

	IEnumerator ShrinkStart () {
		shrinkKey = true;

		int frames = 120;
		for( int i=0; i< frames; i++ )
		{
			float scaleRatio = (float)(frames - i)/(float)frames;
			for( int index = 0; index < partsToShrink.Length; index++ )
			{
				partsToShrink[index].localScale = originalScales[index] * scaleRatio;
			}

			yield return new WaitForSeconds( 0.03f );
		}
	}
}
