using UnityEngine;
using System.Collections;
using SimpleJSON;

public class ClickSender : MonoBehaviour {

	public static string serverIP = "192.168.2.102";
	public GameObject RabbitMQServer;
//	public RabbitMQSenderServer sender;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	//	Debug.DrawRay();

		if (Input.GetMouseButtonDown(0)) 
		{ 
		

			if (Physics.Raycast(ray, out hit))
			{
//				Debug.Log(hit.collider.name);
				if(hit.collider.name == "Button")
				{
					string s ;
					s = "{\"id\":\"id1\",\"data\":{\"rightHand\":{\"action\":{\"vibrate\":{\"duration\":1000,\"interval\":1,\"times\":1}}}}}";
					RabbitMQServer.SendMessage( "SendMessageToServer", s);
					Debug.Log ("In Raycast" );


//					if( !sender.IsReady() ) return;
					
	//				JSONClass json = new JSONClass();
	//				json.Add("id" ,);
//					json = "{\"id\":\"id1\",\"data\":{\"rightHand\":{\"action\":{\"vibrate\":{\"duration\":1000,\"interval\":1,\"times\":1}}}}}";
//					messageToShow = "";
					
/*					for( int i=0; i< sendingPoints.Length; i++ )
					{
						string pointName = sendingPoints[i].name;
						Vector3 pointValues = sendingPoints[i].transform.rotation.eulerAngles;
						
						json[ pointName ][ "x" ].AsFloat = pointValues.x;
						json[ pointName ][ "y" ].AsFloat = pointValues.y;
						json[ pointName ][ "z" ].AsFloat = pointValues.z;
						
						messageToShow += pointName + " : ( " + pointValues.x + "," + pointValues.y + "," + pointValues.z + " ) \n";
					}*/
					
//					sender.SendMessageToServer( json.ToString() );
				}
			}
		}
	}
}
