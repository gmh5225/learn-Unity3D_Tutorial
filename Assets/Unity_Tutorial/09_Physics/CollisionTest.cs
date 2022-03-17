﻿/******************************************************************************************************************************************************
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
	public float pushForce = 1.5f;

	// Called when another object enters in collision with the physical shape (Collider) of this object.
	// Useful to apply some forces when objects collide.
	// Conditions for this method to be called :
	// - this Collider.isTrigger should be false 
	// - the other gameObject should have a rigidbody component
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(string.Format("{0} enters in collision with {1}", collision.rigidbody.name, gameObject.name));
		if (pushForce != 0f)
		{
			collision.rigidbody.AddForce(collision.impulse * pushForce, ForceMode.Impulse);
		}
	}

	// Called when another object exits the collision with the physical shape (Collider) of this object.
	// Conditions for this method to be called :
	// - this Collider.isTrigger should be false 
	// - the other gameObject should have a rigidbody component
	void OnCollisionExit(Collision collision)
	{
		Debug.Log(string.Format("{0} exits the collision with {1}", collision.rigidbody.name, gameObject.name));
	}
}