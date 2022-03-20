/******************************************************************************************************************************************************
* MIT License																																		  *
*																																					  *
* Copyright (c) 2022																																  *
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

#define CC_CORRECTION

using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerTest : MonoBehaviour
{
	// In Update(), we need to multiply speeds with Time.deltaTime to get the quantity of rotation/translation to apply each frame.
	// As deltaTime (inverse of framerate) varies regarding the amount of computation to do each frame, this way we are sure to apply
	// the right quantity of rotation/translation to maintain constant speeds no matter the current framerate.
	public float translationSpeed = 8f; // in m/s
	public float rotationSpeed = 120f; // in deg/s

	// Exercise 1: Make the main camera follow the character continuously.
	// Exercise 2: Make the character moving using keys Z(or W)/S/Q(or A)/D instead of arrow keys.
	// Exercise 3: Allow the character to run using the Left Shift Key.
	//			   Create a variable "runFactor" to speed up the character's translation when running.
	// Exercise 4: Allow the character to move on any terrain topologies with holes and slopes.
	//			   https://docs.unity3d.com/Manual/class-CharacterController.html
	// Exercise 5: Make the character always stick to the ground : you need to apply some gravity.
	//             Be careful to not apply gravity infinitely while the character is grounded.
	// Exercise 6: Make the character more or less stick to the ground.
	//			   Create a variable "stickToGround".
	// Exercise 7: Allow the character to jump using the Space key.
	//			   Create a variable "jumpHeight" to set the character's maximum jump height.
	//			   Compute vertical velocity using the "jumpHeight" variable and kinematics equation : Vf² = Vi² + 2*a*h
	// Exercise 8: Cancel the jump movement when the Space key is released.
	// Exercise 9: Horizontal kinematic trajectory of the jump movement should continue after takeoff (=inertia).
	//			   Use the translation speed at the moment of the jump as initial ground velocity.
	//			   Now you should :
	//			   - control the character using velocity only
	//			   - slow down the horizontal velocity continuously while grounded : you can create a variable "brakeForce" for this purpose.
	// Exercise 10: Slow down the movement of the character while in the air.
	//              Create a variable "airControl" to apply a gain to the translation.
	//              Create a variable "airAngularControl" to apply a gain to the rotation.
	// Exercise 11: Allow the character to move using either a keyboard or a Gamepad
	//			   https://docs.unity3d.com/Manual/class-InputManager.html
	// Exercise 12: Allow the use of a Mouse to control character yaw rotation and the Camera pitch.
	//				- Mouse Y axis => control camera pitch : constraint pitch values to a comfortable range for the gameplay
	//				- Mouse X axis => control character yaw : constraint yaw values to a comfortable range for the gameplay
	//				The yaw rotation should be controllable also with the second stick of a gamepad
	//				Create a variable "cameraControl" to enable/disable the control of the camera.
	// Exercise 13: Also the mouse cursor to disappear when "cameraControl" is true, and to appear when "cameraControl" is false.
	//				Don't need to check "cameraControl" every frames : find the most suitable Monobehaviour method to put this code.
	// Exercise 14: When "cameraControl" is true, the character can also make lateral movements (along X axis)
	//				When "cameraControl" is false, the character can only make forward/backward movements (along Z axis)
	// Exercise 15: Allow the use of the scrollwheel to control camera distance from character
	//				- constraint camera.position.z to a comfortable range for the gameplay
	//		        - constraint camera.position.y to a comfortable range for the gameplay
	//				Camera distance should be also controllable with a gamepad
	void Update()
	{
#if CC_CORRECTION
		_CC_Update();
#else
		_Update();
#endif
	}

	private void _Update()
	{
		// Rotation
		float yawRotation = 0f;
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			// Rotate to the left (=negative rotation) on the Y axis.
			yawRotation = -rotationSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			// Rotate to the right (=positive rotation) on the Y axis.
			yawRotation = rotationSpeed * Time.deltaTime;
		}
		// Apply rotation to the Y axis of the local reference frame (Space.Self) of the gameObject.
		transform.Rotate(0f, yawRotation, 0f, Space.Self);
		// The code below do the same as the Rotate() method.
		//Vector3 euler = transform.eulerAngles;
		//euler.y += yawRotation;
		//transform.eulerAngles = euler;

		// Translation
		float zTranslation = 0f;
		if (Input.GetKey(KeyCode.UpArrow))
		{
			// Translate forward (=positive direction) along the Z axis.
			zTranslation = translationSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			// Translate backward (=negative direction) along the Z axis.
			zTranslation = -translationSpeed * Time.deltaTime;
		}
		// Apply translation along the Z axis of the local reference frame (Space.Self) of the gameObject.
		transform.Translate(0f, 0f, zTranslation, Space.Self);
		// The code below do the same as the Translate() method.
		//Vector3 pos = transform.position;
		//pos += transform.forward * zTranslation; // == transform.rotation * new Vector3(0f, 0f, zTranslation)
		//transform.position = pos;
	}

#if CC_CORRECTION
	[Tooltip("translationSpeed multiplier")]
	public float runFactor = 2f;
	[Range(0f, 1f)]
	[Tooltip("How much the character will stick to the ground")]
	public float stickToGround = 0.5f;
	[Range(0f, 1f)]
	[Tooltip("How much the character ground speed will slow down while the character is grounded")]
	public float groundFriction = 0.5f;
	[Tooltip("Character's max jump height")]
	public float jumpHeight = 2f;
	[Tooltip("multipler applied to the control of character's movement while the character is in the air")]
	public float airControl = 0.01f;
	[Tooltip("multipler applied to the control of character's rotation while the character is in the air")]
	public float airAngularControl = 0.4f;
	[Tooltip("The mass of the object to interact with others RigidBodies")]
	public float mass = 10f;
	[Tooltip("Allow to push RigidBodies below the character controller")]
	public bool pushObjectsBelow = false;

	private Transform _transform;
	private CharacterController _cc;
	private Vector3 _velocity = Vector3.zero;
	private bool _isJumping = false;
	private float _jumpVerticalSpeed = 0f;

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		//Debug.Log("OnControllerColliderHit : " + hit.gameObject.name);
		Rigidbody rb = hit.rigidbody;
		if ((rb != null) && !rb.isKinematic)
		{
			// Avoid pushing objects below us
			if ((!pushObjectsBelow) && (hit.moveDirection.y < -0.3f))
			{
				return;
			}

			// Apply force
			Vector3 force = _velocity * mass * Time.deltaTime;
			force.y = 0f; // Push only in X/Z directions.
			rb.AddForceAtPosition(force, hit.point, ForceMode.Impulse);
			//rb.AddForce(force, ForceMode.Impulse);
			//Debug.Log(string.Format("Rigidbody {0} pushed with force {1}", hit.gameObject.name, force.magnitude));
		}
	}

	private void Awake()
	{
		// calling transform directly is like doing GetComponent<Transform>() which is costly.
		// Better to cache the transform reference in the script.
		_transform = transform;

		// Exercise 1:
		Transform cameraTransform = Camera.main.transform; // This is also costly (== GameObject.FindGameObjectWithTag("MainCamera").transform)
		cameraTransform.parent = _transform; // As a child of this transform, the camera movement is following automatically this transform.
		cameraTransform.localPosition = new Vector3(0f, 4f, -10f); // Set the camera a little higher than the character, and a little behind.
		cameraTransform.localEulerAngles = new Vector3(15f, 0f, 0f); // Pitch the camera a little towards the ground.

		// Excercise 4:
		// Add a CharacterController, but first destroy any existing Collider
		// Because CharacterController is a Collider and we don't need 2 colliders.
		Collider collider = GetComponent<Collider>();
		if (collider != null)
		{
			Destroy(collider);
		}
		_cc = gameObject.AddComponent<CharacterController>();
	}

	private void _CC_Update()
	{
		float gravity = Physics.gravity.y;
		float dt = Time.deltaTime;
		bool isGrounded = _cc.isGrounded;

		// Exercise 11 : use of Input.GetAxis() & Input.GetButton(), instead of Input.GetKey().
		// GET ROTATION
		float yawAngularSpeed = Input.GetAxisRaw("Horizontal") * rotationSpeed;

		// GET TRANSLATION
		float maxGroundSpeed = translationSpeed;
		float zSpeed = Input.GetAxisRaw("Vertical");
		if (Input.GetButton("Run"))
		{
			maxGroundSpeed *= runFactor;
		}
		zSpeed *= maxGroundSpeed;

		// JUMP & GRAVITY
		float minFallSpeed = gravity * 0.1f; // value when stickToGround = 0. gravity * dt is not enough for the CharacterController.isGrounded to be accurate.
		float maxFallSpeed = gravity; // value when stickToGround = 1
		float brakeSpeed = 0f;
		if (isGrounded)
		{
			// Exercise 5: Make the character always stick to the ground. 
			//_velocity.y = -Mathf.Clamp(-_velocity.y, -minFallSpeed, -maxFallSpeed);

			// Exercise 6: Make the character more or less stick to the ground.
			_velocity.y = -Mathf.Lerp(-minFallSpeed, -maxFallSpeed, stickToGround);
			// breakAmount when grounded.
			brakeSpeed = groundFriction * translationSpeed;

			if (_isJumping)
			{
				_isJumping = false;
			}

			if (Input.GetButtonDown("Jump"))
			{
				// Kinematic equation : Vf² = Vi² + 2*a*h
				// When max height (h) is reached : Vf is 0
				// => 0 = Vi² + 2*a*h
				// => Vi² = -2*a*h
				// => Vi = sqrt(-2*a*h)
				// Exercise 7 : use only vertical velocity as initial velocity
				_jumpVerticalSpeed = Mathf.Sqrt(-2f * gravity * jumpHeight);
				//_velocity = Vector3.up * _jumpVerticalSpeed;

				// Exercise 9 : use ground + vertical velocities as initial velocity
				_velocity = _transform.forward * zSpeed + Vector3.up * _jumpVerticalSpeed;
				_isJumping = true;
				//Debug.Log("Jump");
			}
		}
		// Normal fall speed caused by gravity.
		float fallSpeed = gravity * dt;
		if (_isJumping)
		{
			// Exercise 8 : jump movement is cancelled if the Jump button is released while maxHeight is not reached (_velocity.y > 0f).
			if ((_velocity.y > 0f) && Input.GetButtonUp("Jump"))
			{
				// To cancel the jump movement, we apply a fall velocity.
				// if vertical speed > 0 (=max height not reached) => apply normal fall speed
				// if vertical speed == _jumpVerticalSpeed (=on the ground just before takeoff) => apply max fall speed
				// Linearly interpolates between the 2 values to apply more "fall speed" when close to the ground, and less "fall speed" while maintaining the jump button pressed.
				fallSpeed = -Mathf.Lerp(-fallSpeed, -maxFallSpeed, _velocity.y / _jumpVerticalSpeed);
				//Debug.Log("Jump cancelled");
			}
		}
		// When character is in the air
		if (!isGrounded)
		{
			// Exercise 5 : add gravity.
			_velocity.y += fallSpeed;
			// Exercise 10 : character is less controllable.
			yawAngularSpeed *= airAngularControl;
			zSpeed *= airControl;
		}

		// APPLY ROTATION
		_transform.Rotate(0f, yawAngularSpeed * dt, 0f);

		// APPLY TRANSLATION
		Vector3 groundVelocity = _transform.forward * zSpeed;
		// Exercise 9 : Control ground velocity
		float y = _velocity.y; // store y velocity
		_velocity.y = 0f; // ground velocity is in X/Z plane only.
						  // Add ground velocity to the current velocity.
		_velocity += groundVelocity;
		float groundSpeed = _velocity.magnitude;
		// Slowdown & Clamp the new ground velocity only if > 0
		// Otherwise this operation is useless + it will generate an error (_velocity / 0f).
		if (groundSpeed > 0f)
		{
			// Normalize ground velocity + multiply by the new speed which is:
			// - slowdowned with brakeSpeed
			// - clamped in [0; maxSpeed]
			_velocity = _velocity / groundSpeed * Mathf.Clamp(groundSpeed - brakeSpeed, 0f, maxGroundSpeed);
		}
		// reset y velocity after working on ground velocity only (X/Z).
		_velocity.y = y;
		_cc.Move(_velocity * dt);
		// Exercise 4 :
		//_cc.Move(_velocity * dt + _transform.forward * zSpeed * dt);

#if UNITY_EDITOR
		// Debug.
		Debug.DrawRay(_transform.position, _velocity, Color.red);
		Debug.DrawRay(_transform.position, groundVelocity, Color.green);
		//Debug.Log("Is Grounded : " + isGrounded);
		//Debug.Log("Velocity : " + _velocity);
		//Debug.Log("Ground Velocity : " + groundVelocity);
#endif
	}
#endif
}
