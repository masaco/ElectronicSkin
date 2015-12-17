using UnityEngine;
using System.Collections;
using SimpleJSON;

public class RotationCopyManager : MonoBehaviour {

	// use to save file
	public string settingName = "defaultName";

	public bool showButton = false;
	public bool loadSettingsOnStart = false;

	// get from child
	private RotationCopier[] copiers;

	private string savePath = "Assets/MocapSettings/";

	// Use this for initialization
	IEnumerator Start () {

		// delay, wait for mapper first
		yield return new WaitForSeconds (0.5f);

		Component[] copiersInChildren = GetComponentsInChildren<RotationCopier>();
		copiers = new RotationCopier[ copiersInChildren.Length ];

		for( int i=0; i< copiersInChildren.Length; i++ )
			copiers[i] = (RotationCopier)copiersInChildren[i];
	
		if( loadSettingsOnStart )
			LoadSavedStatus ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {

		if( !showButton ) return;

		GUILayout.BeginArea( new Rect( Screen.width * 0.5f, Screen.height * 0.05f, Screen.width * 0.4f, Screen.height * 0.4f ) );
		if( GUILayout.Button( "Save Copier Status" ) )
		{
			SaveCopierStatus ();
		}

		if( GUILayout.Button( "Load Copier Status" ) )
		{
			LoadSavedStatus ();
		}

		GUILayout.EndArea();

	}


	void SaveCopierStatus ()
	{
		// check folder exists
		if( !System.IO.Directory.Exists( savePath ) )
		{
			System.IO.Directory.CreateDirectory( savePath );
		}
		
		JSONClass json = new JSONClass();
		
		for( int i=0; i< copiers.Length; i++ )
		{
			string indexName = copiers[i].name;
			
			json[ indexName ][ "x" ].AsFloat = copiers[i].gameObject.transform.rotation.x;
			json[ indexName ][ "y" ].AsFloat = copiers[i].gameObject.transform.rotation.y;
			json[ indexName ][ "z" ].AsFloat = copiers[i].gameObject.transform.rotation.z;
			json[ indexName ][ "w" ].AsFloat = copiers[i].gameObject.transform.rotation.w;

			// scale
			json[ indexName ][ "sx" ].AsFloat = copiers[i].gameObject.transform.localScale.x;
			json[ indexName ][ "sy" ].AsFloat = copiers[i].gameObject.transform.localScale.y;
			json[ indexName ][ "sz" ].AsFloat = copiers[i].gameObject.transform.localScale.z;

			json[ indexName ][ "copytype" ].AsInt = (int)copiers[i].copyType;

		}
		
		json.SaveToFile( savePath + settingName + ".cop" );
		Debug.Log( "Copier Status Saved" );
	}
	
	
	void LoadSavedStatus ()
	{
		// check file exists
		if( !System.IO.File.Exists( savePath + settingName + ".cop" ) )
		{
			Debug.Log( "File Not Exists : Please check the SettingName" );
		}
		
		
		JSONNode json = JSONNode.LoadFromFile( savePath + settingName + ".cop" );
		
		for( int i=0; i< copiers.Length; i++ )
		{
			string indexName = copiers[i].name;
			
			Quaternion rot = new Quaternion();
			rot.x = json[ indexName ][ "x" ].AsFloat;
			rot.y = json[ indexName ][ "y" ].AsFloat;
			rot.z = json[ indexName ][ "z" ].AsFloat;
			rot.w = json[ indexName ][ "w" ].AsFloat;

			Vector3 scale = new Vector3();
			scale.x = json[ indexName ][ "sx" ].AsFloat;
			scale.y = json[ indexName ][ "sy" ].AsFloat;
			scale.z = json[ indexName ][ "sz" ].AsFloat;

			copiers[i].copyType = (MocapMapper.CopyType) json[ indexName ][ "copytype" ].AsInt;
			copiers[i].gameObject.transform.rotation = rot;
			copiers[i].gameObject.transform.localScale = scale;
		}
	}

}
