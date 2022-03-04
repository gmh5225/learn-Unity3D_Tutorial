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
