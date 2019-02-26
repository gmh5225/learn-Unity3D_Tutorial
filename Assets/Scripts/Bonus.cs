using UnityEngine;

public class Bonus : MonoBehaviour
{
	Game _game;
	Collider _collider;

	void Start()
	{
		_game = FindObjectOfType<Game>();

		_collider = GetComponent<Collider>();
		_collider.isTrigger = true;
	}

	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(transform.up, 360.0f * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			//Debug.Log("Player Enter !");
			_game.BonusCatched(this);
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
