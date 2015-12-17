﻿using UnityEngine;


[AddComponentMenu("Dynamic Bone/Dynamic Bone Collider")]
public class DynamicBoneCollider : MonoBehaviour 
{
	public Vector3 m_Center = Vector3.zero;
	public float m_Radius = 0.5f;
	public float m_Height = 0;
	
	public enum Direction 
	{
		X, Y, Z
	}
	public Direction m_Direction = Direction.X;
	
	void OnValidate()
	{
		m_Radius = Mathf.Max(m_Radius, 0);
		m_Height = Mathf.Max(m_Height, 0);
	}
	
	public void Collide(ref Vector3 particlePosition, float particleRadius)
	{
		float radius = m_Radius * transform.lossyScale.x;
		float h = m_Height * 0.5f - radius;
		if (h <= 0)
		{
			CollideSphere(ref particlePosition, particleRadius, transform.TransformPoint(m_Center), radius);
		}
		else
		{
			Vector3 c0 = m_Center;
			Vector3 c1 = m_Center;
			
			switch(m_Direction)
			{
			case Direction.X:
				c0.x -= h;
				c1.x += h;
				break;
			case Direction.Y:
				c0.y -= h;
				c1.y += h;
				break;
			case Direction.Z:
				c0.z -= h;
				c1.z += h;
				break;												
			}
			CollideCapsule(ref particlePosition, particleRadius, transform.TransformPoint(c0), transform.TransformPoint(c1), radius);					
		}	
	}
	
	static void CollideSphere(ref Vector3 particlePosition, float particleRadius, Vector3 sphereCenter, float sphereRadius)
	{
		float r = sphereRadius + particleRadius;
		float r2 = r * r;
		Vector3 d = particlePosition - sphereCenter;
		float len2 = d.sqrMagnitude;
		
		// if is inside sphere, project onto sphere surface
		if (len2 > 0 && len2 < r2)
		{
			float len = Mathf.Sqrt(len2);
			particlePosition = sphereCenter + d * (r / len);
		}
	}
	
	static void CollideCapsule(ref Vector3 particlePosition, float particleRadius, Vector3 capsuleP0, Vector3 capsuleP1, float capsuleRadius)
	{
		float r = capsuleRadius + particleRadius;
		float r2 = r * r;
		Vector3 dir = capsuleP1 - capsuleP0;
		Vector3 d = particlePosition - capsuleP0;
		float t = Vector3.Dot(d, dir);
		
		if (t <= 0)
		{
			// check sphere1
			float len2 = d.sqrMagnitude;
			if (len2 > 0 && len2 < r2)
			{
				float len = Mathf.Sqrt(len2);
				particlePosition = capsuleP0 + d * (r / len);
			}
		} 
		else
		{
			float dl = dir.sqrMagnitude;
			if (t >= dl)
			{
				// check sphere2
				d = particlePosition - capsuleP1;
				float len2 = d.sqrMagnitude;
				if (len2 > 0 && len2 < r2)
				{
					float len = Mathf.Sqrt(len2);
					particlePosition = capsuleP1 + d * (r / len);
				}
			} 
			else if (dl > 0)
			{
				// check cylinder
				t /= dl;
				d -= dir * t;
				float len2 = d.sqrMagnitude;
				if (len2 > 0 && len2 < r2)
				{
					float len = Mathf.Sqrt(len2);
					particlePosition += d * ((r - len) / len);
				}
			}
		}
	}
	
	void OnDrawGizmosSelected()
	{
		if (!enabled)
			return;

		Gizmos.color = Color.yellow;
		float radius = m_Radius * transform.lossyScale.x;
		float h = m_Height * 0.5f - radius;
		if (h <= 0)
		{
			Gizmos.DrawWireSphere(transform.TransformPoint(m_Center), radius);
		}
		else
		{
			Vector3 c0 = m_Center;
			Vector3 c1 = m_Center;
			
			switch(m_Direction)
			{
			case Direction.X:
				c0.x -= h;
				c1.x += h;
				break;
			case Direction.Y:
				c0.y -= h;
				c1.y += h;
				break;
			case Direction.Z:
				c0.z -= h;
				c1.z += h;
				break;												
			}
			Gizmos.DrawWireSphere(transform.TransformPoint(c0), radius);
			Gizmos.DrawWireSphere(transform.TransformPoint(c1), radius);				
		}	
	}
}
