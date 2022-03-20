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
