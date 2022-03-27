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

// Put this script on the Camera's gameObject
public class CameraMovement : MonoBehaviour
{
	// Exercise 01: Ensure Camera is placed at a convenient position & rotation at start.
	//				Create a public CharacterMovement variable "characterMovement" to access characterMovement.transform.
	// Exercise 02: Use Mouse X Axis to control character's yaw rotation.
	//				Create a public float variable "yawSpeed" to adjust the yaw rotation speed if needed.
	// Exercise 03: Yaw rotation should consider character state (grounded or not) to adapt the yaw rotation speed.
	//				Allow characterMovement to share its _isGrounded attribute using a public readonly property "IsGrounded".
	//				Use the characterMovement.airAngularControl to adjust the yaw rotation speed.
	// Exercise 04: Use Mouse Y axis to control camera's pitch rotation.
	//				Create a public float variable "pitchSpeed" to adjust the pitch rotation speed if needed.
	//				Camera's pitch rotation should also consider character state (grounded or not) to adapt the pitch rotation speed.
	//				Use the characterMovement.airAngularControl to adjust the yaw rotation speed.
	// Exercise 05: Where can we put the code to update CameraMovement to be sure it will always take into account the latest character's state ?
	//				https://docs.unity3d.com/ScriptReference/MonoBehaviour.LateUpdate.html
	// Exercise 06: What problem do you have when camera's pitch > 90f or < -90f ?
	//				What can you do to tackle this issue ?
	//				Constraint pitch values to a comfortable range for the gameplay.
	// Exercise 07: - If the script is enabled : the mouse cursor should disappear & be locked to the center of the screen + characterMovement.strafe should be true
	//				- If the script is disabled : the mouse cursor should appear & be unlocked + characterMovement.strafe should be false
	//				https://docs.unity3d.com/ScriptReference/Cursor.html
	// Exercise 08: The second stick of a gamepad can also be used instead of Mouse X/Y axis.
	//				- Create a second "Mouse X" axis (if not already existing) : Gravity = 0 ; Dead = 0.3 ; Sensitivity = 1 ; Type = Joystick Axis ; Axis = 4th axis
	//				- Create a second "Mouse Y" axis (if not already existing) : Gravity = 0 ; Dead = 0.3 ; Sensitivity = 1 ; Invert = true ; Type = Joystick Axis ; Axis = 5th axis
	// Exercise 09: Allow the use of the mouse scrollwheel to control camera distance from the character (zoom in/out)
	//				Create a public float variable "mouseWheelSpeed" to adjust the zoom in/out speed.
	//				Along which vector do you need to move the camera ?
	//				Create a private Vector3 variable "_characterToCameraDir" to adjust the zoom in/out speed.
	//				Ensure that this vector is never equal to Vector3.zero.
	// Exercise 10: Constraint camera distance to vary in a comfortable range for the gameplay
	//				- Create a public float variable "minDistance"
	//				- Create a public float variable "maxDistance"
	// Exercise 11:	Camera distance should be also controllable with a gamepad
	//				- Create a second "Mouse ScrollWheel" axis (if not already existing) : Gravity = 0 ; Dead = 0.01 ; Sensitivity = 0.1 ; Type = Joystick Axis ; Axis = 3rd axis
	// Exercise 12: Create string constants on the top of the script to replace easily all Axis used in your code.

	// Exercise 12
	public const string MOUSE_X = "Mouse X";
	public const string MOUSE_Y = "Mouse Y";
	public const string MOUSE_SW = "Mouse ScrollWheel";

	// Exercise 01
	public CharacterMovement characterMovement;
	// Exercise 02
	public float yawSpeed = 1f;
	// Exercise 04
	public float pitchSpeed = 1f;
	// Exercise 06 Camera Pitch constrained
	public float pitchMin = -17f;
	public float pitchMax = 23f;
	// Exercise 09
	public float mouseWheelSpeed = 10f;
	// Exercise 10
	public float minDistance = 10f;
	public float maxDistance = 40f;

	// Exercise 09
	private Vector3 _characterToCameraDir;

	// Exercise 06: enabled = cursor disappear & is locked to the center of the screen + characterMovement.strafe = true
	private void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		characterMovement.strafe = true;
	}
	// Exercise 06: disabled = cursor appear & is unlocked + characterMovement.strafe = false
	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		characterMovement.strafe = false;
	}

	private void Awake()
	{
		// Exercise 01 : Ensure parent is character and a convenient placement of the camera is set at start.
		transform.parent = characterMovement.transform; // As a child of this transform, the camera movement is following automatically this transform.
		transform.localPosition = new Vector3(0f, 4f, -10f); // Set the camera a little higher than the character, and a little behind.
		transform.localEulerAngles = new Vector3(15f, 0f, 0f); // Pitch the camera a little towards the ground.
		
		// Exercise 09 : Here we are sure the direction vector is not Vector3.zero.
		// We normalize it once for all to avoid duplicate computations in Update().
		_characterToCameraDir = transform.localPosition.normalized;
	}

	// Execute after Character movements
	private void LateUpdate()
	{
		// Exercise 02:
		float yaw = Input.GetAxisRaw(MOUSE_X) * yawSpeed;
		// Exercise 04:
		float pitch = -Input.GetAxisRaw(MOUSE_Y) * pitchSpeed;

		if(!characterMovement.IsGrounded)
		{
			// Exercise 03: adapt yaw regarding character's grounded state
			yaw *= characterMovement.airAngularControl;
			// Exercise 04: adapt pitch regarding character's grounded state
			pitch *= characterMovement.airAngularControl;
		}
		// Exercise 02: Character Yaw
		characterMovement.transform.Rotate(Vector3.up, yaw, Space.Self);
		// Exercise 04: Camera Pitch
		Vector3 euler = transform.localEulerAngles;
		//euler.x += _cameraPitch;
		// Exercise 06: Camera Pitch constrained
		euler.x = _ClampAngle(euler.x + pitch, pitchMin, pitchMax);
		transform.localEulerAngles = euler;
		// Exercise 09: Mouse wheel controls camera Distance
		float deltaDistance = -Input.GetAxisRaw(MOUSE_SW) * mouseWheelSpeed;
		//transform.localPosition += _characterToCameraDir * deltaDistance;
		// Exercise 10: Constraint camera Distance
		float distance = transform.localPosition.magnitude;
		float newDistance = Mathf.Clamp(distance + deltaDistance, minDistance, maxDistance);
		transform.localPosition = _characterToCameraDir * newDistance;
	}

	// Exercise 06
	private float _ClampAngle(float pAngle, float pMinAngle = -180f, float pMaxAngle = 180f)
	{
		// Ensure angle is in [-180;180]
		pAngle %= 360f;
		if (pAngle > 180f)
		{
			pAngle -= 360f;
		}
		// Clamp
		return pAngle > pMaxAngle ? pMaxAngle : pAngle < pMinAngle ? pMinAngle : pAngle;
	}
}