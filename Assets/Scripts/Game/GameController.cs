﻿using UnityEngine;
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
		public HashSet<Treasure> Treasures;
		public int numTreasuresUntilApocalypse = 6;

		private List<Player> players;
		private int numTreasuresPickedUpTotal = 0;

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

				SetInputs(player, id);

				players.Add(player);
				return player;
			}
			else
			{
				throw new Exception("Could not instantiate player " + id);
			}
		}

		void SetInputs(Player player, int id)
		{
			switch(id)
			{
				case 1:
					// use defaults
					break;
				case 2:
					player.Input.KCUp = KeyCode.W;
					player.Input.KCDown = KeyCode.S;
					player.Input.KCLeft = KeyCode.A;
					player.Input.KCRight = KeyCode.D;
					break;
			}
		}

		// Update is called once per frame
		void Update()
		{

		}

		// TODO implement
		public void PlayerPickedUpTreasure(Player player, Treasure treasure)
		{
			numTreasuresPickedUpTotal++;
			Debug.Log("Player " + player.Name + " picked up treasure " + treasure.Name);
			Debug.Log("Total treasures picked up: " + numTreasuresPickedUpTotal + " (" + numTreasuresUntilApocalypse + " until Cthulhu apocalypse)");

			if (numTreasuresPickedUpTotal >= numTreasuresUntilApocalypse)
			{
				TriggerApocalypse();
			}
			else
			{
				TreasurePickupConsequence();
			}
		}

		private void TriggerApocalypse()
		{
			Debug.Log("APOCALPYPSE INCOMING! (todo)");

			// Trigger big Cthulhu - instakill on contact
			// Trigger a few Deep Ones on islands - slow down players if close
			// Trigger apocalypse camera filter
			// Trigger apocalypse music

			// Activate Death trigger at left screen edge
			// Trigger Camera autoscroll
		}

		private void TreasurePickupConsequence()
		{
			Debug.Log("Treasure pickup consequence... (todo)");

			// Trigger some minor obstacle (at treasure position or random?)
			// Trigger music change? stinger or ramp up tension level
		}
	}
}