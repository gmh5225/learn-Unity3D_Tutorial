/******************************************************************************************************************************************************
* MIT License																																		  *
*																																					  *
* Copyright (c) 2020																																  *
* Emmanuel Badier <emmanuel.badier@gmail.com>																										  *
* 																																					  *
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),  *
* to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,  *
* and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:		  *
* 																																					  *
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.					  *
* 																																					  *
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, *
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 																							  *
* IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 		  *
* TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.							  *
******************************************************************************************************************************************************/

using UnityEngine;

public class Player : MonoBehaviour
{
	public const string H_AXIS = "Horizontal";
	public const string V_AXIS = "Vertical";
	public const string MOUSE_X = "Mouse X";
	public const string MOUSE_Y = "Mouse Y";
	public const string MOUSE_SW = "Mouse ScrollWheel";
	public const string JUMP = "Jump";
	public const string MOVE = "Move";
	public const string RUN = "Run";

	public bool IsGrounded { get { return _isGrounded; } }
	public bool IsJumping { get { return _isJumping; } }
	public bool IsRunning { get { return _isRunning; } }
	public float TranslationSpeed { get; private set; }
	public float RotationSpeed { get; private set; }

	[Header("Camera")]
	// Ajust Mouse sensivity X/Y & ScrollWheel directly in ProjectSettings.Input : 0.5 is a good value.
	public bool useMouse = true;
	public Transform cameraTransform;
	public float cameraPitchMin = -17f;
	public float cameraPitchMax = 23f;
	public float cameraDistanceMin = 10f;
	public float cameraDistanceMax = 30f;
	public float cameraHeightMin = 6f;
	public float cameraHeightMax = 15f;
	[Header("Player")]
	public float rotationSpeed = 270f; // in deg/s.
	public float translationSpeed = 10f; // in m/s.
	public float runFactor = 2f;
	public float airFactor = 0.4f;
	public float jumpHeight = 2.5f;
	public bool stickToGround = true;
	public float animationSpeedScale = 0.2f;
	public float rotAnimationSpeedScale = 0.01f;
	//public bool animationUseGroundSpeed = true;
	//public float groundCheckDistance = 0.3f;

	//private Rigidbody _rigidbody;
	private Transform _transform;
	private CharacterController _controller;
	private Animator _animator;
	private Footsteps _footsteps;
	private TrailRenderer[] _trails;

	private float _cameraPitch = 0f;
	private Vector3 _cameraLocalPos = Vector3.zero;

	private float _yaw = 0f;
	private Vector3 _move = Vector3.zero;
	private Vector3 _velocity = Vector3.zero;
	private bool _isGrounded = true;
	private bool _isJumping = false;
	private bool _isRunning = false;
	//private int _groundLayerMask;
	//private Vector3 _jumpForce = Vector3.zero;


	private Vector3 _lastPos = Vector3.zero;
	private Vector3 _currentPos = Vector3.zero;
	private Vector3 _computedVelocity = Vector3.zero;

	private Vector2 _lastGroundPos = Vector2.zero;
	private Vector2 _currentGroundPos = Vector2.zero;
	private Vector2 _computedGroundVelocity = Vector2.zero;

	private float _lastYaw = 0f;
	private float _currentYaw = 0f;
	private float _computedRotationSpeed = 0f;

	void Awake()
	{
		//_groundLayerMask = 1 << LayerMask.NameToLayer("Ground"); // same as : LayerMask.GetMask("Ground");
		_transform = GetComponent<Transform>();
		_controller = GetComponent<CharacterController>();
		_animator = GetComponentInChildren<Animator>();
		_footsteps = GetComponentInChildren<Footsteps>();
		_trails = GetComponentsInChildren<TrailRenderer>();

		// We can override Unity Inspector parameters to be sure to get the good behaviour.
		//_rigidbody = GetComponent<Rigidbody>();
		//if (_rigidbody != null)
		//{
		//	_rigidbody.useGravity = true;
		//	_rigidbody.isKinematic = false;
		//	_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ; // same as : _rigidbody.freezeRotation = true;
		//}
	}

	void OnEnable()
	{
		if(_animator != null)
		{
			_animator.enabled = true;
		}
	}

	void OnDisable()
	{
		if (_animator != null)
		{
			_animator.enabled = false;
		}
	}

	void Update ()
	{
		float dt = Time.deltaTime;
		float gravity = Physics.gravity.y;
		_isGrounded = _controller.isGrounded;
		//_isGrounded = Physics.CheckSphere(_transform.position, groundCheckDistance, _groundLayerMask, QueryTriggerInteraction.Ignore);
		//Debug.Log("Is grounded : " + _isGrounded);

		// GET INPUTS
		// Rotation
		if (useMouse)
		{
			_yaw = Input.GetAxis(MOUSE_X);
			_cameraPitch = -Input.GetAxis(MOUSE_Y);

			// Mouse wheel controls camera Distance/Height
			float mouseWheel = -Input.GetAxis(MOUSE_SW);
			_cameraLocalPos = cameraTransform.localPosition;
			// Adjust camera Distance
			float distance = Mathf.Clamp(Mathf.Abs(_cameraLocalPos.z) + mouseWheel, cameraDistanceMin, cameraDistanceMax);
			_cameraLocalPos.z = -distance;
			// Adjust camera Height relative to distance.
			_cameraLocalPos.y = _Scale(distance, cameraDistanceMin, cameraDistanceMax, cameraHeightMin, cameraHeightMax);
			cameraTransform.localPosition = _cameraLocalPos;
		}
		else
		{
			_yaw = rotationSpeed * Input.GetAxis(H_AXIS);
		}

		// V1
		//_angle = 0.0f;
		//if (Input.GetKey(KeyCode.RightArrow))
		//{
		//	_angle = rotationSpeed;
		//}
		//else if (Input.GetKey(KeyCode.LeftArrow))
		//{
		//	_angle = -rotationSpeed;
		//}

		// Translation
		_move = Vector3.zero;
		if(useMouse)
		{
			// strafe
			_move.Set(Input.GetAxis(H_AXIS), 0f, Input.GetAxis(V_AXIS));
			_move.Normalize(); // Apply speed to the X/Z direction
			_move *= translationSpeed;
		}
		else
		{
			// forward only
			_move.Set(0f, 0f, translationSpeed * Input.GetAxis(V_AXIS));  // Apply speed directly to Z direction
		}

		// V1
		//if (Input.GetKey(KeyCode.UpArrow))
		//{
		//	_move.Set(0.0f, 0.0f, speed);
		//}
		//else if (Input.GetKey(KeyCode.DownArrow))
		//{
		//	_move.Set(0.0f, 0.0f, -speed);
		//}

		// Run : accelerate translation.
		_isRunning = Input.GetButton(RUN);
		if (_isRunning)
		{
			_move *= runFactor;
		}

		// Slows down translation & rotation while jumping.
		if(_isJumping)
		{
			_move *= airFactor;
			_yaw *= airFactor;
			_cameraPitch *= airFactor;
		}

		// APPLY MOTION
		if(useMouse)
		{
			// Player Yaw
			_transform.Rotate(Vector3.up, _yaw, Space.Self);
			// Camera Pitch
			Vector3 euler = cameraTransform.localEulerAngles;
			euler.x = _ClampAngle(euler.x + _cameraPitch, cameraPitchMin, cameraPitchMax);
			cameraTransform.localEulerAngles = euler;
		}
		else
		{
			_transform.Rotate(Vector3.up, _yaw * dt, Space.Self);
			//_transform.rotation *= Quaternion.Euler(0f, _yaw * dt, 0f); // Do the same
			//_transform.Rotate(_transform.up, _yaw * dt, Space.World); // Do the same
			//_transform.Rotate(_transform.InverseTransformDirection(_transform.up), _yaw * dt, Space.Self); // Do the same
		}
		_controller.Move(_transform.rotation * _move * dt);
		//_transform.Translate(_move * dt, Space.Self);

		// APPLY JUMP & GRAVITY
		if (_isGrounded)
		{
			if(!stickToGround)
			{
				_velocity.y = 0f;
			}
			
			if (_isJumping)
			{
				if (_animator != null)
				{
					_animator.SetBool(JUMP, false);
				}
				_isJumping = false;
				_footsteps.PlayLand();
			}
			else
			{
				if (Input.GetButtonDown(JUMP))
				{
					if (_animator != null)
					{
						_animator.SetBool(JUMP, true);
					}
					_isJumping = true;
					// TODO : explain this.
					_velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
					//Debug.Log("Jump");
					_footsteps.PlayJump();
				}
			}
		}
		_velocity.y += gravity * dt;
		_controller.Move(_velocity * dt);

		// COMPUTE SPEEDS
		TranslationSpeed = _ComputeGroundSpeed(_transform.position, dt);
		RotationSpeed = Mathf.Abs(_ComputeRotationSpeed(_transform.localEulerAngles.y, dt));
		bool translate = (_move != Vector3.zero); // more reactive than checking TranslationSpeed > 0f.
		bool rotate = (_yaw != 0f); // more reactive than checking RotationSpeed > 0f.
		bool move = _isGrounded && (translate || rotate);
		float speed = 1f;
		if (move)
		{
			speed = Mathf.Max(TranslationSpeed * animationSpeedScale, RotationSpeed * rotAnimationSpeedScale);
			if(translate)
			{
				// FOOTSTEPS (CapsulePlayer Only)
				_footsteps.UpdateSlidingFootStep(speed);
			}
		}

		// ANIMATE
		if (_animator != null)
		{
			_animator.SetBool(MOVE, move);	
			_animator.speed = speed; //Mathf.Lerp(_animator.speed, speed, 0.5f);
		}

		// VFX
		_trails[0].emitting = _trails[1].emitting = _isJumping || (_isGrounded && _isRunning);

		//Debug.Log("Is Grounded : " + _isGrounded);
		//Debug.Log("Animator speed : " + _animator.speed);
	}

	//void FixedUpdate()
	//{
	//	// APPLY MOTION : RIGID BODIES SHOULD BE UPDATED IN FixedUpdate() !
	//	if (PhysicsEnabled)
	//	{
	//		_rigidbody.rotation = _rigidbody.rotation * Quaternion.AngleAxis(_angle * Time.fixedDeltaTime, Vector3.up);
	//		_rigidbody.position = _rigidbody.position + (_rigidbody.rotation * _move * Time.fixedDeltaTime);
	//		//_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.AngleAxis(_angle * Time.fixedDeltaTime, Vector3.up));
	//		//_rigidbody.MovePosition(_rigidbody.position + (_rigidbody.rotation * _move * Time.fixedDeltaTime));			
	//		//Debug.Log("Speed : " + _ComputeSpeed(_rigidbody.position, Time.fixedDeltaTime));
	//	}
	//}

	private float _ComputeRotationSpeed(float pYaw, float pDt)
	{
		_lastYaw = _currentYaw;
		_currentYaw = pYaw;
		_computedRotationSpeed = Mathf.DeltaAngle(_currentYaw, _lastYaw) / pDt;
		return _computedRotationSpeed;
	}

	private float _ComputeGroundSpeed(Vector3 pNewPos, float pDt)
	{
		_lastGroundPos = _currentGroundPos;
		_currentGroundPos.Set(pNewPos.x, pNewPos.z);
		_computedGroundVelocity = (_currentGroundPos - _lastGroundPos) / pDt;
		return _computedGroundVelocity.magnitude;
	}

	private float _ComputeSpeed(Vector3 pNewPos, float pDt)
	{
		_lastPos = _currentPos;
		_currentPos = pNewPos;
		_computedVelocity = (_currentPos - _lastPos) / pDt;
		return _computedVelocity.magnitude;
	}

	/// <summary>
	/// Clamp an angle in degrees.
	/// </summary>
	private float _ClampAngle(float pAngle, float pMinAngle = -180f, float pMaxAngle = 180f)
	{
		// Set angle in [-180;180]
		pAngle %= 360f;
		if (pAngle > 180f)
		{
			pAngle -= 360f;
		}
		// Clamp
		return pAngle > pMaxAngle ? pMaxAngle : pAngle < pMinAngle ? pMinAngle : pAngle;
	}

	/// <summary>
	/// Convert the given value pVal from its original range to a new range.
	/// Ex: pVal = 0 in [-1.0f; 1.0.f] returns 0.5f in [0.0f;1.0f].
	/// Be sure (pRangeMin <= pVal) && (pVal <= pRangeMax) && (pNewRangeMin < pNewRangeMax) before calling. 
	/// </summary>
	public static float _Scale(float pVal, float pRangeMin, float pRangeMax, float pNewRangeMin, float pNewRangeMax)
	{
		return (((pVal - pRangeMin) * (pNewRangeMax - pNewRangeMin)) / (pRangeMax - pRangeMin)) + pNewRangeMin;
	}
}
