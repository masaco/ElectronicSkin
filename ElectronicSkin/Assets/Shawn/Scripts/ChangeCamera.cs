using UnityEngine;
using System.Collections;

public class ChangeCamera : MonoBehaviour {

	public GameObject[] CamObjSelect;
	public GameObject[] CamObjPlay;

	private int selectCamNum = 0;
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetKeyDown(KeyCode.C) )
		{

			foreach( GameObject obj in CamObjSelect )
				obj.SetActive( false );
			foreach( GameObject obj in CamObjPlay )
				obj.SetActive( false );
			
			if ( selectCamNum <  CamObjSelect.Length-1)
				selectCamNum++;
			else
				selectCamNum = 0;

			CamObjSelect[selectCamNum].SetActive( true );
			CamObjPlay[selectCamNum].SetActive( true );


		}		
	}
}
