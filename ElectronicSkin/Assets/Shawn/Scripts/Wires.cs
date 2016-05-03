using UnityEngine;
using System.Collections;

public class Wires : MonoBehaviour {
	private MeshRenderer meshR ;
	private Color meshColor;
	private float addValue;
	// Use this for initialization
	void Start () {
		meshR = GetComponent<MeshRenderer> ();
		meshColor = meshR.material.GetColor ("_TintColor");
		meshR.material.SetColor ( "_TintColor", new Color( meshColor.r, meshColor.g, meshColor.b, 0f) );
	}
	
	void OnTriggerStay ( Collider other )
	{
		if (other.tag.Contains ("CollisionBody")) {
			if (!other.transform.root.name.Contains ("OtherPlayer")) {
				addValue += Time.deltaTime * 2;
				addValue = Mathf.Clamp01 (addValue);
			}
		}
	}

	void Update ()
	{
		addValue -= Time.deltaTime;
		addValue = Mathf.Clamp01 (addValue);
		meshR.material.SetColor ( "_TintColor", Color.Lerp(  new Color( meshColor.r, meshColor.g, meshColor.b, 0f ) ,
			new Color( meshColor.r, meshColor.g, meshColor.b, 127f/255f ), addValue ) );
	}
}
