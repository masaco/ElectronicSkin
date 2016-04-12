using UnityEngine;
using System.Collections;

public class IDManager : MonoBehaviour {

	public GameObject MQSelfID;
	public GameObject MQColorID;

	public GameObject Player;
	public GameObject OtherPlayer;

	private MQMapperManager roadSelfID;
	private RabbitMQColorMapper roadColorID;

	private MainEffectControl colorControlPlayer;
	private MainEffectControl colorControlOtherP;



	// Use this for initialization
	void Start () {

		roadSelfID = MQSelfID.GetComponent<MQMapperManager>();
		roadColorID = MQColorID.GetComponent<RabbitMQColorMapper>();

		colorControlPlayer = Player.GetComponent<MainEffectControl>();
		colorControlOtherP = OtherPlayer.GetComponent<MainEffectControl>();

	}
	
	// Update is called once per frame
	void Update () {

		if(roadSelfID.selfID == 1)
		{
			colorControlPlayer.ChangeBody(roadColorID.colorIDP1);
			colorControlOtherP.ChangeBody(roadColorID.colorIDP2);
		}
		else if (roadSelfID.selfID == 2)
		{
			colorControlPlayer.ChangeBody(roadColorID.colorIDP2);
			colorControlOtherP.ChangeBody(roadColorID.colorIDP1);
		}

	
	}
}
