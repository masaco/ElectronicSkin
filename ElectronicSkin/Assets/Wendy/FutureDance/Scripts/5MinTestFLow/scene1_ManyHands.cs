using UnityEngine;
using System.Collections;

public class scene1_ManyHands : MonoBehaviour {

	public GameObject[] hands;
	public GameObject[] handObjs;

	public int statusIndex = 0;
	public float handSize = 1.3f;

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
				// first hand grow, left and right
				hands[0].SendMessage( "Grow", handSize );
				hands[1].SendMessage( "Grow", handSize );
			}
			else if ( statusIndex == 2 )
			{
				// other hands grow
				GameObject[] otherhands = new GameObject[]{
					hands[2],
					hands[3],
					hands[4],
					hands[5],
					hands[6],
					hands[7],
					hands[8],
					hands[9]};
				StartCoroutine( "GrowInOrder", otherhands );
			}
			else if ( statusIndex == 3 )
			{
				// set delay to wings
				for( int i=0; i< handObjs.Length; i++ )
				{
					handObjs[i].SendMessage( "SetDelay", 7*(i+1) );
				}
			}
			else if ( statusIndex == 4 )
			{
				// shrink all
				StartCoroutine( "ShrinkInOrder", hands );
			}
			else if ( statusIndex == 5 )
			{
				Application.LoadLevel( 2 );
			}
		}
	}

	IEnumerator GrowInOrder ( GameObject[] objs ) {

		for( int i=0; i< objs.Length; i+=2 )
		{
			objs[i].SendMessage( "Grow", handSize );
			objs[i+1].SendMessage( "Grow", handSize );
			yield return new WaitForSeconds( 1.0f );
		}
	}

	IEnumerator ShrinkInOrder ( GameObject[] objs ) {

		for( int i=0; i< objs.Length; i+=2 )
		{
			objs[i].SendMessage( "Grow", 0.0f );
			objs[i+1].SendMessage( "Grow", 0.0f );
			yield return new WaitForSeconds( 0.5f );
		}
	}
}
