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

#define CAM_CORRECTION

using UnityEngine;

// Put this script on the Camera's gameObject
public class CameraMovement : MonoBehaviour
{
	public CharacterMovement characterMovement;
	// Exercise 01: Use Mouse X Axis to control character's yaw rotation.
	//				Create a float variable yawSpeed to adjust the yaw rotation speed if needed.
	// Exercise 02: Yaw rotation should consider character state (grounded or not) to adapt the yaw rotation speed.
	//				Allow CharacterMovement script to share publicly its _isGrounded attribute using a readonly property "IsGrounded".
	// Exercise 03: Use Mouse Y axis to control camera's pitch rotation.
	//				Create a float variable pitchSpeed to adjust the yaw rotation speed if needed.
	//				Camera's pitch rotation should also consider character state (grounded or not) to adapt the pitch rotation speed.
	// Exercise 04: What problem do you have when camera's pitch > 90f or < -90f ?
	//				What can you do to tackle this issue ?
	//				Constraint pitch values to a comfortable range for the gameplay.
	//				Where can we put the code to update CameraMovement to be sure it will always take into account the lastest character's state ?
	// Exercise 05: Allow the mouse cursor to:
	//				- disappear & be locked to the center of the screen, when the script is enabled
	//				- appear & be unlocked, when the script is disabled
	// Exercise 06: The second stick of a gamepad can also be used instead of Mouse X/Y axis.
	//				No need to change anything in the code !
	// Exercise 07: Allow the use of the mouse scrollwheel to control camera distance from the character
	//				Create a float variable "mouseWheelSpeed" to to adjust camera motion's speed.
	// Exercise 08: Constraint camera distance to vary in a comfortable range for the gameplay
	// Exercise 09:	Camera distance should be also controllable with a gamepad
	//				No need to change anything in the code !
	// Exercise 10: Create string constants on the top of the script to replace easily all Axis used in your code.

#if CAM_CORRECTION
	// Exercise 10:
	public const string MOUSE_X = "Mouse X";
	public const string MOUSE_Y = "Mouse Y";
	public const string MOUSE_SW = "Mouse ScrollWheel";

	// Exercise 01:
	public float yawSpeed = 1f;
	// Exercise 03:
	public float pitchSpeed = 1f;
	// Exercise 04: Camera Pitch constrained
	public float pitchMin = -17f;
	public float pitchMax = 23f;
	// Exercise 07:
	public float mouseWheelSpeed = 10f;
	// Exercise 08:
	public float minDistance = 10f;
	public float maxDistance = 40f;

	private Transform _cameraTransform;
	private Transform _characterTransform;

	// Exercise 05: cursor disappear & is locked to the center of the screen, when the script is enabled
	private void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	// Exercise 05: cursor appear & is unlocked, when the script is disabled
	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	private void Awake()
	{
		_cameraTransform = transform;
		_characterTransform = characterMovement.transform;
	}

	// Execute after Character movements
	private void LateUpdate()
	{
		// Exercise 01:
		float yaw = Input.GetAxis(MOUSE_X) * yawSpeed;
		// Exercise 03:
		float pitch = -Input.GetAxis(MOUSE_Y) * pitchSpeed;

		if(!characterMovement.IsGrounded)
		{
			// Exercise 02: adapt yaw regarding character's grounded state
			yaw *= characterMovement.airAngularControl;
			// Exercise 03: adapt pitch regarding character's grounded state
			pitch *= characterMovement.airAngularControl;
		}
		// Exercise 01: Character Yaw
		_characterTransform.Rotate(Vector3.up, yaw, Space.Self);
		// Exercise 03: Camera Pitch
		Vector3 euler = _cameraTransform.localEulerAngles;
		//euler.x += _cameraPitch;
		// Exercise 04: Camera Pitch constrained
		euler.x = _ClampAngle(euler.x + pitch, pitchMin, pitchMax);
		_cameraTransform.localEulerAngles = euler;
		// Exercise 07: Mouse wheel controls camera Distance
		float deltaDistance = -Input.GetAxis(MOUSE_SW) * mouseWheelSpeed;
		Vector3 cameraOffset = _cameraTransform.localPosition;
		//cameraOffset += cameraOffset.normalized * mouseWheel;
		//_cameraTransform.localPosition = cameraOffset;
		// Exercise 08: Constraint camera Distance
		float distance = cameraOffset.magnitude;
		float newDistance = Mathf.Clamp(distance + deltaDistance, minDistance, maxDistance);
		_cameraTransform.localPosition = cameraOffset / distance * newDistance;
	}

	// Exercise 04: 
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
#endif
}