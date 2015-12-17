using UnityEngine;
using System.Collections;

public class BodyPartGrowing : MonoBehaviour {


	public Transform[] parts;
	public AnimationCurve curve;
	public float growTime = 1.0f;
	public float toSize = 1.0f;

	// Use this for initialization
	void Start () {
	
		for( int index=0; index< parts.Length; index++ )
			parts[index].transform.localScale = new Vector3( 0.0f, 0.0f, 0.0f );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Grow ( )
	{
		int frames = (int)(growTime * 30.0f);

		for( int i=0; i< frames; i++ )
		{
			for( int index=0; index< parts.Length; index++ )
			{
				parts[index].transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * curve.Evaluate( (float)i/(float)frames ) * toSize;
			}
			yield return new WaitForSeconds( 0.03f );
		}

		for( int index=0; index< parts.Length; index++ )
			parts[index].transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * toSize;
	}

	IEnumerator Shrink ( )
	{
		int frames = (int)(growTime * 30.0f);
		
		for( int i=0; i< frames; i++ )
		{
			for( int index=0; index< parts.Length; index++ )
			{
				parts[index].transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * curve.Evaluate( 1 - (float)i/(float)frames ) * toSize;
			}
			yield return new WaitForSeconds( 0.03f );
		}
		
		for( int index=0; index< parts.Length; index++ )
			parts[index].transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * 0.0f;
	}
}
