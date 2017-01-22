using Game;
using Players;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			Player player = other.gameObject.GetComponent<Player>();
			if (player == null) player = other.gameObject.GetComponentInParent<Player>();

			GameController.Instance.PlayerReachedGoal(player);
		}
	}
}
