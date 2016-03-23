using UnityEngine;
using System.Collections;
using SimpleJSON;

public class ColliderManager : MonoBehaviour {
	
	public GameObject roadIDObj;
	private string[] touchPart;
	private string selfID ;
	public RabbitMQSenderServer sender;
	private string messageToShow;

	// Use this for initialization
	void Start () {

		MQMapperManager roadID = roadIDObj.GetComponent<MQMapperManager>();
		selfID = roadID.selfID.ToString();	
	}

	// Update is called once per frame
	void Update () {
		
		/*
		 	format:
		 	{
  				"id":"user1",
  				"data":
  				{
  					"骨架關節":
  					{
  						"action":
  						{
  							"vibrate":
  							{
								"duration":1000,
								"interval":0,
								"times":1
  							}
  						}
  					}
  				}
		 	}
		 */
		if( !sender.IsReady() ) return;

//		JSONClass actionData = new JSONClass();
		string actionData;
		messageToShow = "";

		actionData = "\"id\":\"user" + selfID + "\",\"data\":{\" "+ touchPart[1] +" \":{\"action\":{\"vibrate\":{\"duration\":1000,\"interval\":0,\"times\":1}}}}";

//		Debug.Log("actionData" + actionData);
		sender.SendMessageToServer(actionData);
		//sender.SendMessageToServer("\"id\":\"\",\"data\":{\"rightHand\":{ \"x\":0,\"y\":0,\"z\":0 },\"leftHand\":{ \"x\":0, \"y\":0, \"z\":0 },\"postion\":{\"x\":0.0, \"y\":0.0, \"z\":0.0 }}");
	}

	//轉換第一個字母成小寫
	string toLowerFirstChar(string s)
	{
		if(s != string.Empty && char.IsUpper(s[0]))
		{
			s = char.ToLower(s[0]) + s.Substring(1);
		}
		return(s);
	}


	void OnTriggerEnter(Collider target)
	{
		string targetName = target.name;
		if (target.tag == "CollisionBody")
		{
//			Debug.Log("touch objName = " + target.name);
			touchPart = gameObject.name.Split('_');

			touchPart[1] = toLowerFirstChar(touchPart[1]);
//			Debug.Log(touchPart[1]);

		}
	}


}
