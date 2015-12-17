using UnityEngine;
using System.Collections;

public class HookInOrder : MonoBehaviour {

	public GameObject[] objsToHook;
	private int nowIndex = 0;

	// Use this for initialization
	void Start () {
	
		for( int i=0; i< objsToHook.Length; i++ )
		{
			objsToHook[i].transform.localScale = new Vector3( 0.0f, 0.0f, 0.0f );
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			StartCoroutine( "Grow", nowIndex );
			nowIndex++;
		}

		if (Input.GetKeyDown (KeyCode.Backspace))
		{
			StartCoroutine( "Shrink", nowIndex );
			nowIndex--;
		}

		if (nowIndex < 0)
				nowIndex = 0;

		if (nowIndex >= objsToHook.Length)
				nowIndex = objsToHook.Length - 1;
	}

	IEnumerator Grow ( int index )
	{
		if( index < objsToHook.Length )
		{
			for( int i=0; i< 60; i++ )
			{
				objsToHook[index].transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * ( (float)i/60.0f*0.1f );
				yield return new WaitForSeconds( 0.03f );
			}
			objsToHook[index].transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f )*0.1f;
			objsToHook[index].SendMessage ("HookOn");
		}
	}

	IEnumerator Shrink ( int index )
	{
		if( index < objsToHook.Length )
		{
			for( int i=0; i< 60; i++ )
			{
				objsToHook[index].transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f ) * ( (float)(60-i)/60.0f*0.1f );
				yield return new WaitForSeconds( 0.03f );
			}
			objsToHook[index].transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f )*0.0f;
			objsToHook[index].SendMessage ("HookOff");
		}
		
		
	}
}
