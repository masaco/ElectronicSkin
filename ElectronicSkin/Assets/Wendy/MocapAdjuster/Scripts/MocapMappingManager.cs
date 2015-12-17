using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;

public class MocapMappingManager : MonoBehaviour {

	public string mocapName = "defaultMocap";

	public GameObject mocapSource;
	public GameObject mocapTarget;
	
	public bool showButton = true;
	public bool loadSettingOnStart = false;


	// will get from children
	private MocapMapper[] datas;

	// default path
	private string savePath = "Assets/MocapSettings/";


	// handling movement
	public bool applyPosition = false;
	private Vector3 sourcePosOriginal;
	private Vector3 targetPosOriginal;
	private bool first = true;

	// Use this for initialization
	void Start () {
	
		// get scripts from children
		Component[] comps = transform.GetComponentsInChildren<MocapMapper>();
		datas = new MocapMapper[ comps.Length ];
		for( int i=0; i< comps.Length; i++ )
		{
			datas[i] = (MocapMapper)comps[i];
		}

		// search for objs by name in source
		for( int i=0; i< datas.Length; i++ )
		{
			Transform tempSource = FindChildNested( mocapSource.transform, datas[i].name );

			if( tempSource != null )
				datas[i].monitorSource = tempSource;
		}

		// search for objs by name in target
		for( int i=0; i< datas.Length; i++ )
		{
			Transform tempTarget = FindChildNested( mocapTarget.transform, datas[i].name );

			if( tempTarget != null )
				datas[i].monitorTarget = tempTarget;
		}

		if( loadSettingOnStart )
			LoadAdjustedValues();
	}
	
	// Update is called once per frame
	void Update () {
	
		if( applyPosition )
		{
			CalculatePosition ();
		}
	}

	void OnGUI ()
	{
		if( !showButton ) return;

		GUILayout.BeginArea( new Rect( Screen.width * 0.1f, Screen.height * 0.05f, Screen.width * 0.4f, Screen.height * 0.4f ) );

		if( GUILayout.Button( "Save Rotation Status" ) )
		{
			SaveAdjustedValues();
		}

		if( GUILayout.Button( "Load Rotation Status" ) )
		{
			LoadAdjustedValues();
		}

		GUILayout.EndArea();
	}

	// recursive call
	Transform FindChildNested ( Transform obj, string searchName )
	{
		Transform result = null;
		string lowerSearchName = searchName.ToLower();

		// lower find
		for( int i=0; i< obj.childCount; i++ )
		{
			if( obj.GetChild(i).name.ToLower() == lowerSearchName )
				result = obj.GetChild(i);
		}



		if( result != null )
		{
			return result;
		}
		else if( obj.childCount == 0 )
		{
			return null;
		}
		else
		{
			for( int i=0; i< obj.childCount; i++ )
			{
				result = FindChildNested( obj.GetChild(i), searchName );

				if( result != null )
					break;
			}

			return result;
		}
	}

	void CalculatePosition ()
	{
		if( first )
		{
			sourcePosOriginal = mocapSource.transform.position;
			targetPosOriginal = mocapTarget.transform.position;
			first = false;
		}

		Vector3 diff = mocapSource.transform.position - sourcePosOriginal;
		mocapTarget.transform.position = targetPosOriginal + diff;
	}

	void SaveAdjustedValues ()
	{
		// check folder exists
		if( !System.IO.Directory.Exists( savePath ) )
		{
			System.IO.Directory.CreateDirectory( savePath );
		}

		JSONClass json = new JSONClass();

		for( int i=0; i< datas.Length; i++ )
		{
			string indexName = datas[i].name;

			json[ indexName ][ "x" ].AsFloat = datas[i].gameObject.transform.rotation.x;
			json[ indexName ][ "y" ].AsFloat = datas[i].gameObject.transform.rotation.y;
			json[ indexName ][ "z" ].AsFloat = datas[i].gameObject.transform.rotation.z;
			json[ indexName ][ "w" ].AsFloat = datas[i].gameObject.transform.rotation.w;

			json[ indexName ][ "copytype" ].AsInt = (int)datas[i].copyType;

			json[ indexName ][ "sx" ].AsFloat = datas[i].gameObject.transform.localScale.x;
			json[ indexName ][ "sy" ].AsFloat = datas[i].gameObject.transform.localScale.y;
			json[ indexName ][ "sz" ].AsFloat = datas[i].gameObject.transform.localScale.z;

			// add on rotation
			json[ indexName ][ "onX" ].AsFloat = datas[i].RotationAddOn.x;
			json[ indexName ][ "onY" ].AsFloat = datas[i].RotationAddOn.y;
			json[ indexName ][ "onZ" ].AsFloat = datas[i].RotationAddOn.z;

		}

		json.SaveToFile( savePath + mocapName + ".sav" );
	}


	void LoadAdjustedValues ()
	{
		// check file exists
		if( !System.IO.File.Exists( savePath + mocapName + ".sav" ) )
		{
			Debug.Log( "File Not Exists : Please check the MocapName" );
		}


		JSONNode json = JSONNode.LoadFromFile( savePath + mocapName + ".sav" );

		for( int i=0; i< datas.Length; i++ )
		{
			string indexName = datas[i].name;

			Quaternion rot = new Quaternion();
			rot.x = json[ indexName ][ "x" ].AsFloat;
			rot.y = json[ indexName ][ "y" ].AsFloat;
			rot.z = json[ indexName ][ "z" ].AsFloat;
			rot.w = json[ indexName ][ "w" ].AsFloat;

			Vector3 scale = new Vector3();
			scale.x = json[ indexName ][ "sx" ].AsFloat;
			scale.y = json[ indexName ][ "sy" ].AsFloat;
			scale.z = json[ indexName ][ "sz" ].AsFloat;

			Vector3 addOnRotation = new Vector3();
			addOnRotation.x = json[ indexName ][ "onX" ].AsFloat;
			addOnRotation.y = json[ indexName ][ "onY" ].AsFloat;
			addOnRotation.z = json[ indexName ][ "onZ" ].AsFloat;


			datas[i].copyType = (MocapMapper.CopyType)json[ indexName ][ "copytype" ].AsInt;
			datas[i].RotationAddOn = addOnRotation;
			datas[i].gameObject.transform.rotation = rot;
			datas[i].gameObject.transform.localScale = scale;
		}
	}
}
