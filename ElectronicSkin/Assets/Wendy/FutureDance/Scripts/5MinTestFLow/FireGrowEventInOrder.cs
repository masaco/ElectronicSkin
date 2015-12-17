using UnityEngine;
using System.Collections;

public class FireGrowEventInOrder : MonoBehaviour {

	public GameObject[] objsToGrow;
	private int nowIndex = 0;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			if( nowIndex < objsToGrow.Length )
			{
				objsToGrow[nowIndex].SendMessage( "Grow" );
				nowIndex++;
			}
			else
			{
				for( int i=0; i< objsToGrow.Length; i++ )
				{
					objsToGrow[i].SendMessage( "Shrink" );
				}
			}
		}
	}
}
