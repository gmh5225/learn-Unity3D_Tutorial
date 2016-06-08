using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public int nbBonus = 10;
	public Terrain Terrain;
	public GameObject BonusPrefab;
	public UnityEngine.UI.Text info;

	int nbBonusLeft;
	
	// Update is called once per frame
	void Start()
	{
		nbBonusLeft = nbBonus;
		//Debug.Log("Bonus left : " + nbBonusLeft);
		info.text = "Bonus left : " + nbBonusLeft;

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

	public void BonusCatched()
	{
		--nbBonusLeft;
		//Debug.Log("Bonus left : " + nbBonusLeft);
		info.text = "Bonus left : " + nbBonusLeft;

		if (nbBonusLeft == 0)
		{
			//Debug.Log("Game Finished !");
			info.text = "Game Finished !";
		}
	}
}
