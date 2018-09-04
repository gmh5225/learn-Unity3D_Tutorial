using UnityEngine;

public class PhysicallizedPlayer : MonoBehaviour
{
	public float speed = 30.0f; // in m / s.
	public float rotationSpeed = 360.0f; // in deg/s.
	public float animationSpeedScale = 0.1f;

	private float _angle;
	private Vector3 _move;

	private Rigidbody _rigidbody;
	private Animator _animator;

	public bool PhysicsEnabled { get { return (_rigidbody != null) && !_rigidbody.isKinematic; } }

	private Vector3 _lastPos = Vector3.zero;
	private Vector3 _currentPos = Vector3.zero;
	private Vector3 _velocity = Vector3.zero;

	private Vector2 _lastGroundPos = Vector2.zero;
	private Vector2 _currentGroundPos = Vector2.zero;
	private Vector2 _groundVelocity = Vector2.zero;

	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();

		// We can override Unity Inspector parameters to be sure to get the good behavior.
		if (_rigidbody != null)
		{
			_rigidbody.useGravity = true;
			_rigidbody.isKinematic = false;
			_rigidbody.freezeRotation = true;
		}
	}

	void Update ()
	{
		// Rotation
		_angle = 0.0f;
        if (Input.GetKey(KeyCode.RightArrow))
		{
			_angle = rotationSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			_angle = -rotationSpeed * Time.deltaTime;
		}
		transform.Rotate(transform.up, _angle, Space.World);
		//transform.Rotate(transform.InverseTransformDirection(transform.up), _angle, Space.Self); // Do the same

		// Translation
		_move = Vector3.zero;
		if (Input.GetKey(KeyCode.UpArrow))
		{
			_move.Set(0.0f, 0.0f, speed);
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			_move.Set(0.0f, 0.0f, -speed);
		}

		if (!PhysicsEnabled)
		{
			//Debug.Log("Speed : " + ComputeSpeed(Time.deltaTime));
			transform.Translate(_move * Time.deltaTime, Space.Self);
		}

		if (_animator != null)
		{
			_lastGroundPos = _currentGroundPos;
			_currentGroundPos.Set(transform.position.x, transform.position.z);
			_groundVelocity = (_currentGroundPos - _lastGroundPos) / Time.deltaTime;
			float groundSpeed = _groundVelocity.magnitude;
			//Debug.Log("Ground Speed : " + groundSpeed);
			_animator.speed = groundSpeed * animationSpeedScale;
		}
	}

	void FixedUpdate()
	{
		// ALL RIGID BODIES SHOULD BE UPDATED IN FixedUpdate() !
		if (PhysicsEnabled)
		{
			//Debug.Log("Speed : " + ComputeSpeed(Time.fixedDeltaTime));
			_rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(_move * Time.fixedDeltaTime));
		}
	}

	private float ComputeSpeed(float dt)
	{
		_lastPos = _currentPos;
		_currentPos = transform.position;
		_velocity = (_currentPos - _lastPos) / dt;
		return _velocity.magnitude;
	}
}
