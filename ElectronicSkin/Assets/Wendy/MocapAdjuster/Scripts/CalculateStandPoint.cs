using UnityEngine;
using System.Collections;

public class CalculateStandPoint : MonoBehaviour {

	public Transform referencePoint;
	public Transform HipPoint;
	public Transform[] possibleStandPoints;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {

		float minIndex = 0;
		float minValue = possibleStandPoints[0].position.y;

		for( int i=1; i< possibleStandPoints.Length; i++ )
		{
			if( possibleStandPoints[i] == null ) continue;
			if( possibleStandPoints[i].position.y < minValue )
			{
				minIndex = i;
				minValue = possibleStandPoints[i].position.y;
			}
		}

		float diffY = minValue - referencePoint.position.y;

		Vector3 newPos = HipPoint.position;
		newPos.y -= diffY;

		HipPoint.position = newPos;
	
	}
}
