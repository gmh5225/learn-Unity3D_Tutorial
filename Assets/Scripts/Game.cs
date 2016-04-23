using UnityEngine;

public class Game : MonoBehaviour
{
	public int nbBonus = 10;
	public GameObject bonusPrefab;

	private Terrain _terrain;
	private int _bonusLeft;
	private UnityEngine.UI.Text _uiText;
	private bool _gameFinished;

	// public float Speed { get; set; } // Acces que dans le code

	public void UpdateScore()
	{
		string msg = string.Format("Bonus : {0} / {1}", --_bonusLeft, nbBonus);
		Debug.Log(msg);
		_uiText.text = msg;
    }

	void Awake()
	{
		_gameFinished = false;
		_bonusLeft = nbBonus;

		_uiText = GetComponentInChildren<UnityEngine.UI.Text>();
		_uiText.text = string.Format("Bonus : {0} / {1}", _bonusLeft, nbBonus);

		_terrain = FindObjectOfType<Terrain>();

		Vector3 terrainPos = _terrain.transform.position;
		Vector3 pos = Vector3.zero;
		for(int i=0; i < nbBonus; ++i)
		{
			pos.x = Random.Range(terrainPos.x + 1.0f, terrainPos.x + _terrain.terrainData.size.x - 1.0f);
			pos.z = Random.Range(terrainPos.z + 1.0f, terrainPos.z +_terrain.terrainData.size.z - 1.0f);
			pos.y = 0.0f;
			pos.y = _terrain.SampleHeight(pos) + 1.0f;

			GameObject.Instantiate(bonusPrefab, pos, Quaternion.identity);
		}
    }
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}

		if ( (!_gameFinished) && (_bonusLeft < 1) )
		{
			Debug.Log("Game Finished !");
			_uiText.text = "Game Finished !";
			_gameFinished = true;
		}
	}
}
