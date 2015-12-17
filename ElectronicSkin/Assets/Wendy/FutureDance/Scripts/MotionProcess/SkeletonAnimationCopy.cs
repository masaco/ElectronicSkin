using UnityEngine;
using System.Collections;

public class SkeletonAnimationCopy : MonoBehaviour {

	public TargetSkeletonContainer target;
	
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

	private Transform[] SkeletonArray; // make coding easy

	// use to calculate
	private Vector3[] OriginalPositions;
	private Vector3[] OriginalLocalPositions;
	private Quaternion[] OriginalRotations;
	private Quaternion[] OriginalLocalRotations;


	private ArrayList motionRecords = new ArrayList();
	public bool applyRoot = false;

	public Vector3 positionBais;
	public Vector3 rotationBais;
	public int delayedFrames = 0;

	// use add, not change the value
	public bool addOneAbsoluteRotation = false;

	public bool useLocalRotation = false;
	public bool useAbsoluteRotation = false;
	public bool useLocalPosition = false;
	public bool useAbsolutePosition = false;



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

		// put position and rotations in
		MotionRecord record = new MotionRecord( target );
		motionRecords.Add( record );

		// use for delay
		if( motionRecords.Count > delayedFrames )
		{
			ApplySkeletonAnimation( (MotionRecord)motionRecords[ 0 ] );
			motionRecords.RemoveAt( 0 );

			// if still greater, means delayed value may have changed
			// so keep kill records
			if( motionRecords.Count > delayedFrames )
			{
				motionRecords.RemoveAt(0);
			}
		}
	}

	void ApplySkeletonAnimation ( MotionRecord record ) {

		for( int i=0; i< record.positions.Length; i++ )
		{
			if( record.positions[i] == null ) continue;

			// obj root
			/*if( i==0 )
			{
				if( applyRoot )
				{
					SkeletonArray[i].position = positionBais + record.positions[i];
					SkeletonArray[i].rotation = record.rotations[i];
				}
				continue;
			}*/

			if( SkeletonArray[i] == null ) continue;

			// choose witch method
			if( addOneAbsoluteRotation )
				SkeletonArray[i].rotation = record.rotationDiffs[i] * OriginalRotations[i];

			if( useLocalRotation )
				SkeletonArray[i].localRotation = record.localRotations[i];
			else if( useAbsoluteRotation )
				SkeletonArray[i].rotation = record.rotations[i];

			if( useLocalPosition )
				SkeletonArray[i].localPosition = record.localPositions[i];
			else if( useAbsolutePosition )
				SkeletonArray[i].position = record.positions[i];

			//SkeletonArray[i].rotation = Quaternion.Slerp( SkeletonArray[i].rotation, record.rotations[i], 1.0f);
			//SkeletonArray[i].localPosition = OriginalLocalPositions[i] + record.localPositions[i] + positionBais;
			//SkeletonArray[i].localRotation = Quaternion.Euler( record.localRotations[i].eulerAngles + rotationBais );
			//SkeletonArray[i].rotation = Quaternion.Euler( OriginalRotations[i].eulerAngles + record.rotations[i].eulerAngles );
		}
	}

	void SetDelay ( int newDelay ) {
		delayedFrames = newDelay;
	}
}
