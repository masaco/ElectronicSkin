using UnityEngine;
using System.Collections;

public class scene2_elephantGrow : MonoBehaviour {

	int statusIndex = 0;
	public GameObject elephant;

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
				// elephant grow
				elephant.SendMessage( "Grow", 0.65f );

			}
			else if ( statusIndex == 2 )
			{
				Application.LoadLevel( 3 );
			}
		}
	}
}
