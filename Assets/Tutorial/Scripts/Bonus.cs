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

using System;
using UnityEngine;

public class Bonus : MonoBehaviour
{
	public AudioClip collectedAudioClip;
	//private AudioSource _audioSource;
	private Collider _collider;
	// V1
	//private Game _game;

	// V2 : remove Game dependency.
	public Action<Bonus> PlayerEntered;

	void Awake()
	{
		//_audioSource = GetComponent<AudioSource>();
		_collider = GetComponent<Collider>();
		_collider.isTrigger = true;

		// V1
		//_game = FindObjectOfType<Game>();
	}

	void Update ()
	{
		// Unity tool.
		transform.Rotate(transform.up, 360.0f * Time.deltaTime);

		// Quaternion Method
		//transform.rotation *= Quaternion.AngleAxis(360.0f * Time.deltaTime, Vector3.up);

		// Euler method (less reliable => Gimbal Lock).
		//Vector3 euler = transform.eulerAngles;
		//euler.y += 360.0f * Time.deltaTime;
		//transform.eulerAngles = euler;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			AudioSource.PlayClipAtPoint(collectedAudioClip, transform.position);
			//_audioSource.Play(); // destroyed before clip played.
			Destroy(gameObject);

			//Debug.Log("Player Enter !");
			if (PlayerEntered != null)
			{
				PlayerEntered(this);
			}

			// V1
			//_game.BonusCollected(this);
		}
	}

	//void OnTriggerExit(Collider other)
	//{
	//	if (other.tag == "Player")
	//	{
	//		Debug.Log("Player Exit !");
	//	}
	//}
}
