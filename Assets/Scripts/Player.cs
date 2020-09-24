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
	public float speed = 10.0f; // in m/s.
	public KeyCode boostKey = KeyCode.LeftShift;
	public float boostFactor = 2.0f;
	public float rotationSpeed = 120.0f; // in deg/s.
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

		// We can override Unity Inspector parameters to be sure to get the good behaviour.
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
			_angle = rotationSpeed;
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			_angle = -rotationSpeed;
		}

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
		// Boost
		if (Input.GetKey(boostKey))
		{
			_move *= boostFactor;
		}

		if (!PhysicsEnabled)
		{
			transform.Rotate(Vector3.up, _angle * Time.deltaTime, Space.Self);
			//transform.Rotate(transform.up, _angle * Time.deltaTime, Space.World); // Do the same
			//transform.Rotate(transform.InverseTransformDirection(transform.up), _angle * Time.deltaTime, Space.Self); // Do the same
			transform.Translate(_move * Time.deltaTime, Space.Self);
			//Debug.Log("Speed : " + _ComputeSpeed(transform.position, Time.deltaTime));
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
		// RIGID BODIES SHOULD BE UPDATED IN FixedUpdate() !
		if (PhysicsEnabled)
		{
			_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.AngleAxis(_angle * Time.fixedDeltaTime, Vector3.up));
			_rigidbody.MovePosition(_rigidbody.position + (_rigidbody.rotation * _move * Time.fixedDeltaTime));
			//Debug.Log("Speed : " + _ComputeSpeed(_rigidbody.position, Time.fixedDeltaTime));
		}
	}

	private float _ComputeSpeed(Vector3 pNewPos, float pDt)
	{
		_lastPos = _currentPos;
		_currentPos = pNewPos;
		_velocity = (_currentPos - _lastPos) / pDt;
		return _velocity.magnitude;
	}
}
