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

public class Footsteps : MonoBehaviour
{
	public float slidingFootstepsVolume = 1f;
	public AudioClip slidingFootstepsClip;
	public float jumpLandVolume = 2f;
	public AudioClip jumpClip;
	public AudioClip landClip;
	public float footStepsVolume = 0.5f;
	public AudioClip[] footstepsClips;
	private AudioSource _audioSource;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
		_audioSource.clip = slidingFootstepsClip;
	}

	// This one is called automatically thanks to Animation Events in Walk animation.
	public void PlayFootStep()
	{
		_audioSource.PlayOneShot(footstepsClips[Random.Range(0, footstepsClips.Length)], footStepsVolume);
	}

	// These ones are called from the Player script directly.
	public void UpdateSlidingFootStep(float pSpeed)
	{
		if(slidingFootstepsClip != null)
		{
			// Play manually continuously.
			_audioSource.Play();
			_audioSource.time = Mathf.Repeat(_audioSource.time + (pSpeed * Time.deltaTime), slidingFootstepsClip.length);
			//Debug.Log("AudioSource time : " + _audioSource.time);
		}
	}

	public void PlayJump()
	{
		_audioSource.PlayOneShot(jumpClip, jumpLandVolume);
	}

	public void PlayLand()
	{
		_audioSource.PlayOneShot(landClip, jumpLandVolume);
	}
}
