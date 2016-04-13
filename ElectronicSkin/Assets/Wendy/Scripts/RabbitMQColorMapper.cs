using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NccuWise;
using SimpleJSON;

public class RabbitMQColorMapper : MonoBehaviour {

	public static int colorIDP1 = 0;
	public static int colorIDP2 = 0;


	public RabbitMQServer rabbitServer;

	private Vector3 preV3;
	// all saved messages
	private List<string> messages;
	public int messageBufferedLimit = 5;

	// Use this for initialization
	void Start () {
		messages = new List<string>();

	}

	// Update is called once per frame
	void Update () {

		SaveNewMessages ();
		//		Debug.Log("messages :"+ messages);

		// apply server data
		if( messages.Count > 0 )
		{
			string msg = messages[0];
			messages.RemoveAt(0);
			LoadJsonRots( msg );
			//			Debug.Log (msg.ToString());
		}
	}

	void LoadJsonRots ( string jsonString )
	{

		// convert pure string to json node
		JSONNode nodesAll = JSON.Parse( jsonString );

		//format: {"id":"1"//1~2,"color":"0" //0~6}


		// if convert failed : not json format
		if( nodesAll == null)
		{
			Debug.Log( "OOps convert fail : not json format" );
			return;
		}
			
		if (nodesAll["id"].AsInt == 1)
		{
			colorIDP1 = nodesAll["color"].AsInt;
		}

		else if (nodesAll["id"].AsInt == 2)
		{
			colorIDP2 = nodesAll["color"].AsInt;
		}
		else
		{
			Debug.LogError("colorID userID 格式不正確");
		}

	}


	void SaveNewMessages ()
	{
		List<string> newMessages = rabbitServer.GetNewMessages();

		// if server haven't started yet
		if( newMessages == null )
			return;

		for( int i=0; i< newMessages.Count; i++ )
		{
			// if meet limit, dont save

			if( messages.Count < messageBufferedLimit )
				messages.Add( newMessages[i] );
			//			Debug.Log("mocap :" + messages);
		}
	}
		
	// recursive call
	Transform FindChildNested ( Transform obj, string searchName )
	{
		Transform result = null;
		string lowerSearchName = searchName.ToLower();

		// lower find
		for( int i=0; i< obj.childCount; i++ )
		{
			if( obj.GetChild(i).name.ToLower() == lowerSearchName )
				result = obj.GetChild(i);
		}



		if( result != null )
		{
			return result;
		}
		else if( obj.childCount == 0 )
		{
			return null;
		}
		else
		{
			for( int i=0; i< obj.childCount; i++ )
			{
				result = FindChildNested( obj.GetChild(i), searchName );

				if( result != null )
					break;
			}

			return result;
		}
	}
}
