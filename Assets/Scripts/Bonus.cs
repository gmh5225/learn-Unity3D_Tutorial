using UnityEngine;

public class Bonus : MonoBehaviour
{
	private Game _game;
	private Collider _collider;

	void Awake()
	{
		_game = FindObjectOfType<Game>();
		_collider = GetComponent<Collider>();
		_collider.isTrigger = true;
    }

	void Update()
	{
		transform.Rotate(transform.up, 540.0f * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			//Debug.Log("Player Enter");
			GameObject.Destroy(this.gameObject);
			_game.UpdateScore();
		}
	}

	//void OnTriggerExit(Collider other)
	//{
	//	if (other.tag == "Player")
	//	{
	//		Debug.Log("Player Exit");
	//	}
	//}
}
