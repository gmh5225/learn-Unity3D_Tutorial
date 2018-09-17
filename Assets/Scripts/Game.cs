using UnityEngine;

public class Game : MonoBehaviour {

	public int nbBonus = 10;
	public Terrain Terrain;
	public GameObject BonusPrefab;
	public UnityEngine.UI.Text infos_Text;
	public UnityEngine.UI.Button restart_Button;

	private int _nbBonusLeft;
	
	// Update is called once per frame
	private void Start()
	{
		restart_Button.onClick.AddListener(_Restart);
		restart_Button.gameObject.SetActive(false);

		_nbBonusLeft = nbBonus;
		//Debug.Log("Bonus left : " + nbBonusLeft);
		infos_Text.text = "Bonus left : " + _nbBonusLeft;

		// Create Bonus all over the Terrain.
		Vector3 terrainPos = Terrain.transform.position;
		Vector3 bonusPos;
		for(int i=0; i < nbBonus; ++i)
		{
			bonusPos.x = Random.Range(terrainPos.x + 1.0f, terrainPos.x + Terrain.terrainData.size.x - 1.0f);
			bonusPos.z = Random.Range(terrainPos.z + 1.0f, terrainPos.z + Terrain.terrainData.size.z - 1.0f);
			bonusPos.y = 0.0f;
			// Here we have a bonus object on the ground (y = 0).
			bonusPos.y = Terrain.SampleHeight(bonusPos) + 1.0f;

			// Create a bonus object at Position bonusPos.
			// Quaternion.identity == zero rotation.
			GameObject.Instantiate(BonusPrefab, bonusPos, Quaternion.identity);
		}
	}

	private void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void BonusCatched()
	{
		--_nbBonusLeft;
		//Debug.Log("Bonus left : " + nbBonusLeft);
		infos_Text.text = "Bonus left : " + _nbBonusLeft;

		if (_nbBonusLeft == 0)
		{
			//Debug.Log("Game Finished !");
			infos_Text.text = "Game Finished !";
			restart_Button.gameObject.SetActive(true);
		}
	}

	private void _Restart()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}
}
