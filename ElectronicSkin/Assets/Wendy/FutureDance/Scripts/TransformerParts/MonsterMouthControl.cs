using UnityEngine;
using System.Collections;

public class MonsterMouthControl : MonoBehaviour {

	// map y pos to openValue
	public GameObject upperTeeth;
	public Vector2 upperMappingRange;

	public GameObject bottomTeeth;
	public Vector2 bottomMappingRange;

	// 0 to 1
	public float openValue = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 upperFrom = upperTeeth.transform.localPosition;
		Vector3 upperTo = upperTeeth.transform.localPosition;
		upperFrom.y = upperMappingRange.x;
		upperTo.y = upperMappingRange.y;

		upperTeeth.transform.localPosition = Vector3.Slerp ( upperFrom, upperTo, openValue );


		Vector3 bottomFrom = bottomTeeth.transform.localPosition;
		Vector3 bottomTo = bottomTeeth.transform.localPosition;
		bottomFrom.y = bottomMappingRange.x;
		bottomTo.y = bottomMappingRange.y;
		
		bottomTeeth.transform.localPosition = Vector3.Slerp ( bottomFrom, bottomTo, openValue );
	}
}
