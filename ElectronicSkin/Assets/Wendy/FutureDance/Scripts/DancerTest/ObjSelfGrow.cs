using UnityEngine;
using System.Collections;

public class ObjSelfGrow : MonoBehaviour {

	public float startSize = 0.0f;
	public float growTime = 1.0f;

	// Use this for initialization
	void Start () {
	
		transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * startSize;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator Grow ( float toSize )
	{
		float fromSize = transform.localScale.x;
		float diff = toSize - fromSize;
		int frames = (int)((float)growTime * 30.0f);

		for( int i=0; i< frames; i++ )
		{
			transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * ( fromSize + diff * ( (float)i/(float)frames ) );
			Debug.Log ( (float)i/(float)frames );
			yield return new WaitForSeconds( 0.03f );
		}
		transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f) * toSize;
	}
}
