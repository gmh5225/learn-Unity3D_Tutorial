/******************************************************************************************************************************************************
* MIT License																																		  *
*																																					  *
* Copyright (c) 2020																																  *
* Emmanuel Badier <emmanuel.badier@gmail.com>																										  *
* 																																					  *
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),  *
* to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,  *
* and/or sell copies of the Software, and to permit persons to whom the Software isfurnished to do so, subject to the following conditions:			  *
* 																																					  *
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.					  *
* 																																					  *
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, *
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 																							  *
* IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 		  *
* TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.							  *
******************************************************************************************************************************************************/

using UnityEngine;

namespace Whatever // Since Unity 4.x
{
	// Execution order of methods : https://docs.unity3d.com/Manual/ExecutionOrder.html
	public class MonoBehaviourMethods : MonoBehaviour
	{
		void Awake()
		{
			Debug.Log("Awake");
        }

		void Start()
		{
			Debug.Log("Start");
		}

		void OnEnable()
		{
			Debug.Log("OnEnable");
		}

		void OnDisable()
		{
			Debug.Log("OnDisable");
		}

		void OnDestroy()
		{
			Debug.Log("OnDestroy");
		}

		// Main Loop
		void Update()
		{
			Debug.Log("Update : " + Time.deltaTime);

			if(Input.GetKeyUp(KeyCode.Space))
			{
				Debug.LogWarning("Space pressed !");
			}
        }

		void LateUpdate()
		{
			Debug.Log("LateUpdate : " + Time.deltaTime);
		}

		// Physics Loop
		void FixedUpdate()
		{
			Debug.Log("FixedUpdate : " + Time.fixedDeltaTime);
		}
	}
}