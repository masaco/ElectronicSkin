using UnityEngine;
using System.Collections;

public class TargetSkeletonContainer : MonoBehaviour {
	
	public Transform ReferenceRoot;
	public Transform Hips;
	public Transform LeftUpLeg;
	public Transform LeftLeg;
	public Transform LeftFoot;
	public Transform LeftToeBase;
	public Transform LeftToes_End;
	public Transform RightUpLeg;
	public Transform RightLeg;
	public Transform RightFoot;
	public Transform RightToeBase;
	public Transform RightToes_End;
	public Transform Spine;
	public Transform Spine1;
	public Transform Spine2;
	public Transform Spine3;
	public Transform LeftShoulder;
	public Transform LeftArm;
	public Transform LeftForeArm;
	public Transform LeftHand;
	public Transform LeftHand_End;
	public Transform RightShoulder;
	public Transform RightArm;
	public Transform RightForeArm;
	public Transform RightHand;
	public Transform RightHand_End;
	public Transform Neck;
	public Transform Head;
	public Transform Head_End;

	public Transform[] SkeletonArray; // just for make coding easy

	private Vector3[] OriginalPositions;
	private Vector3[] OriginalLocalPositions;
	private Quaternion[] OriginalRotations;
	private Quaternion[] OriginalLocalRotations;

	// Use this for initialization
	void Start () {
	
		SkeletonArray = new Transform[]{
			transform,
			ReferenceRoot,
			Hips,
			LeftUpLeg,
			LeftLeg,
			LeftFoot,
			LeftToeBase,
			LeftToes_End,
			RightUpLeg,
			RightLeg,
			RightFoot,
			RightToeBase,
			RightToes_End,
			Spine,
			Spine1,
			Spine2,
			Spine3,
			LeftShoulder,
			LeftArm,
			LeftForeArm,
			LeftHand,
			LeftHand_End,
			RightShoulder,
			RightArm,
			RightForeArm,
			RightHand,
			RightHand_End,
			Neck,
			Head,
			Head_End
		};

		OriginalRotations = new Quaternion[ SkeletonArray.Length ];
		OriginalLocalRotations = new Quaternion[ SkeletonArray.Length ];
		OriginalPositions = new Vector3[ SkeletonArray.Length ];
		OriginalLocalPositions = new Vector3[ SkeletonArray.Length ];

		for( int i=0; i< OriginalRotations.Length; i++ )
		{
			if( SkeletonArray[i] == null )
			{
				OriginalRotations[i] = Quaternion.identity;
				OriginalLocalRotations[i] = Quaternion.identity;
				OriginalPositions[i] = Vector3.zero;
				OriginalLocalPositions[i] = Vector3.zero;
				continue;
			}
			OriginalPositions[i] = SkeletonArray[i].position;
			OriginalLocalPositions[i] = SkeletonArray[i].localPosition;
			OriginalRotations[i] = SkeletonArray[i].rotation;
			OriginalLocalRotations[i] = SkeletonArray[i].localRotation;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Quaternion GetRotation ( int index )
	{
		if (SkeletonArray [index] == null)
		{
			return Quaternion.identity;
		}
		//return Quaternion.Euler( SkeletonArray[index].rotation.eulerAngles - OriginalRotations[index].eulerAngles );
		return SkeletonArray [index].rotation;
	}

	public Quaternion GetLocalRotation ( int index )
	{
		if (SkeletonArray [index] == null)
		{
			return Quaternion.identity;
		}
		//return Quaternion.Euler( SkeletonArray[index].localRotation.eulerAngles - OriginalLocalRotations[index].eulerAngles );
		return SkeletonArray [index].localRotation;
	}

	public Quaternion GetLocalRotationDiff ( int index )
	{
		if (SkeletonArray [index] == null)
		{
			return Quaternion.identity;
		}
		//return Quaternion.Euler( SkeletonArray[index].localRotation.eulerAngles - OriginalLocalRotations[index].eulerAngles );
		return SkeletonArray [index].localRotation * Quaternion.Inverse( OriginalLocalRotations[index] );
	}

	public Quaternion GetRotationDiff ( int index )
	{
		if (SkeletonArray [index] == null)
		{
			return Quaternion.identity;
		}
		//return Quaternion.Euler( SkeletonArray[index].localRotation.eulerAngles - OriginalLocalRotations[index].eulerAngles );
		return SkeletonArray [index].rotation * Quaternion.Inverse( OriginalRotations[index] );
	}

	public Vector3 GetPosition (int index)
	{
		if (SkeletonArray [index] == null)
		{
			return Vector3.zero;
		}
		return SkeletonArray[index].position;
	}

	public Vector3 GetLocalPosition (int index)
	{
		if (SkeletonArray [index] == null)
		{
			return Vector3.zero;
		}
		return SkeletonArray [index].localPosition;
	}
}
