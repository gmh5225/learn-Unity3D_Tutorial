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

using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	// Exercise 01: Get LeftArrow/RightArrow inputs from keyboard to rotate (left/right) the character using its transform.
	//				Which axis of the transform do you need to use ?
	//				Create a public float variable "rotationSpeed" to rotate the character continuously at a given angular speed.
	// Exercise 02: Get UpArrow/DownArrow inputs from keyboard to move (forward/backward) the character using its transform.
	//				Which axis of the transform do you need to use ?
	//				Which reference frame do you need to use ?
	//				Create a public float variable "translationSpeed" to translate the character continuously at a given speed.
	// Exercise 03: Make the main camera follow the character continuously : https://docs.unity3d.com/Manual/Hierarchy.html#Parenting
	// Exercise 04: Allow the character to run using the Left Shift Key.
	//				Create a public float variable "runFactor" to speed up the character's translation when the key is pressed.
	//				Use an intermediate variable "maxTranslationSpeed" to handle the current maximum allowed speed and to translate accordingly.
	// Exercise 05: Allow the character to move on any terrain topologies with holes and slopes.
	//				https://docs.unity3d.com/Manual/class-CharacterController.html
	//				When using a CharacterController, you should remove any previously existing collider, because the CharacterController itself is already a Collider.
	//				Create a private CharacterController variable "_cc" to get a reference to the CharacterController component at start of the application.
	// Exercise 06: The character can now step up/down slopes automatically thanks to the CharacterController component.
	//				But there is a problem : it keeps the same height while moving horizontally and not colliding with anything.
	//				Make the character always stick to the ground : you need to apply some gravity.
	//				To do this, create a private Vector3 variable "_velocity".
	// Exercise 07:	Show the _velocity vector in red in the scene view using the method Debug.DrawRay().
	//				What's happen when the character is grounded ?
	//				Make sure gravity is not applied infinitely while the character is grounded.
	// Exercise 08: Make the character more or less stick to the ground.
	//				Create a public float variable "stickToGround" to apply more or less gravity while the character is grounded.
	//				Restrict the range of values of "stickToGround" to [0.1f ; 1f] from the code and then from the UnityEditor.
	// Exercise 09: Allow the character to jump using the Space key.
	//				Create a public float variable "jumpHeight" to set the character's maximum jump height.
	//				Compute the vertical velocity required to jump at a desired jump height using kinematic equation : Vf² = Vi² + 2 * gravity * jumpheight
	//				https://www.physicsclassroom.com/class/1DKin/Lesson-6/Kinematic-Equations
	// Exercise 10: Create a private bool variable "_isJumping" to always know when the character is jumping or not.
	//				Update the value of this variable in the Update() method.
	//				Be careful to distinguish a jump and a simple fall from a step.
	// Exercise 11: Cancel the jump movement when the jump key is released before the end of the jump's takeoff phase.
	//				How to know if we are in the takeoff phase of a jump ?
	//				- You'll need to apply an opposite of the current vertical velocity.
	//				- Make this opposite vertical velocity relative to the "stickToGround" value.
	// Exercise 12: Make the character more or less sliding on the ground regarding its current _velocity.
	//				- Create a private Vector3 variable "_xzMoveVelocity" to handle the desired X/Z move velocity (from the inputs).
	//				- Use _xzMoveVelocity to move the character.
	//				- Show the _xzMoveVelocity vector in green in the scene view using the method Debug.DrawRay().
	// Exercise 13: The character is now sliding infinitely : how can we stop it ?
	//				Create a public float variable "groundFriction" and use it to slow down the ground velocity continuously while the character is grounded.
	//				Restrict the range of values of "groundFriction" to [0.001f ; 1f] from the UnityEditor.
	//				You should also ensure the ground velocity to never be :
	//				- negative
	//				- greater than "maxTranslationSpeed"
	// Exercise 14:	Now let's simulate jump's horizontal inertia.
	//				We would like the jump movement to also consider the ground velocity (not only the vertical velocity) at start of a jump.
	//				This way the character horizontal trajectory defined at start of a jump can continue, even if the jump is aborted.
	// Exercise 15: Slow down the control of movements of the character while it is in the air.
	//              - Create a public float variable "airControl" to slow down the control of horizontal movements.
	//              - Create a public float variable "airAngularControl" to slow down the control of the yaw rotation.
	// Exercise 16: Allow the character to move (translation, run, jump) using either a keyboard or a Gamepad
	//				In Project Settings > Inputs (https://docs.unity3d.com/Manual/class-InputManager.html):
	//				- Create a second "Horizontal" axis (if not already existing) : Gravity = 0 ; Dead = 0.3 ; Sensitivity = 1 ; Type = Joystick Axis ; Axis = X Axis
	//				- Create a second "Vertical" axis (if not already existing) : Gravity = 0 ; Dead = 0.3 ; Sensitivity = 1 ; Invert = true ; Type = Joystick Axis ; Axis = Y Axis
	//				- Create a "Run" button (if not already existing) : (Alt) Positive Button = joystick button 4 ; Gravity = 1000 ; Dead = 0.001 ; Sensitivity = 1000 ; Type = Key or Mouse Button
	//				- Create a "Jump" button (if not already existing) : (Alt) Positive Button = joystick button 5 ; Gravity = 1000 ; Dead = 0.001 ; Sensitivity = 1000 ; Type = Key or Mouse Button
	//				Use these axis and buttons in your code instead of the GetKey/Up/Down() calls.
	//				Use GetAxisRaw() instead of GetAxis() for the inputs to be more reactive (no smoothing).
	// Exercise 17: Allow the character to push gameObjects having rigidbody components.
	//				- Add the method OnControllerColliderHit() (https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnControllerColliderHit.html)
	//				- Create a public float variable "mass" to push objects with more or less force.
	//				- Use "mass" and "_velocity" to push other gameObjects having rigidbody components, using the AddForce() method.
	// Exercise 18: Fine-tune interactions with other gameObjects
	//				- In Unity Editor, enable interpolation on other objects' rigidbodies for a more accurate physics simulation.
	//				- Push only in X/Z directions
	//				- Add a public bool variable "pushObjectsBelow" to allow (or not) to push objects below you.
	//				- Make force relative to groundFriction when pushing objects below.
	//				- Use method AddForceAtPosition() to add a force and a torque at the same time.
	// Exercise 19: Add the possibility to also translate in left/right directions (not only forward/backward).
	//				- Create a public bool variable "strafe" to enable/disable this functionality.
	//				- if strafe is true : translation in left & right directions is allowed, but yaw rotation is not allowed
	//				- if strafe is false : translation in left & right directions is not allowed, but yaw rotation is allowed
	// Exercise 20: Create string constants on the top of the script to replace easily all Axis & Buttons used in your code.
	// Exercise 21: Make sure Debug.DrawRay() calls happen only in the Unity Editor : https://docs.unity3d.com/Manual/PlatformDependentCompilation.html

	// Exercise 20
	// Constants are just literals (no memory slot required). 
	// They are automatically replaced by their values by the compiler everywhere they are used in the code.
	const string H_AXIS = "Horizontal";
	const string V_AXIS = "Vertical";
	const string RUN = "Run";
	const string JUMP = "Jump";

	// Exercise 01
	public float rotationSpeed = 120f; // in deg/s
	// Exercise 02
	public float translationSpeed = 8f; // in m/s
	// Exercise 04
	[Tooltip("translationSpeed multiplier")]
	public float runFactor = 2f;
	// Exercise 08
	[Range(0.1f, 1f)]
	[Tooltip("How much the character should stick to the ground")]
	public float stickToGround = 0.5f;
	// Exercise 09
	[Tooltip("Character's max jump height")]
	public float jumpHeight = 2f;
	// Exercise 13
	[Range(0.001f, 1f)]
	[Tooltip("How slippery is the ground")]
	public float groundFriction = 0.5f;
	// Exercise 15
	[Tooltip("How much to slow down control of horizontal movement while the character is in the air")]
	public float airControl = 0.01f;
	[Tooltip("How much to slow down control of rotation while the character is in the air")]
	public float airAngularControl = 0.2f;
	// Exercise 17
	public float mass = 2f;
	// Exercise 18
	public bool pushObjectsBelow = false;
	// Exercise 19
	public bool strafe = false;

	// Exercise 05
	private CharacterController _cc;
	// Exercise 06 : the current velocity in all axis.
	private Vector3 _velocity = Vector3.zero;
	// Exercise 10
	private bool _isJumping = false;
	// Exercise 12 : the desired X/Z move velocity (from the inputs).
	Vector3 _xzMoveVelocity = Vector3.zero;

	// CameraMovement - Exercise 03
	public bool IsGrounded { get { return _cc.isGrounded; } }
	
	void Awake()
	{
		// Exercise 05 : Get a reference to the CharacterController at start of the Application.
		_cc = GetComponent<CharacterController>();
		if(_cc != null)
		{
			_cc.skinWidth = _cc.radius * 0.1f; // recommended parameter : https://docs.unity3d.com/Manual/class-CharacterController.html
		}
		else
		{
			Debug.LogError("[CharacterMovement] No CharacterController found ! Please add one.");
		}
	}

	// Exercise 17
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		//Debug.Log("[ControllerColliderHit] " + hit.gameObject.name);
		//Debug.Log("[ControllerColliderHit] Move direction : " + hit.moveDirection);

		Rigidbody rb = hit.rigidbody;
		// No rigidbody or kinematic one (= not under control of the physics engine).
		if (rb == null || rb.isKinematic)
		{
			return;
		}

		float friction = 1f;
		bool objectBelow = hit.moveDirection.y < -0.3f;
		if (objectBelow)
		{
			if(pushObjectsBelow)
			{
				// Exercise 18 : Make force relative to groundFriction when pushing object below
				friction = groundFriction;
			}
			else
			{
				// Exercise 18 : Pushing objects below could be not allowed
				return;
			}
		}

		// Push rigid body object.
		Vector3 force = _velocity * friction * mass;
		//rb.AddForce(force, ForceMode.Force); // Apply force to the center of mass
		//Debug.Log("[ControllerColliderHit] Force : " + force);

		// Exercise 18
		force.y = 0f; // Push only with horizontal forces (avoid weird physics behaviours)
		rb.AddForceAtPosition(force, hit.point, ForceMode.Force); // Apply force to the hit point.
	}

	void Update()
	{
		// In Update(), we need to multiply speeds with Time.deltaTime to get the quantity of rotation/translation to apply each frame.
		// As deltaTime (inverse of framerate) varies regarding the amount of computation to do each frame, this way we are sure to apply
		// the right quantity of rotation/translation to maintain constant speeds no matter the current framerate.
		#region Exercise 01 : Rotate using LeftArrow/RightArrow keys
		/*
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
		// Apply rotation to the Y axis of the local reference frame(Space.Self) of the gameObject.
		transform.Rotate(0f, yawRotation, 0f, Space.Self);
		// The code below do the same as the Rotate() method.
		//Vector3 euler = transform.eulerAngles;
		//euler.y += yawRotation;
		//transform.eulerAngles = euler;
		*/
		#endregion

		#region Exercise 02 : Translate using UpArrow/DownArrow keys
		/*
		float zMove = 0f;
		if (Input.GetKey(KeyCode.UpArrow))
		{
			// Translate forward (=positive direction) along the Z axis.
			zMove = translationSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			// Translate backward (=negative direction) along the Z axis.
			zMove = -translationSpeed * Time.deltaTime;
		}
		// Apply translation along the Z axis of the local reference frame (Space.Self) of the gameObject.
		transform.Translate(0f, 0f, zMove, Space.Self);
		//The code below do the same as the Translate() method.
		//Vector3 pos = transform.position;
		//pos += transform.forward * zMove; // == transform.rotation * new Vector3(0f, 0f, zMove)
		//transform.position = pos;
		*/
		#endregion

		// Exercise 04 : Run
		float maxTranslationSpeed = translationSpeed;
		//if (Input.GetKey(KeyCode.LeftShift))
		if (Input.GetButton(RUN))
		{
			maxTranslationSpeed *= runFactor;
		}

		// Exercise 16: translate using either a keyboard or a gamepad.
		float zSpeed = Input.GetAxisRaw(V_AXIS) * maxTranslationSpeed;
		// transform.forward = the z axis of the local reference frame of the gameObject.
		_xzMoveVelocity = transform.forward * zSpeed;

		// Exercise 19
		float yawRotation = 0f;
		if (strafe)
		{
			float xSpeed = Input.GetAxisRaw(H_AXIS) * maxTranslationSpeed;
			// transform.right = the x axis of the local reference frame of the gameObject.
			_xzMoveVelocity += transform.right * xSpeed;
		}
		else
		{
			// Exercise 16: rotate using either a keyboard or a gamepad.
			yawRotation = Input.GetAxisRaw(H_AXIS) * rotationSpeed * Time.deltaTime;
		}

		Vector3 gravity = Physics.gravity;
		float xzBrakeSpeed = 0f;
		// Exercise 07 : Check if the character is grounded (=standing on a surface, not in the air)
		if (_cc.isGrounded)
		{
			// Exercise 10: if we were jumping and we are now grounded, it means the jump is finished.
			if (_isJumping) // distinguish a jump and a simple fall from a step.
			{
				_isJumping = false;
			}

			// Exercise 07 :
			// Apply the same vertical velocity when the character is grounded, in order to:
			// - stop accumulating vertical velocity : we don't need to fall, we are grounded.
			// - stick to the ground
			//_velocity.y = gravity.y; // this value correspond to a velocity accumulated during 1s.
			// It is an arbitrary value but it makes the feeling the character is really sticking to the ground.
			
			// Exercise 08 : stick more or less to the ground
			_velocity.y = gravity.y * stickToGround; //Mathf.Clamp(stickToGround, 0.1f, 1f);

			// Exercise 09 : Jump
			//if (Input.GetKeyDown(KeyCode.Space))
			if (Input.GetButtonDown(JUMP))
			{
				// Vf² = Vi² + 2 * gravity * jumpheight
				// When we reach jumpheight, we know : Vf = 0
				// 0 = Vi² + 2 * gravity * jumpheight
				// Vi² = -2 * gravity * jumpheight
				// Vi =  Sqrt(-2 * gravity * jumpheight)
				_velocity.y = Mathf.Sqrt(-2f * gravity.y * jumpHeight);

				// Exercise 14 : jump with horizontal inertia.
				_velocity += _xzMoveVelocity;
				
				// Exercise 10
				_isJumping = true;
			}

			// Exercise 13 : stop sliding infinitely with groundFriction
			xzBrakeSpeed = translationSpeed * groundFriction;
		}
		else // We are in the air
		{
			// Exercise 15 : slow down control of horizontal movements while in the air
			_xzMoveVelocity *= airControl;
			yawRotation *= airAngularControl;

			// Exercise 11 : During a jump, check if the jump key is released during the takeoff phase (vertical velocity > 0)
			//if (_isJumping && Input.GetKeyUp(KeyCode.Space) && (_velocity.y > 0f))
			if (_isJumping && Input.GetButtonUp(JUMP) && (_velocity.y > 0f))
			{
				// If yes, apply down velocity according to current vertical velocity and stickToGround parameter.
				_velocity.y = -_velocity.y * stickToGround;
			}

			// Exercise 07 : When character is in the air, it needs to fall thanks to the effect of gravity (=vertical velocity accumulation)
			_velocity.y += gravity.y * Time.deltaTime; // gravity is acceleration : it increase the velocity every frame.
		}

		// Exercise 19
		if(!strafe)
		{
			// Exercise 15 : apply rotation now here to take into account the effect of airAngularControl
			transform.Rotate(0f, yawRotation, 0f, Space.Self);
		}

		// Exercise 13 : stop sliding infinitely with groundFriction
		Vector3 xzVelocity = _velocity + _xzMoveVelocity; // take all ground velocities
		xzVelocity.y = 0f; // ground velocity is in X/Z plane only.
		float xzSpeed = xzVelocity.magnitude; // ground speed.
		if(xzSpeed > 0f)
		{
			// Slowdown the ground speed, but don't allow negative speed.
			float newXZSpeed = Mathf.Max(0f, xzSpeed - xzBrakeSpeed);
			// Limit ground speed with maxTranslationSpeed
			newXZSpeed = Mathf.Min(newXZSpeed, maxTranslationSpeed);
			// Finally limit ground velocity (clamp magnitude of the vector xzVelocity)
			xzVelocity /= xzSpeed; // normalize xzVelocity vector : xzVelocity's length is now 1
			xzVelocity *= newXZSpeed; // xzVelocity's length is now newXZSpeed
		}
		// Apply result to the _velocity vector.
		_velocity.x = xzVelocity.x;
		_velocity.z = xzVelocity.z;

		// Exercise 05 : When using a character controller we cannot use the transform to move the character anymore.
		// Instead we have to call the Move() method, in charge of: 
		// - detecting collision with others objects in the environment
		// - adapting the given movement to step up/down slopes automatically
		// https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
		//_cc.Move(transform.forward * zMove);

		// Exercise 06 : Apply gravity for the character to stick to the ground.
		//_velocity += gravity * Time.deltaTime;
		//_cc.Move(_velocity * Time.deltaTime + transform.forward * zMove);

		// Exercise 12 : control the character using _xzMoveVelocity.
		//_velocity += _xzMoveVelocity;

		_cc.Move(_velocity * Time.deltaTime);

		// Exercise 21
#if UNITY_EDITOR
		// Exercise 07 : Draw a red vector in the Scene View to debug _velocity.
		Debug.DrawRay(transform.position, _velocity, Color.red);
		//Debug.Log("Velocity : " + _velocity);
		// Exercise 12 : Draw a green vector in the Scene View to debug _xzMoveVelocity.
		Debug.DrawRay(transform.position, _xzMoveVelocity, Color.green);
		//Debug.Log("XZ Move Velocity : " + _xzMoveVelocity);
#endif
	}
}