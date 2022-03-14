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
	public float pushPower = 1.5f; // simulation living & stable with RB.drag = 0.25f

	// Catch events with collision only if (collider.isTrigger = false).
	// Useful to apply some forces when objects collides.
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.name + " collides with " + gameObject.name);
		if(pushPower != 0f)
		{
			collision.rigidbody.AddForce(collision.impulse * pushPower, ForceMode.Impulse);
		}
	}

	// Catch events with collision only if (collider.isTrigger = false).
	void OnCollisionExit(Collision collision)
	{
		Debug.Log(collision.gameObject.name + " stop collides with " + gameObject.name);
	}
}
