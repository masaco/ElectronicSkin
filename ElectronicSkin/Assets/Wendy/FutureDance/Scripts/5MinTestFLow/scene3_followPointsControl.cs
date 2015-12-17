using UnityEngine;
using System.Collections;

public class scene3_followPointsControl : MonoBehaviour {

	public AroundDancerPoint[] points;
	public AnimationCurve curve;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.P))
		{
			StartCoroutine( "PlayCurve" );
		}
	}

	IEnumerator PlayCurve ()
	{
		int frames = 30 * 30; // 20 seconds

		for( int i=0; i< frames; i++ )
		{
			float curveTime = (float)i/(float)frames;
			for( int index=0; index < points.Length; index++ )
			{
				points[index].ratio = curve.Evaluate( curveTime );
			}

			yield return new WaitForSeconds( 0.03f );
		}
	}
}
