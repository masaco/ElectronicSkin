using UnityEngine;
using System.Collections;

public class SeclectSender : MonoBehaviour {

	public GameObject roadIDObj;
	private string selfID ;
	public RabbitMQSenderServer sender;

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
  				"colorID":"1"
		 	}
		 */
		if(selfID == "1")
		{
			ColorDataSender(RabbitMQColorMapper.colorIDP1);
		}
		else if(selfID == "2" )
		{
			ColorDataSender(RabbitMQColorMapper.colorIDP2);
		}


	}

	public void ColorDataSender(int colorID)
	{
		if (roadIDObj == null) return;

		string colorData;
		colorData = "{\"id\":\""+ selfID + "\",\"color\":\"" + colorID +"\"}";
		sender.SendMessageToServer(colorData);

		Debug.Log("colorData = " + colorData );
	}
		

}
