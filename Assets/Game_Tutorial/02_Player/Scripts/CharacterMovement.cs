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

#define CHAR_CORRECTION

using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	// In Update(), we need to multiply speeds with Time.deltaTime to get the quantity of rotation/translation to apply each frame.
	// As deltaTime (inverse of framerate) varies regarding the amount of computation to do each frame, this way we are sure to apply
	// the right quantity of rotation/translation to maintain constant speeds no matter the current framerate.
	public float translationSpeed = 8f; // in m/s
	public float rotationSpeed = 120f; // in deg/s

	// Exercise 01: Make the main camera follow the character continuously (parenting)
	// Exercise 02: Make the character moving using keys Z(or W)/S/Q(or A)/D instead of arrow keys.
	//				You can create 4 variables of type "KeyCode" to set your keys from the UnityEditor.
	// Exercise 03: Allow the character to run using the Left Shift Key.
	//				Create a float variable "runFactor" to speed up the character's translation when the key is pressed.
	//				You can also create a KeyCode variable "runKey" to define the run key from the UnityEditor.
	// Exercise 04: Allow the character to move on any terrain topologies with holes and slopes.
	//				https://docs.unity3d.com/Manual/class-CharacterController.html
	//				When using a CharacterController, you should remove any previously existing collider, because the CharacterController itself is already a Collider.
	// Exercise 05: The character can now step up/down slopes automatically thanks to the CharacterController component.
	//				But there is a problem : it keeps the same height while moving horizontally and not colliding with anything.
	//				Make the character always stick to the ground : you need to apply some gravity.
	//				Be careful to not apply gravity infinitely while the character is grounded.
	// Exercise 06: Make the character more or less stick to the ground.
	//				Create a float variable "stickToGround" to apply more or less gravity while the character is grounded.
	//				Restrict the range of values of "stickToGround" to [0.1f ; 1f]
	// Exercise 07: Allow the character to jump using the Space key.
	//				You can also create a KeyCode variable "jumpKey" to define the jump key from the UnityEditor.
	//				Create a float variable "jumpHeight" to set the character's maximum jump height.
	//				Compute the vertical velocity required to jump at a desired jump height using kinematic equation : Vf² = Vi² + 2*acceleration*height
	//				https://www.physicsclassroom.com/class/1DKin/Lesson-6/Kinematic-Equations
	// Exercise 08: Cancel the jump movement when the jump key is released before the end of the jump's takeoff phase.
	// Exercise 09: Horizontal kinematic trajectory of the jump movement should also continue after takeoff.
	//				Use the translation speed at the moment of the jump as initial horizontal velocity.
	//				Now you should :
	//				- control the character using velocity only
	//				- slow down the horizontal velocity continuously while grounded : you can create a variable "brakeForce" for this purpose.
	// Exercise 10: Slow down the horizontal movements of the character while it is in the air.
	//              Create a variable "airControl" to apply a gain to the translation speed.
	//              Create a variable "airAngularControl" to apply a gain to the rotation speed.
	// Exercise 11: Allow the character to move (translation, run, jump) using either a keyboard or a Gamepad
	//			    https://docs.unity3d.com/Manual/class-InputManager.html
	// Exercise 12: Allow the character to push gameObjects having rigidbody components.
	//				https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnControllerColliderHit.html
	//				- Add a float variable called "mass" to push objects with more or less force.
	//				- Add a bool variable called "pushObjectsBelow" to allow to push objects below you.
	//				You should enable interpolation on rigidbodies for a better physics simulation.
	// Exercise 13: Add the possibility to also translate in left & right directions.
	//				Add a boolean variable "strafe" to enable/disable this functionality.
	//				- if strafe is true : translation in left & right directions is allowed, and yaw rotation is not allowed
	//				- if strafe is false : translation in left & right directions is not allowed, and yaw rotation is allowed
	// Exercise 14: Create string constants on the top of the script to replace easily all Axis & Buttons used in your code.
	void Update()
	{
#if CHAR_CORRECTION
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

#if CHAR_CORRECTION
	// Exercise 14:
	public const string H_AXIS = "Horizontal";
	public const string V_AXIS = "Vertical";
	public const string RUN_BUTTON = "Run";
	public const string JUMP_BUTTON = "Jump";

	[Tooltip("Allow translation in left & right directions")]
	public bool strafe = false;
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
	[Tooltip("The mass of the character (to interact with others rigid bodies)")]
	public float mass = 10f;
	[Tooltip("Allow to push others rigid bodies below the character controller")]
	public bool pushObjectsBelow = false;

	private Transform _transform;
	private CharacterController _cc;
	private bool _isGrounded = false;
	private bool _isJumping = false;
	private float _jumpSpeed = 0f;
	private Vector3 _velocity = Vector3.zero;
	private Vector3 _xzMoveVelocity = Vector3.zero;
	private float _xzMoveMaxSpeed = 0f;
	private float _xzMoveBrakeSpeed = 0f;
	private float _yawSpeed = 0f;

	// CameraMovement - Exercise 02
	public bool IsGrounded { get { return _isGrounded; } }

	// Exercise 12 : Called every time the CharacterController hit another collider.
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
		// Exercise 01: Can be done direclty in Unity Editor
		//Transform cameraTransform = Camera.main.transform; // This is also costly (== GameObject.FindGameObjectWithTag("MainCamera").transform)
		//cameraTransform.parent = _transform; // As a child of this transform, the camera movement is following automatically this transform.
		//cameraTransform.localPosition = new Vector3(0f, 4f, -10f); // Set the camera a little higher than the character, and a little behind.
		//cameraTransform.localEulerAngles = new Vector3(15f, 0f, 0f); // Pitch the camera a little towards the ground.

		// Excercise 04:
		// Add a CharacterController, but first destroy any existing Collider
		// Because CharacterController is a Collider and we don't need 2 colliders.
		Collider collider = GetComponent<Collider>();
		if (collider != null)
		{
			Destroy(collider);
		}
		_cc = gameObject.AddComponent<CharacterController>();
		_cc.skinWidth = _cc.radius * 0.1f; // recommended
	}

	private void _CC_Update()
	{
		float dt = Time.deltaTime;
		// Check if the character is grounded (=standing on a surface, not in the air)
		_isGrounded = _cc.isGrounded;

		_Inputs();
		_VerticalMove(dt);
		_ApplyMove(dt);

#if UNITY_EDITOR
		// Debug.
		// Draw a vector in the Scene View to debug the vertical velocity
		Debug.DrawRay(_transform.position, _velocity, Color.red);
		// Draw a vector in the Scene View to debug the horizontal move velocity
		Debug.DrawRay(_transform.position, _xzMoveVelocity, Color.green);
		//Debug.Log("Is Grounded : " + _isGrounded);
		//Debug.Log("Velocity : " + _velocity);
		//Debug.Log("Move Velocity : " + _xzMoveVelocity);
#endif
	}

	private void _Inputs()
	{
		// Speeds
		float yawMaxSpeed = rotationSpeed;
		_xzMoveMaxSpeed = translationSpeed;
		if (Input.GetButton(RUN_BUTTON))
		{
			_xzMoveMaxSpeed *= runFactor;
		}
		float xzMoveSpeed = _xzMoveMaxSpeed;

		if (_isGrounded)
		{
			// Exercise 09 : character must slow down due to ground friction when grounded.
			_xzMoveBrakeSpeed = groundFriction * translationSpeed;
		}
		else
		{
			_xzMoveBrakeSpeed = 0f; // no slow down due to ground friction in the air.
									// Exercise 10 : character is less controllable in the air.
			yawMaxSpeed *= airAngularControl;
			xzMoveSpeed *= airControl;
		}

		// Exercise 11 : use of Input.GetAxis() & Input.GetButton(), instead of Input.GetKey().
		float xMove = Input.GetAxisRaw(H_AXIS);
		float zMove = Input.GetAxisRaw(V_AXIS);
		// Exercise 13 : strafe
		if (strafe)
		{
			// Rotation
			_yawSpeed = 0f;
			// Translation in X/Z
			_xzMoveVelocity = _transform.forward * zMove + _transform.right * xMove;
			_xzMoveVelocity = Vector3.ClampMagnitude(_xzMoveVelocity * xzMoveSpeed, xzMoveSpeed);
		}
		else
		{
			// Rotation
			_yawSpeed = xMove * yawMaxSpeed;
			// Translation in Z only
			_xzMoveVelocity = _transform.forward * zMove * xzMoveSpeed;
		}
	}

	private void _VerticalMove(float pDt)
	{
		// JUMP & GRAVITY
		float gravity = Physics.gravity.y;
		float maxFallSpeed = gravity; // value when stickToGround = 1
		if (_isGrounded)
		{
			// Exercise 05: 
			// Apply the same vertical velocity when the character is grounded, in order to:
			// - stop accumulating vertical velocity : we don't need to fall, we are grounded.
			// - stick to the ground
			//_velocity.y = gravity; // this value correspond to a velocity accumulated during 1s.
			// It is an arbitrary value but it makes the feeling the character is really sticking to the ground.

			// Exercise 06: Make the character more or less stick to the ground.
			float minGroundedFallSpeed = gravity * 0.1f; // value when stickToGround = 0 (gravity * dt : not enough for CharacterController.isGrounded to be accurate in all situations).
			_velocity.y = -Mathf.Lerp(-minGroundedFallSpeed, -maxFallSpeed, stickToGround);

			if (_isJumping)
			{
				_isJumping = false;
			}

			if (Input.GetButtonDown(JUMP_BUTTON))
			{
				// Kinematic equation : Vf² = Vi² + 2*a*h
				// When max height (h) is reached : Vf is 0
				// => 0 = Vi² + 2*a*h
				// => Vi² = -2*a*h
				// => Vi = sqrt(-2*a*h)
				_jumpSpeed = Mathf.Sqrt(-2f * gravity * jumpHeight);
				// Exercise 07 : use only vertical velocity as initial velocity
				//_velocity = Vector3.up * _jumpSpeed;

				// Exercise 09 : use move + vertical velocities as initial velocity
				_velocity = _xzMoveVelocity + Vector3.up * _jumpSpeed;
				_isJumping = true;
				//Debug.Log("Jump");
			}
		}
		else
		{
			float fallSpeed = gravity * pDt; // Normal fall speed caused by gravity.
			// Exercise 08 : jump movement is cancelled if the Jump button is released while jumpHeight is not reached (_velocity.y > 0f).
			if (_isJumping && (_velocity.y > 0f) && Input.GetButtonUp(JUMP_BUTTON))
			{
				// To cancel the jump movement, we apply more fall speed.
				// if vertical speed > 0 (=jump height not reached) => apply normal fall speed
				// if vertical speed == _jumpVerticalSpeed (=on the ground just before takeoff) => apply max fall speed
				// Linearly interpolates between the 2 values to apply more "fall speed" when close to the ground, and less "fall speed" while maintaining the jump button pressed.
				fallSpeed = -Mathf.Lerp(-fallSpeed, -maxFallSpeed, _velocity.y / _jumpSpeed);
				//Debug.Log("Jump cancelled");
			}

			// Exercise 05 :
			// When character is in the air, it needs to fall thanks to the effect of gravity (=vertical velocity accumulation)
			_velocity.y += fallSpeed; // gravity is acceleration : it increase the velocity every frame.
		}
	}

	private void _ApplyMove(float pDt)
	{
		// APPLY ROTATION
		if(!strafe)
		{
			_transform.Rotate(0f, _yawSpeed * pDt, 0f);
		}

		// APPLY TRANSLATION
		// Exercise 09 : Control ground velocity
		Vector3 groundVelocity = _velocity + _xzMoveVelocity; // Add move velocity to the current velocity.
		groundVelocity.y = 0f; // ground velocity is in X/Z plane only.
		float groundSpeed = groundVelocity.magnitude;
		// Slowdown & Clamp the new ground velocity
		float newGroundSpeed = Mathf.Clamp(groundSpeed - _xzMoveBrakeSpeed, 0f, _xzMoveMaxSpeed);
		groundVelocity = Vector3.ClampMagnitude(groundVelocity, newGroundSpeed);
		// reset y velocity after working on ground velocity only.
		_velocity.Set(groundVelocity.x, _velocity.y, groundVelocity.z);
		// When using a character controller we cannot use the transform to move the character anymore.
		// Instead we have to call the Move() method, in charge of: 
		// - detecting collision with others objects in the environment
		// - adapting the given movement to step up/down slopes automatically
		// https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
		_cc.Move(_velocity * pDt);
		// Exercise 04 :
		//_cc.Move(_velocity * dt + _xzMoveVelocity * dt);
	}
#endif
}
