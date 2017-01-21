using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

using Treasures;
using Players;

namespace Game
{
	public class GameController : MonoBehaviour
	{
		private static GameController instance;
		public static GameController Instance
		{
			get
			{
				if (instance == null)
				{
					instance = GameObject.FindObjectOfType<GameController>();
				}
				return instance;
			}
		}

		public List<GameObject> PlayerPrefabs;

		private List<Player> players;


        void Awake()
		{
			instance = this;
		}

		// Use this for initialization
		void Start()
		{
			players = new List<Player>();
			CreatePlayer(1);
			CreatePlayer(2);
		}

		private Player CreatePlayer(int id)
		{
			int prefabIndex = id;
			if (id >= PlayerPrefabs.Count) prefabIndex = 1;

			GameObject playerGO = GameObject.Instantiate(PlayerPrefabs[prefabIndex]);
			if (playerGO != null)
			{
				Player player = playerGO.GetComponent<Player>();
				player.Name = "Player " + id;
				
				// randomize position
				playerGO.transform.position = new Vector2(UnityEngine.Random.Range(-4, 4), UnityEngine.Random.Range(-3, 3));

				PlayerControl playerControl = playerGO.GetComponent<PlayerControl>();
				SetInputs(playerControl, id);

				players.Add(player);
				return player;
			}
			else
			{
				throw new Exception("Could not instantiate player " + id);
			}
		}

		void SetInputs(PlayerControl playerControl, int id)
		{
			switch(id)
			{
				case 1:
					// use defaults
					break;
				case 2:
					playerControl.KCUp = KeyCode.W;
					playerControl.KCDown = KeyCode.S;
					playerControl.KCLeft = KeyCode.A;
					playerControl.KCRight = KeyCode.D;

					playerControl.KCThrottle = KeyCode.LeftControl;
					playerControl.KCAction1 = KeyCode.E;
					break;
			}
		}

		// Update is called once per frame
		void Update()
		{

		}

	
	}
}
