using UnityEngine;
using System.Collections;
using SimpleJSON;

public class SendWholeSkeletonData : MonoBehaviour {

	public RabbitMQSenderServer sender;
	public Transform[] sendingPoints;
	private string messageToShow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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

		if (Input.GetMouseButtonDown(0))sender.SendMessageToServer(  "{\"id\":\"id1\",\"data\":{\"rightHand\":{\"action\":{\"vibrate\":{\"duration\":1000,\"interval\":1,\"times\":1}}}}}");
	}

	void OnGUI ()
	{
		GUILayout.Label( messageToShow );
	}
}
