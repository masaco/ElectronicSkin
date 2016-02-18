using UnityEngine;
using System.Collections;

public class particleTest : MonoBehaviour {

	BoxCollider collider;
	Vector3 nearestPoint = Vector3.zero;
	Vector3 ddd = Vector3.zero;
	float nearestDis = 100f;
	Vector3[] RaycastDir;
	void Awake () {
		collider = GetComponent<BoxCollider>();

		int checkLength = 1;
		RaycastDir = new Vector3[ Mathf.FloorToInt( Mathf.Pow(checkLength*2+1, 3 ))];
		int count = 0;
		for (int i = -checkLength; i < (checkLength+1); i++)
		{
			for (int j = -checkLength; j < (checkLength + 1); j++)
			{
				for (int k = -checkLength; k < (checkLength + 1); k++)
				{
					RaycastDir[count] = new Vector3(i, j, k);
					count++;					
                }
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		Debug.DrawLine(transform.position, other.transform.position, Color.red);
		Vector3 boundSize = collider.bounds.size;
				
		for (int i = 0; i < RaycastDir.Length; i++)
		{
			RaycastHit hit;
			Vector3 tempDir = transform.TransformPoint(RaycastDir[i]);
			Vector3 scaleDir = new Vector3(tempDir.x * boundSize.x, tempDir.y * boundSize.y, tempDir.z * boundSize.z);
            Vector3 dir = scaleDir - transform.position;
			Vector3 offsetOriginPoint = transform.position - dir * 0.3f;

			Debug.DrawLine(offsetOriginPoint, scaleDir, Color.blue);			

			if (Physics.Raycast(offsetOriginPoint, dir, out hit, 10f))
			{
				if (nearestDis > hit.distance)
				{
					nearestDis = hit.distance;
					nearestPoint = hit.point;
					ddd = offsetOriginPoint;
                }			
			}
		}

		if (nearestPoint != Vector3.zero)
			Debug.DrawLine(ddd, nearestPoint, Color.yellow);
		else
			Debug.Log( 0 );
	}
}
