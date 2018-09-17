using UnityEngine;

public class CollisionTest : MonoBehaviour
{
	public float power = 1.0f;

	// Catch events with collision (collider.isTrigger = false).
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.name + " started collision !");
		collision.rigidbody.AddForce(collision.impulse * power, ForceMode.Impulse);
	}

	private void OnCollisionExit(Collision collision)
	{
		Debug.Log(collision.gameObject.name + " stopped collision !");
	}

	// Catch events only (collider.isTrigger = true).
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.gameObject.name + " entered !");
	}

	private void OnTriggerExit(Collider other)
	{
		Debug.Log(other.gameObject.name + " exited !");
	}
}
