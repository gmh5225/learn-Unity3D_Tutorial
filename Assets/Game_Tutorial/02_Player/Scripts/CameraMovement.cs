using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform character;
	// Exercise 01: Allow the use of a Mouse to control character's yaw rotation and the camera's pitch rotation.
	//				- Mouse Y axis => control camera pitch : constraint pitch values to a comfortable range for the gameplay
	//				- Mouse X axis => control character yaw : constraint yaw values to a comfortable range for the gameplay
	// Exercise 02: Allow the mouse cursor to:
	//				- disappear & be locked to the center of the screen, when the script is enabled
	//				- appear & be unlocked, when the script is disabled
	// Exercise 03: The second stick of a gamepad can also be used instead of Mouse X/Y axis
	// Exercise 04: Allow the use of the mouse scrollwheel to control camera distance from the character
	// Exercise 05: Constraint camera.position.z to vary in a comfortable range for the gameplay
	//		        Constraint camera.position.y to vary in a comfortable range for the gameplay
	// Exercise 06:	Camera distance should be also controllable with a gamepad

	// Exercise 02
	private void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
