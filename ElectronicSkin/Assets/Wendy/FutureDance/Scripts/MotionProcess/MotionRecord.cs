using UnityEngine;
using System.Collections;

public class MotionRecord {

	public Vector3[] positions;
	public Vector3[] localPositions;
	public Quaternion[] rotations;
	public Quaternion[] localRotations;
	public Quaternion[] rotationDiffs;
	public Quaternion[] localRotationDiffs;

	public MotionRecord ( Transform[] targetTransform )
	{
		SetRecordsFromTransform( targetTransform );
	}

	public MotionRecord ( TargetSkeletonContainer container )
	{
		SetRecordsFromContainer (container);
	}

	public void SetRecordsFromTransform ( Transform[] targetTransform )
	{
		positions = new Vector3[ targetTransform.Length ];
		localPositions = new Vector3[ targetTransform.Length ];
		rotations = new Quaternion[ targetTransform.Length ];
		localRotations = new Quaternion[ targetTransform.Length ];
		rotationDiffs = new Quaternion[ targetTransform.Length ];
		localRotationDiffs = new Quaternion[ targetTransform.Length ];

		for( int i=0; i< targetTransform.Length; i++ )
		{
			if (targetTransform[i] == null)
			{
				continue;
			}

			positions[i] = targetTransform[i].position;
			localPositions[i] = targetTransform[i].localPosition;
			rotations[i] = targetTransform[i].rotation;
			localRotations[i] = targetTransform[i].localRotation;
		}
	}

	public void SetRecordsFromContainer ( TargetSkeletonContainer container )
	{
		int dataLength = container.SkeletonArray.Length;
		positions = new Vector3[ dataLength ];
		localPositions = new Vector3[ dataLength ];
		rotations = new Quaternion[ dataLength ];
		localRotations = new Quaternion[ dataLength ];
		rotationDiffs = new Quaternion[ dataLength ];
		localRotationDiffs = new Quaternion[ dataLength ];


		for( int i=0; i< dataLength; i++ )
		{
			positions[i] = container.GetPosition(i);
			localPositions[i] = container.GetLocalPosition(i);
			rotations[i] = container.GetRotation(i);
			localRotations[i] = container.GetLocalRotation(i);
			rotationDiffs[i] = container.GetRotationDiff(i);
			localRotationDiffs[i] = container.GetLocalRotationDiff(i);
		}
	}
}
