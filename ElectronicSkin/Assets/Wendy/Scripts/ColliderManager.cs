using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Threading;

public class ColliderManager : MonoBehaviour {

	public GameObject roadIDObj;
	private string[] touchPart;
	private string selfID ;
	public RabbitMQSenderServer sender;

	//碰撞部位的ID
	[System.NonSerialized]
	public string colliderID;

	private float vibrateInterval = 50f;
	private float vibrateTimer = 0f;
	private bool isReady;

	// Use this for initialization
	void Start () {

		if (roadIDObj == null)
			return;

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
//		if( !sender.IsReady() ) return;

//		JSONClass actionData = new JSONClass();

//		string actionData;

//		actionData = "[{\"id\":\"user" + selfID + "\",\"data\":{\""+ touchPart[1] +"\":{\"action\":{\"vibrate\":{\"duration\":500,\"interval\":0,\"times\":1}}}}}]";

		//暫停50ms
//		Thread.Sleep(50);

//		Debug.Log("actionData" + actionData);
//		sender.SendMessageToServer(actionData);
		//sender.SendMessageToServer("\"id\":\"\",\"data\":{\"rightHand\":{ \"x\":0,\"y\":0,\"z\":0 },\"leftHand\":{ \"x\":0, \"y\":0, \"z\":0 },\"postion\":{\"x\":0.0, \"y\":0.0, \"z\":0.0 }}");
		if (vibrateTimer > 0f)
			vibrateTimer -= Time.deltaTime;
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
		if (roadIDObj == null)
			return;
		
		//過濾不必要碰撞的物件(自己,Untagged)
		if (target.name.Contains(colliderID) || target.tag.Contains("Untagged"))
		{
			return;
		}

		if (target.tag == "CollisionBody")
		{
//			if ( vibrateTimer > 0f ) return;

//			Debug.Log("touch objName = " + target.name);
			touchPart = gameObject.name.Split('_');

			touchPart[1] = toLowerFirstChar(touchPart[1]);

//			Debug.Log("touchPart:"+touchPart[1]);
			string actionData;
			actionData = "[{\"id\":\"user" + selfID + "\",\"data\":{\""+ touchPart[1] +"\":{\"action\":{\"vibrate\":{\"duration\":500,\"interval\":500,\"times\":1}}}}}]";
			sender.SendMessageToServer(actionData);
			vibrateTimer = vibrateInterval * 0.001f; //轉成毫秒
//			Debug.Log(actionData);

		}
	}


}
