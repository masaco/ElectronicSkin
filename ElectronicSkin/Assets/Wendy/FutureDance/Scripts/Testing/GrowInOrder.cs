using UnityEngine;
using System.Collections;

public class GrowInOrder : MonoBehaviour {

	public GameObject[] objsToGrow;
	private int nowIndex = 0;

	// Use this for initialization
	void Start () {
	
		for( int i=0; i< objsToGrow.Length; i++ )
		{
			objsToGrow[i].transform.localScale = new Vector3( 0.0f, 0.0f, 0.0f );
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			StartCoroutine( "Grow", nowIndex );
			nowIndex++;
		}
	}

	IEnumerator Grow ( int index )
	{
		if( index < objsToGrow.Length )
		{
			for( int i=0; i< 60; i++ )
			{
				objsToGrow[index].transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * ( (float)i/60.0f );
				yield return new WaitForSeconds( 0.03f );
			}
			objsToGrow[index].transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f );
		}
	}
}
