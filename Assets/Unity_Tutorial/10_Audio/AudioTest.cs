using UnityEngine;

public class AudioTest : MonoBehaviour
{
	public AudioSource audioSource;

	private void Update()
	{
		// Play / Pause
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (audioSource.isPlaying)
			{
				audioSource.Pause();
				Debug.Log("Audio paused");
			}
			else
			{
				audioSource.Play();
				Debug.Log("Audio plays");
			}
		}
	}
}
