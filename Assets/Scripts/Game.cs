/******************************************************************************************************************************************************
* MIT License																																		  *
*																																					  *
* Copyright (c) 2020																																  *
* Emmanuel Badier <emmanuel.badier@gmail.com>																										  *
* 																																					  *
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),  *
* to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,  *
* and/or sell copies of the Software, and to permit persons to whom the Software isfurnished to do so, subject to the following conditions:			  *
* 																																					  *
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.					  *
* 																																					  *
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, *
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 																							  *
* IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 		  *
* TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.							  *
******************************************************************************************************************************************************/

using UnityEngine;

public class Game : MonoBehaviour
{
	public int nbBonus = 10;
	public Terrain terrain;
	public GameObject BonusPrefab;
	public UnityEngine.UI.Text infos_Text;
	public UnityEngine.UI.Button newGame_Button;

	private int _nbBonusLeft;
	
	void Start()
	{
		newGame_Button.onClick.AddListener(() =>
		{
			newGame_Button.gameObject.SetActive(false);
			_NewGame();
		});
	}

	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void BonusCatched(Bonus pBonus)
	{
		// Destroy Bonus
		Object.Destroy(pBonus.gameObject);

		// Update Score
		--_nbBonusLeft;
		//Debug.Log("Bonus left : " + nbBonusLeft);
		infos_Text.text = "Bonus left : " + _nbBonusLeft;

		if (_nbBonusLeft <= 0)
		{
			//Debug.Log("Game Finished !");
			infos_Text.text = "Game Finished !";
			newGame_Button.gameObject.SetActive(true);
		}
	}

	private void _NewGame()
	{
		_nbBonusLeft = nbBonus;
		//Debug.Log("Bonus left : " + nbBonusLeft);
		infos_Text.text = "Bonus left : " + _nbBonusLeft;

		// Create Bonus all over the Terrain.
		Vector3 terrainPos = terrain.transform.position;
		Vector3 bonusPos = Vector3.zero;
		for (int i = 0; i < nbBonus; ++i)
		{
			bonusPos.x = Random.Range(terrainPos.x + 1.0f, terrainPos.x + terrain.terrainData.size.x - 1.0f);
			bonusPos.z = Random.Range(terrainPos.z + 1.0f, terrainPos.z + terrain.terrainData.size.z - 1.0f);
			bonusPos.y = 0.0f;
			// Put the bonus object 1m above the terrain.
			bonusPos.y = terrain.SampleHeight(bonusPos) + 1.0f;

			// Create a bonus object at Position bonusPos.
			// Quaternion.identity == zero rotation.
			GameObject.Instantiate(BonusPrefab, bonusPos, Quaternion.identity);
		}
	}
}
