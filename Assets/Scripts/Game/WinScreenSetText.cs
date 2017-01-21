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
		GUIText text = GetComponent<GUIText>();
		
		if (winningPlayer != null && text != null)
		{
			text.text = text.text.Replace("[P]", winningPlayer.Name);
			text.text = text.text.Replace("[T]", winningPlayer.NumCollectedTreasures+"");
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
