using UnityEngine;
using System.Collections;

public class IDManager : MonoBehaviour {

	public GameObject MQSelfID;
	public GameObject MQColorID;

	public GameObject[] Player;

	public int colorIDP1;
	public int colorIDP2;

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
		colorIDP1 = RabbitMQColorMapper.colorIDP1;
		colorIDP2 = RabbitMQColorMapper.colorIDP2;
//		Debug.Log("selfID = " +roadSelfID.selfID);
		if(roadSelfID.selfID == 1)
		{
			Debug.Log("ColorIDP1 = " +colorIDP1);
			colorControlPlayer.ChangeBody(colorIDP1);

			if (Player.Length > 1)colorControlOtherP.ChangeBody(colorIDP2);
		}
		else if (roadSelfID.selfID == 2)
		{
			colorControlPlayer.ChangeBody(colorIDP2);
			if (Player.Length > 1)colorControlOtherP.ChangeBody(colorIDP1);
		}

	
	}
}
