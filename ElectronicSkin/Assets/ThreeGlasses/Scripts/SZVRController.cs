using UnityEngine;
using System.Collections;

enum MovementTransferOnJump 
{
	None,
	InitTransfer,
	PermaTransfer,
	PermaLocked
}

[SerializeField]
class MovingPlatform
{
	public bool enabled = true;
	public MovementTransferOnJump movementTransfer = MovementTransferOnJump.PermaTransfer;
	public Transform hitPlatform;
	public Transform activePlatform;
	public Vector3 activeLocalPoint;
	public Vector3 activeGlobalPoint;
	public Quaternion activeLocalRotation;
	public Quaternion activeGlobalRotation;
	public Matrix4x4 lastMatrix;
	public Vector3 platformVelocity;
	public bool newPlatform;
}

[SerializeField]
class PlayerMovement 
{
	public float maxForwardSpeed = 10.0f;
	public float maxSidewaysSpeed = 10.0f;
	public float maxBackwardsSpeed = 10.0f;
	public AnimationCurve slopeSpeedMultiplier = new AnimationCurve(new Keyframe(-90,1), new Keyframe(0, 1), new Keyframe(90, 0));
	public float maxGroundAcceleration = 30.0f;
	public float maxAirAcceleration = 20.0f;
	public float gravity = 10.0f;
	public float maxFallSpeed = 20.0f;
	public CollisionFlags collisionFlags; 
	public Vector3 velocity;
	public Vector3 frameVelocity = Vector3.zero;
	public Vector3 hitPoint = Vector3.zero;
	public Vector3 lastHitPoint = new Vector3(Mathf.Infinity, 0, 0);
}

[SerializeField]
class Jumping 
{
	public bool enabled = true;
	public float baseHeight = 1.0f;
	public float extraHeight = 4.1f;
	public float perpAmount = 0.0f;
	public float steepPerpAmount = 0.5f;
	public bool jumping = false;
	public bool holdingJumpButton = false;
	public float lastStartTime = 0.0f;
	public float lastButtonDownTime = -100.0f;
	public Vector3 jumpDir = Vector3.up;
}

[SerializeField]
class Sliding 
{
	public bool enabled = true;
	public float slidingSpeed = 15.0f;
	public float sidewaysControl = 1.0f;
	public float speedControl = 0.4f;
}

[RequireComponent(typeof(CharacterController))]
public class SZVRController : MonoBehaviour 
{
	public bool canControl = true;
	public bool useFixedUpdate = true;
	public bool useHmdControllerDirection = true;

	public float moveSpeed = 10.0f;

	private bool inputJump = false;
	private Vector3 inputMoveDirection = Vector3.zero;
	private bool grounded = true;
	private Vector3 groundNormal = Vector3.zero;
	public static Transform tr;

	private CharacterController controller;

	private Sliding sliding;
	private PlayerMovement movement;
	private MovingPlatform movingPlatform;
	private Jumping jumping;

	private SZVRDevice device;

	private Vector3 lastGroundNormal = Vector3.zero;

	void Start ()
	{
		tr = transform;
		controller = gameObject.GetComponent<CharacterController>();
		device = gameObject.GetComponentInChildren<SZVRDevice>();

		movingPlatform = new MovingPlatform();
		sliding = new Sliding();
		movement = new PlayerMovement();
		movement.maxForwardSpeed = movement.maxBackwardsSpeed = movement.maxSidewaysSpeed = moveSpeed;
		jumping = new Jumping();
	}

	private bool TooSteep () 
	{
		return (groundNormal.y <= Mathf.Cos(controller.slopeLimit * Mathf.Deg2Rad));
	}

	private Vector3 GetDesiredHorizontalVelocity () 
	{
		Vector3 desiredLocalDirection = tr.InverseTransformDirection(inputMoveDirection);
		float maxSpeed = MaxSpeedInDirection(desiredLocalDirection);
		if (grounded) 
		{
			float movementSlopeAngle = Mathf.Asin(movement.velocity.normalized.y)  * Mathf.Rad2Deg;
			maxSpeed *= movement.slopeSpeedMultiplier.Evaluate(movementSlopeAngle);
		}
		return tr.TransformDirection(desiredLocalDirection * maxSpeed);
	}

	private float MaxSpeedInDirection (Vector3 desiredMovementDirection)
	{
		if (desiredMovementDirection == Vector3.zero)
			return 0;
		else 
		{
			float zAxisEllipseMultiplier = (desiredMovementDirection.z > 0 ? movement.maxForwardSpeed : movement.maxBackwardsSpeed) / movement.maxSidewaysSpeed;
			Vector3 temp = new Vector3(desiredMovementDirection.x, 0, desiredMovementDirection.z / zAxisEllipseMultiplier).normalized;
			float length = new Vector3(temp.x, 0, temp.z * zAxisEllipseMultiplier).magnitude * movement.maxSidewaysSpeed;
			return length;
		}
	}

	private Vector3 AdjustGroundVelocityToNormal (Vector3 hVelocity, Vector3 groundNormal)
	{
		Vector3 sideways = Vector3.Cross(Vector3.up, hVelocity);
		return Vector3.Cross(sideways, groundNormal).normalized * hVelocity.magnitude;
	}

	private float GetMaxAcceleration (bool grounded)
	{
		if (grounded)
			return movement.maxGroundAcceleration;
		else
			return movement.maxAirAcceleration;
	}

	private float CalculateJumpVerticalSpeed (float targetJumpHeight)
	{
		return Mathf.Sqrt (2 * targetJumpHeight * movement.gravity);
	}

	private bool MoveWithPlatform ()
	{
		return (
			movingPlatform.enabled 
			&& (grounded || movingPlatform.movementTransfer == MovementTransferOnJump.PermaLocked)
			&& movingPlatform.activePlatform != null);
	}

	private bool IsGroundedTest ()
	{
		return (groundNormal.y > 0.01f);
	}

	private Vector3 ApplyInputVelocityChange (Vector3 velocity) 
	{	
		if (!canControl)
			inputMoveDirection = Vector3.zero;
		
		Vector3 desiredVelocity = Vector3.zero;
		if (grounded && TooSteep()) 
		{
			desiredVelocity = new Vector3(groundNormal.x, 0.0f, groundNormal.z).normalized;
			Vector3 projectedMoveDir = Vector3.Project(inputMoveDirection, desiredVelocity);
			desiredVelocity = desiredVelocity + projectedMoveDir * sliding.speedControl + (inputMoveDirection - projectedMoveDir) * sliding.sidewaysControl;
			desiredVelocity *= sliding.slidingSpeed;
		}
		else
			desiredVelocity = GetDesiredHorizontalVelocity();
		
		if (movingPlatform.enabled && movingPlatform.movementTransfer == MovementTransferOnJump.PermaTransfer)
		{
			desiredVelocity += movement.frameVelocity;
			desiredVelocity.y = 0.0f;
		}
		
		if (grounded)
			desiredVelocity = AdjustGroundVelocityToNormal(desiredVelocity, groundNormal);
		else
			velocity.y = 0.0f;
		
		float maxVelocityChange = GetMaxAcceleration(grounded) * Time.deltaTime;
		Vector3 velocityChangeVector = (desiredVelocity - velocity);

		if (velocityChangeVector.sqrMagnitude > maxVelocityChange * maxVelocityChange) 
			velocityChangeVector = velocityChangeVector.normalized * maxVelocityChange;
		if (grounded || canControl)
			velocity += velocityChangeVector;
		if (grounded)
			velocity.y = Mathf.Min(velocity.y, 0.0f);
		
		return velocity;
	}

	private Vector3 ApplyGravityAndJumping (Vector3 velocity) {
		
		if (!inputJump || !canControl) {
			jumping.holdingJumpButton = false;
			jumping.lastButtonDownTime = -100.0f;
		}
		
		if (inputJump && jumping.lastButtonDownTime < 0.0f && canControl)
			jumping.lastButtonDownTime = Time.time;
		
		if (grounded)
			velocity.y = Mathf.Min(0.0f, velocity.y) - movement.gravity * Time.deltaTime;
		else 
		{
			velocity.y = movement.velocity.y - movement.gravity * Time.deltaTime;
			
			if (jumping.jumping && jumping.holdingJumpButton) 
			{
				if (Time.time < jumping.lastStartTime + jumping.extraHeight / CalculateJumpVerticalSpeed(jumping.baseHeight)) 
					velocity += jumping.jumpDir * movement.gravity * Time.deltaTime;
			}
			
			velocity.y = Mathf.Max (velocity.y, -movement.maxFallSpeed);
		}
		
		if (grounded) 
		{
			if (jumping.enabled && canControl && (Time.time - jumping.lastButtonDownTime < 0.2f)) 
			{
				grounded = false;
				jumping.jumping = true;
				jumping.lastStartTime = Time.time;
				jumping.lastButtonDownTime = -100.0f;
				jumping.holdingJumpButton = true;
				
				if (TooSteep())
					jumping.jumpDir = Vector3.Slerp(Vector3.up, groundNormal, jumping.steepPerpAmount);
				else
					jumping.jumpDir = Vector3.Slerp(Vector3.up, groundNormal, jumping.perpAmount);
				
				velocity.y = 0.0f;
				velocity += jumping.jumpDir * CalculateJumpVerticalSpeed (jumping.baseHeight);
				
				if (movingPlatform.enabled &&
				   (movingPlatform.movementTransfer == MovementTransferOnJump.InitTransfer ||
				 	movingPlatform.movementTransfer == MovementTransferOnJump.PermaTransfer)) 
				{
					movement.frameVelocity = movingPlatform.platformVelocity;
					velocity += movingPlatform.platformVelocity;
				}
				
				SendMessage("OnJump", SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				jumping.holdingJumpButton = false;
			}
		}
		
		return velocity;
	}

	private IEnumerator SubtractNewPlatformVelocity () 
	{
		if (movingPlatform.enabled &&
		   (movingPlatform.movementTransfer == MovementTransferOnJump.InitTransfer ||
			movingPlatform.movementTransfer == MovementTransferOnJump.PermaTransfer)) 
		{
			if (movingPlatform.newPlatform) 
			{
				Transform platform = movingPlatform.activePlatform;
				yield return new WaitForFixedUpdate();
				yield return new WaitForFixedUpdate();
				if (grounded && platform == movingPlatform.activePlatform)
					yield return new WaitForSeconds(1.0f);
			}
			movement.velocity -= movingPlatform.platformVelocity;
		}
	}

	private void UpdateFunction() 
	{
		float y = 0.0f;
		if(device != null)
			y = device.transform.localEulerAngles.y;

		if (useHmdControllerDirection && device != null)
			transform.localEulerAngles = new Vector3(0.0f, y+transform.localEulerAngles.y, 0.0f);
		else
			transform.Rotate(0.0f, Input.GetAxis("Mouse X") * 2.0f, 0.0f);

		Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
		
		if (directionVector != Vector3.zero)
		{
			float directionLength = directionVector.magnitude;
			directionVector = directionVector / directionLength;
			
			directionLength = Mathf.Min(1.0f, directionLength);
			directionLength = directionLength * directionLength;
			directionVector = directionVector * directionLength;
		}
		
		inputMoveDirection = transform.rotation * directionVector;
		inputJump = Input.GetButton("Jump");

		Vector3 velocity = movement.velocity;
		velocity = ApplyInputVelocityChange(velocity);
		velocity = ApplyGravityAndJumping(velocity);

		Vector3 moveDistance = Vector3.zero;

		if (MoveWithPlatform())
		{
			Vector3 newGlobalPoint = movingPlatform.activePlatform.TransformPoint(movingPlatform.activeLocalPoint);
			moveDistance = (newGlobalPoint - movingPlatform.activeGlobalPoint);
			if (moveDistance != Vector3.zero)
				controller.Move(moveDistance);
			
			Quaternion newGlobalRotation = movingPlatform.activePlatform.rotation * movingPlatform.activeLocalRotation;
			Quaternion rotationDiff = newGlobalRotation * Quaternion.Inverse(movingPlatform.activeGlobalRotation);
			
			float yRotation = rotationDiff.eulerAngles.y;
			if (yRotation != 0.0f) tr.Rotate(0.0f, yRotation, 0.0f);
		}
		
		Vector3 lastPosition = tr.position;
		
		Vector3 currentMovementOffset = velocity * Time.deltaTime;
		
		float pushDownOffset = Mathf.Max(controller.stepOffset, new Vector3(currentMovementOffset.x, 0, currentMovementOffset.z).magnitude);
		if (grounded) currentMovementOffset -= pushDownOffset * Vector3.up;
		
		movingPlatform.hitPlatform = null;
		groundNormal = Vector3.zero;
		
		movement.collisionFlags = controller.Move (currentMovementOffset);
		
		movement.lastHitPoint = movement.hitPoint;
		lastGroundNormal = groundNormal;
		
		if (movingPlatform.enabled && movingPlatform.activePlatform != movingPlatform.hitPlatform)
		{
			if (movingPlatform.hitPlatform != null) 
			{
				movingPlatform.activePlatform = movingPlatform.hitPlatform;
				movingPlatform.lastMatrix = movingPlatform.hitPlatform.localToWorldMatrix;
				movingPlatform.newPlatform = true;
			}
		}
		
		Vector3 oldHVelocity = new Vector3(velocity.x, 0.0f, velocity.z);
		movement.velocity = (tr.position - lastPosition) / Time.deltaTime;
		Vector3 newHVelocity = new Vector3(movement.velocity.x, 0.0f, movement.velocity.z);
		
		if (oldHVelocity == Vector3.zero) 
			movement.velocity = new Vector3(0.0f, movement.velocity.y, 0.0f);
		else 
		{
			float projectedNewVelocity = Vector3.Dot(newHVelocity, oldHVelocity) / oldHVelocity.sqrMagnitude;
			movement.velocity = oldHVelocity * Mathf.Clamp01(projectedNewVelocity) + movement.velocity.y * Vector3.up;
		}
		
		if (movement.velocity.y < velocity.y - 0.001f) 
		{
			if (movement.velocity.y < 0.0f) 
				movement.velocity.y = velocity.y;
			else 
				jumping.holdingJumpButton = false;
		}
		
		if (grounded && !IsGroundedTest()) 
		{
			grounded = false;
			
			if (movingPlatform.enabled &&
			   (movingPlatform.movementTransfer == MovementTransferOnJump.InitTransfer ||
			 	movingPlatform.movementTransfer == MovementTransferOnJump.PermaTransfer))
			{
				movement.frameVelocity = movingPlatform.platformVelocity;
				movement.velocity += movingPlatform.platformVelocity;
			}
			
			SendMessage("OnFall", SendMessageOptions.DontRequireReceiver);
			tr.position += pushDownOffset * Vector3.up;
		}
		else if (!grounded && IsGroundedTest()) 
		{
			grounded = true;
			jumping.jumping = false;
			SubtractNewPlatformVelocity();
			
			SendMessage("OnLand", SendMessageOptions.DontRequireReceiver);
		}
		
		if (MoveWithPlatform()) 
		{
			movingPlatform.activeGlobalPoint = tr.position + Vector3.up * (controller.center.y - controller.height*0.5f + controller.radius);
			movingPlatform.activeLocalPoint = movingPlatform.activePlatform.InverseTransformPoint(movingPlatform.activeGlobalPoint);
			
			movingPlatform.activeGlobalRotation = tr.rotation;
			movingPlatform.activeLocalRotation = Quaternion.Inverse(movingPlatform.activePlatform.rotation) * movingPlatform.activeGlobalRotation; 
		}
	}

	void FixedUpdate () 
	{
		if (movingPlatform.enabled) 
		{
			if (movingPlatform.activePlatform != null) 
			{
				if (!movingPlatform.newPlatform)
				{
					Vector3 lastVelocity = movingPlatform.platformVelocity;
					
					movingPlatform.platformVelocity = (
						movingPlatform.activePlatform.localToWorldMatrix.MultiplyPoint3x4(movingPlatform.activeLocalPoint)
						- movingPlatform.lastMatrix.MultiplyPoint3x4(movingPlatform.activeLocalPoint)
						) / Time.deltaTime;
				}
				movingPlatform.lastMatrix = movingPlatform.activePlatform.localToWorldMatrix;
				movingPlatform.newPlatform = false;
			}
			else
			{
				movingPlatform.platformVelocity = Vector3.zero;	
			}
		}
		
		if (useFixedUpdate)
			UpdateFunction();
	}

	void Update ()
	{
		if (!useFixedUpdate)
			UpdateFunction();
	}

	void OnControllerColliderHit (ControllerColliderHit hit) 
	{
		if (hit.normal.y > 0.0f && hit.normal.y > groundNormal.y && hit.moveDirection.y < 0.0f) 
		{
			if ((hit.point - movement.lastHitPoint).sqrMagnitude > 0.001f || lastGroundNormal == Vector3.zero)
				groundNormal = hit.normal;
			else
				groundNormal = lastGroundNormal;
			
			movingPlatform.hitPlatform = hit.collider.transform;
			movement.hitPoint = hit.point;
			movement.frameVelocity = Vector3.zero;
		}
	}
}
