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

public class ButtonTest : MonoBehaviour
{
	public GameObject cubesPrefab;
	public float waitTime = 10f;

	private float _elapsedTime = 0f;
	private MeshRenderer _meshRenderer;
	private bool _ready = true;

	private void Awake()
	{
		_meshRenderer = GetComponent<MeshRenderer>();
		_meshRenderer.material.color = Color.green;
	}

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log(other.name);

		if(_ready && other.CompareTag("Player"))
		{
			GameObject.Instantiate(cubesPrefab, cubesPrefab.transform.position, cubesPrefab.transform.rotation);
			_meshRenderer.material.color = Color.red;
			_elapsedTime = 0f;
			_ready = false;
		}
	}

	private void Update()
	{
		if(!_ready)
		{
			_elapsedTime += Time.deltaTime;
			if (_elapsedTime >= waitTime)
			{
				_meshRenderer.material.color = Color.green;
				_ready = true;
			}
		}
	}
}
