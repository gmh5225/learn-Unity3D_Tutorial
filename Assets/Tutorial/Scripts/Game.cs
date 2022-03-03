/******************************************************************************************************************************************************
* MIT License																																		  *
*																																					  *
* Copyright (c) 2020																																  *
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

public class Game : MonoBehaviour
{
	public int nbBonus = 10;
	public Terrain terrain;
	public Bonus bonusPrefab;
	public AudioSource backgroundMusic;
	public float pitchChangeSmoothTime = 0.01f;
	public UnityEngine.UI.Text infos_Text;
	public UnityEngine.UI.Button newGame_Button;

	private Player _player;
	private int _nbBonusLeft;

	void Start()
	{
		_player = FindObjectOfType<Player>();

		newGame_Button.onClick.AddListener(() =>
		{
			newGame_Button.gameObject.SetActive(false);
			_NewGame();
		});

		_ShowNewGameButton();
	}

	void Update()
	{
		float pitch = Mathf.Max(1f, _player.enabled ? _player.TranslationSpeed / _player.translationSpeed : 1f);
		float pitchVelocity = 0f;
		backgroundMusic.pitch = Mathf.SmoothDamp(backgroundMusic.pitch, pitch, ref pitchVelocity, pitchChangeSmoothTime);
		//Debug.Log("Pitch : " + pitch);

		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	// V1
	//public void BonusCatched(Bonus pBonus)
	private void _BonusCatched(Bonus pBonus)
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
			_ShowNewGameButton();
		}
	}

	private void _NewGame()
	{
		if (_player.useMouse)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		_player.enabled = true;

		_nbBonusLeft = nbBonus;
		//Debug.Log("Bonus left : " + nbBonusLeft);
		infos_Text.text = "Bonus left : " + _nbBonusLeft;

		// Create Bonus all over the Terrain.
		Vector3 terrainPos = terrain.transform.position;
		Vector3 bonusPos = Vector3.zero;
		float minHeight = bonusPrefab.transform.localScale.y * 0.5f;
		float maxHeight = minHeight + (_player.transform.localScale.y * 0.5f) + _player.jumpHeight - 0.1f;
		for (int i = 0; i < nbBonus; ++i)
		{
			bonusPos.x = Random.Range(terrainPos.x + 1.0f, terrainPos.x + terrain.terrainData.size.x - 1.0f);
			bonusPos.z = Random.Range(terrainPos.z + 1.0f, terrainPos.z + terrain.terrainData.size.z - 1.0f);
			bonusPos.y = 0.0f;
			// Put the bonus object 1m above the terrain.
			bonusPos.y = terrain.SampleHeight(bonusPos) + Random.Range(minHeight, maxHeight);

			// Create a bonus object at Position bonusPos.
			// Quaternion.identity == zero rotation.
			Bonus bonus = GameObject.Instantiate(bonusPrefab, bonusPos, Quaternion.identity);
			bonus.PlayerEntered += _BonusCatched;
		}
	}

	private void _ShowNewGameButton()
	{
		newGame_Button.gameObject.SetActive(true);
		_player.enabled = false;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
