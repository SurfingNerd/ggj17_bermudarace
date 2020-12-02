using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game;
using Players;

public class WinScreenSetText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Player winningPlayer = null;
		if (GameController.Instance != null)
		{
			winningPlayer = GameController.Instance.getWinningPlayer();
		}

		
		UnityEngine.UI.Text text = GetComponent<UnityEngine.UI.Text>();
		
		if (winningPlayer != null && text != null)
		{
			text.text = text.text.Replace("[P]", winningPlayer.Name);
			text.text = text.text.Replace("[T]", winningPlayer.securedTreasures.Count+"");
		}
		else
		{
			Debug.Log("winningPlayer not found");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
