/******************************************************************************************************************************************************
* MIT License																																		  *
*																																					  *
* Copyright (c) 2020																																  *
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

public class CollisionTest : MonoBehaviour
{
	public float power = 1.0f;

	// Catch events with collision (collider.isTrigger = false).
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.name + " started collision !");
		collision.rigidbody.AddForce(collision.impulse * power, ForceMode.Impulse);
	}

	void OnCollisionExit(Collision collision)
	{
		Debug.Log(collision.gameObject.name + " stopped collision !");
	}

	// Catch events only (collider.isTrigger = true).
	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.gameObject.name + " entered !");
	}

	void OnTriggerExit(Collider other)
	{
		Debug.Log(other.gameObject.name + " exited !");
	}
}
