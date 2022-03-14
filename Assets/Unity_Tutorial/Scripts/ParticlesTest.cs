using UnityEngine;

public class ParticlesTest : MonoBehaviour
{
	public ParticleSystem particlesPrefab;

	private Camera _camera;

	private void Awake()
	{
		// We can get a component directly on the gameObject.
		_camera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update()
    {
        if(Input.GetMouseButtonDown(0))
		{
			// Throw a ray to the plane.
			Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out RaycastHit hitInfo))
			{
				//Debug.LogWarning("Plane hit at : " + hitInfo.point);
				// Create a particles gameObject where the ray hit the plane.
				ParticleSystem particles = GameObject.Instantiate(particlesPrefab, hitInfo.point, Quaternion.identity);
				// The particles gameObject will be destroyed after the given delay.
				GameObject.Destroy(particles.gameObject, particles.main.duration);
			}
		}
    }
}
