using UnityEngine;
using System.Collections;

public class scene0_HeavyInOrder : MonoBehaviour {

	public GameObject[] objsToGrow;
	public float[] ToSize;
	int statusIndex = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			statusIndex++;

			if( statusIndex == 1 )
			{
				// foot become heavy
				objsToGrow[0].SendMessage( "Grow", ToSize[0] );
			}
			else if ( statusIndex == 2 )
			{
				objsToGrow[0].SendMessage( "Grow", 0.0f );
			}
			else if ( statusIndex == 3 )
			{
				// hands become heavy
				objsToGrow[1].SendMessage( "Grow", ToSize[1] );
				objsToGrow[2].SendMessage( "Grow", ToSize[2] );
			}
			else if ( statusIndex == 4 )
			{
				objsToGrow[1].SendMessage( "Grow", 0.0f);
				objsToGrow[2].SendMessage( "Grow", 0.0f );
			}
			else if ( statusIndex == 5 )
			{
				// head
				objsToGrow[3].SendMessage( "Grow", ToSize[3] );
			}
			else if ( statusIndex == 6 )
			{
				objsToGrow[3].SendMessage( "Grow", 0.0f );
			}
			else if ( statusIndex == 7 )
			{
				Application.LoadLevel( 1 );
			}
		}
	}


}
