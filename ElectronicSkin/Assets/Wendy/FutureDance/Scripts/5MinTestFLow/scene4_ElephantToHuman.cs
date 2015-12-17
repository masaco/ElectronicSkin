using UnityEngine;
using System.Collections;

public class scene4_ElephantToHuman : MonoBehaviour {

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
				elephant.SendMessage( "Grow", 0.0f );
				
			}
			else if ( statusIndex == 2 )
			{
				Application.LoadLevel( 3 );
			}
		}
	}
}
