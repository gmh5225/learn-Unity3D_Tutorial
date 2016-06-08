using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float desiredSpeed = 10.0f; // in m/s
	public float rotationSpeed = 360.0f; // in deg/s

	Vector3 lastPos = Vector3.zero;
	Vector3 currentPos = Vector3.zero;
	Vector3 move = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Translation
		move = Vector3.zero;
		if (Input.GetKey(KeyCode.UpArrow))
		{
			move.z = desiredSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			move.z = -desiredSpeed * Time.deltaTime;
		}
		transform.Translate(move);

		// Rotation
		float rotation = 0.0f;
		if (Input.GetKey(KeyCode.RightArrow))
		{
			rotation = rotationSpeed * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{
			rotation = -rotationSpeed * Time.deltaTime;
		}
		transform.Rotate(transform.up, rotation, Space.World);

		//Debug.Log("Speed : " + ComputeSpeed());
	}

	float ComputeSpeed()
	{
		lastPos = currentPos;
		currentPos = transform.position;

		float speed = ((currentPos - lastPos) / Time.deltaTime).magnitude;
		return speed;
	}
}
