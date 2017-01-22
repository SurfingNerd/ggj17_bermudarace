using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Players;
using Game;

public class KillPlayer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			Player player = other.GetComponent<Player>();
			if (player == null) player = other.GetComponentInParent<Player>();
			if (player != null)
			{
				player.Kill();
			}
			else
			{
				Debug.Log("player to kill not found!");
			}
		}
	}

}
