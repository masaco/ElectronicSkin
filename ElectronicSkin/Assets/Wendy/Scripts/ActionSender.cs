using UnityEngine;
using System.Collections;
using SimpleJSON;

public class ActionSender : MonoBehaviour {

	public RabbitMQSenderServer sender;
	public Transform[] sendingPoints;
	private string messageToShow;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		/*
		 	format:
		 	{
  				"id":"user1",
  				"data":{
  					""
  				}
		 	}
		 */
		if( !sender.IsReady() ) return;

		JSONClass json = new JSONClass();
		messageToShow = "";

		for( int i=0; i< sendingPoints.Length; i++ )
		{
			string pointName = sendingPoints[i].name;
			Vector3 pointValues = sendingPoints[i].transform.rotation.eulerAngles;

			json[ pointName ][ "x" ].AsFloat = pointValues.x;
			json[ pointName ][ "y" ].AsFloat = pointValues.y;
			json[ pointName ][ "z" ].AsFloat = pointValues.z;

			messageToShow += pointName + " : ( " + pointValues.x + "," + pointValues.y + "," + pointValues.z + " ) \n";
		}

		sender.SendMessageToServer( json.ToString() );
		//sender.SendMessageToServer("\"id\":\"\",\"data\":{\"rightHand\":{ \"x\":0,\"y\":0,\"z\":0 },\"leftHand\":{ \"x\":0, \"y\":0, \"z\":0 },\"postion\":{\"x\":0.0, \"y\":0.0, \"z\":0.0 }}");
	}


}
