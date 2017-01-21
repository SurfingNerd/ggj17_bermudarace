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
			GameController.Instance.SendMessage("PlayerReachedGoal", other.gameObject.GetComponent<Player>());
		}
	}
}
