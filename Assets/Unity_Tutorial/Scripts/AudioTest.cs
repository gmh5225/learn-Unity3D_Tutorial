using UnityEngine;

public class AudioTest : MonoBehaviour
{
	public AudioSource audioSource;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(audioSource.isPlaying)
			{
				audioSource.Stop();
			}
			else
			{
				audioSource.Play();
			}
		}
	}
}
