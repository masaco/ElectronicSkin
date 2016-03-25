using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NccuWise;

using SimpleJSON;

public class MQMapperManager : MonoBehaviour {
//	public GameObject[] ModelObjAll;
//	public Transform[] HipsAll;
	public int selfID ;
//	public int selfColorID = 2;
	private int otherID;
//	private int otherColorID = 2;

	public RabbitMQServer rabbitServer;
	public GameObject[] skeletonObj;
	public Transform[] skeletonRoot; // use for positions
	public GameObject[] root;

	private MQMapperItem[][] mappers;


	// all saved messages
	private List<string> messages;
	public int messageBufferedLimit = 5;

	void Awake ()
	{
//		mappers = new MQMapperItem[skeletonObj.Length][];
		mappers = new MQMapperItem[2][];

		for(int i = 0; i < mappers.Length ; i++)
		{
			mappers[i] = root[i].GetComponentsInChildren<MQMapperItem>();
		}
			
	}

	// Use this for initialization
	void Start () {
		messages = new List<string>();

		//如果自己是1 對方就是2
		otherID = (selfID == 1)?2:1;

		// find transforms in skeletonObj
		ResetSource ();

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

		// 資料格式 "dataSet":[{"id":"1","data":{關節資料}}]
		JSONNode nodesDataSet = nodesAll["dataSet"]; //存dataSet裡面的陣列資料

		// if convert failed : not json format
		if( nodesAll == null || nodesDataSet == null)
		{
			Debug.Log( "OOps convert fail : not json format" );
			return;
		}

		// 讀取wise的id值
		//PlayerIDWise = int.Parse(nodes[0]["id"]) ;  
		// 資料格式 "dataSet":[{"id":"user1","data":{關節資料}}]


		// search is there data for skeletonNodes
		for( int i=0; i< mappers.Length; i++ )
		{
			for (int j= 0 ; j<mappers[i].Length; j++)
			{
				string indexName = mappers[i][j].name;

				if( nodesDataSet[i]["data"][ indexName ] != null )
				{
					Vector3 newRotation = new Vector3( nodesDataSet[i]["data"][ indexName ]["x"].AsFloat, 
						nodesDataSet[i]["data"][ indexName ]["y"].AsFloat, 
						nodesDataSet[i]["data"][ indexName ]["z"].AsFloat );
					mappers[i][j].ApplyJsonRot( newRotation );
				}

			}

		}

		// check position
		if( nodesDataSet[0]["data"][ "Position" ] != null )
		{
			for (int i =0 ; i <skeletonRoot.Length; i++)
			{
				string indexName = "Position";
				Vector3 newPos = new Vector3( nodesDataSet[i]["data"][ indexName ]["x"].AsFloat, 
					nodesDataSet[i]["data"][ indexName ]["y"].AsFloat, 
					nodesDataSet[i]["data"][ indexName ]["z"].AsFloat );
				skeletonRoot[i].position = newPos;
			}

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

	void ResetSource ()
	{
		// get scripts from children
		Component[][] comps = new Component[mappers.Length][];
			
		for (int i = 0; i< mappers.Length ; i++)
		{
			comps[i] = root[i].GetComponentsInChildren<MQMapperItem>();

		}

		for( int i=0; i< 1; i++ )
		{
			for( int j=0; j < comps[i].Length; j++)
			{
				mappers[i][j] = (MQMapperItem)comps[i][j];
				//Debug.Log( mappers[i][j].name );
			}

		}


		// search for objs by name in source
		for( int i=0; i< mappers.Length; i++ )
		{
			//Debug.Log ("mappers[i].Length:"+mappers[i].Length);
			for( int j =0 ; j < mappers[i].Length;j++)
			{
				Transform tempSource = FindChildNested( skeletonObj[i].transform, mappers[i][j].name );

				if( tempSource != null )
					mappers[i][j].monitorTransform = tempSource;

			}

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
