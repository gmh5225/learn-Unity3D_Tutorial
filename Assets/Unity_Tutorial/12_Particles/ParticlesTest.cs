using UnityEngine;

public class ParticlesTest : MonoBehaviour
{
	public bool debug = true;
	public ParticleSystem particlesPrefab;
	private Camera _camera;

	private void Awake()
	{
		// Get the Camera component directly on this gameObject.
		_camera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update()
    {
		// Left-button : 0
		// Right-button : 1
		if (Input.GetMouseButtonDown(0))
		{
			// The mouse position at the moment of the mouse left-click.
			Vector3 mousePos = Input.mousePosition;
			// Create a ray from camera to point (mouse position) on screen.
			Ray rayFromCameraToScreen = _camera.ScreenPointToRay(mousePos);
			// Throw the ray in the 3D scene and see if an object is hit.
			if(Physics.Raycast(rayFromCameraToScreen, out RaycastHit hitInfo))
			{
				// An object has been hit by the ray at this position.
				Vector3 hitPos = hitInfo.point;
				// Create a particles gameObject where the ray hit the object.
				ParticleSystem particles = GameObject.Instantiate(particlesPrefab, hitPos, Quaternion.identity);
				//Vector3 euler = new Vector3(0f, 0f, 0f); // == Quaternion.identity
				// The particles gameObject will be destroyed after its duration.
				GameObject.Destroy(particles.gameObject, particles.main.duration);

				// Debug
				if(debug)
				{
					Debug.Log(string.Format("{0} hit at {1}", hitInfo.transform.name, hitPos));
					Debug.DrawRay(rayFromCameraToScreen.origin, rayFromCameraToScreen.direction * 1000f, Color.red, 1f);
				}
			}
		}
    }
}
