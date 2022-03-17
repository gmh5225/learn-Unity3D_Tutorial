using UnityEngine;

public class CharacterControllerTest : MonoBehaviour
{
	// In Update(), we need to multiply speeds with Time.deltaTime to get the quantity of rotation/translation to apply each frame.
	// As deltaTime (inverse of framerate) varies regarding the amount of computation to do each frame, this way we are sure to apply
	// the right quantity of rotation/translation to maintain constant speeds no matter the current framerate.
	public float translationSpeed = 3f; // in m/s
	public float rotationSpeed = 120f; // in deg/s

	void Update()
	{
		// Rotation
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			// Rotate to the left (=negative rotation) on the local (Space.Self) Y axis of the gameObject.
			transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f, Space.Self);
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			// Rotate to the right (=positive rotation) on the local (Space.Self) Y axis of the gameObject.
			transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);
		}

		// Translation
		if (Input.GetKey(KeyCode.UpArrow))
		{
			// Translate forward (=positive direction) along the local (Space.Self) Z axis of the gameObject.
			transform.Translate(0f, 0f, translationSpeed * Time.deltaTime, Space.Self);
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			// Translate backward (=negative direction) along the local (Space.Self) Z axis of the gameObject.
			transform.Translate(0f, 0f, -translationSpeed * Time.deltaTime, Space.Self);
		}

	}
}
