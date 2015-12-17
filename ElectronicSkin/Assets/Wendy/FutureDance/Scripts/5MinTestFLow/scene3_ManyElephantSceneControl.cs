using UnityEngine;
using System.Collections;

public class scene3_ManyElephantSceneControl : MonoBehaviour {

	public GameObject camRotatePoint;
	public GameObject[] elephants;
	public GameObject followPointController;
	public int statusIndex = 0;

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
				// camera leaving
				camRotatePoint.SendMessage( "RotateXTo", 12.0f );
			}
			else if ( statusIndex == 2 )
			{
				// elephants come in
				for( int i=0; i< elephants.Length; i++ )
					elephants[i].SendMessage( "Go" );
			}
			else if ( statusIndex == 3 )
			{
				// play curve
				followPointController.SendMessage( "PlayCurve" );
			}
			else if ( statusIndex == 4 )
			{
				// elephants dissappear
				for( int i=0; i< elephants.Length; i++ )
					elephants[i].SendMessage( "Grow", 0.0f );
			}
			else if( statusIndex == 5 )
			{
				// camera leaving
				camRotatePoint.SendMessage( "RotateXTo", 0.0f );
			}
			else if( statusIndex == 6 )
			{
				Application.LoadLevel( 4 );
			}
		}
	}
}
