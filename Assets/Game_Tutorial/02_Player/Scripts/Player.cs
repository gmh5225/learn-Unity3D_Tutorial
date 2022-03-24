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

#define PLAYER_CORRECTION

using UnityEngine;

// Configure every components + speed up music
public class Player : MonoBehaviour
{
	public CharacterMovement characterMovement;
	public CameraMovement cameraMovement;

	// Exercise 01:	Create a boolean variable "cameraControl" to enable/disable the control of the camera at Start of the application.
	// - if "cameraControl" is true : the character should strafe + CameraMovement script should be enabled
	// - if "cameraControl" is false : the character shouldn't strafe + CameraMovement script should be disabled

#if PLAYER_CORRECTION
	// Exercise 01
	public bool cameraControl = true;

	private void Start()
	{
		cameraMovement.enabled = characterMovement.strafe = cameraControl;
	}
#endif
}