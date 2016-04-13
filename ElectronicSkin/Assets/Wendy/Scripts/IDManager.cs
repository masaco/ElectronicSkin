using UnityEngine;
using System.Collections;

public class IDManager : MonoBehaviour {

	public GameObject MQSelfID;
	public GameObject MQColorID;

	public GameObject[] Player;

	private MQMapperManager roadSelfID;

	private MainEffectControl colorControlPlayer;
	private MainEffectControl colorControlOtherP;



	// Use this for initialization
	void Start () {

		roadSelfID = MQSelfID.GetComponent<MQMapperManager>();

		colorControlPlayer = Player[0].GetComponent<MainEffectControl>();
		if (Player.Length > 1)colorControlOtherP = Player[1].GetComponent<MainEffectControl>();

	}
	
	// Update is called once per frame
	void Update () {

		if(roadSelfID.selfID == 1)
		{
			colorControlPlayer.ChangeBody(RabbitMQColorMapper.colorIDP1);
			if (Player.Length > 1)colorControlOtherP.ChangeBody(RabbitMQColorMapper.colorIDP2);
		}
		else if (roadSelfID.selfID == 2)
		{
			colorControlPlayer.ChangeBody(RabbitMQColorMapper.colorIDP2);
			if (Player.Length > 1)colorControlOtherP.ChangeBody(RabbitMQColorMapper.colorIDP1);
		}

	
	}
}
