using Game;
using Players;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameController.Instance != null)
			{
				Player winningPlayer = null;
				if (GameController.Instance != null)
				{
					winningPlayer = GameController.Instance.getWinningPlayer();
					if(winningPlayer != null)
					{
						GameObject.Destroy(winningPlayer.gameObject);
					}

				}

				GameObject.Destroy(GameController.Instance.gameObject);
			}
			SceneManager.LoadScene("00_titlescreen");
		}
	}
}
