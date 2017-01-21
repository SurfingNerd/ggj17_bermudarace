using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

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
	public int numTreasuresUntilApocalypse = 6;

	List<Player> players;
	private int numTreasuresPickedUpTotal = 0;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		players = new List<Player>();
		CreatePlayer(1);
		CreatePlayer(2);
	}

	private Player CreatePlayer(int id)
	{
		int prefabIndex = id;
		if (id >= PlayerPrefabs.Count) prefabIndex = 1;

		GameObject playerGO = GameObject.Instantiate(PlayerPrefabs[prefabIndex]);
		if(playerGO != null)
		{
			Player player = playerGO.GetComponent<Player>();
			players.Add(player);

			// randomize position
			player.transform.position = new Vector2(UnityEngine.Random.Range(-4, 4), UnityEngine.Random.Range(-3, 3));

			return player;
		} else
		{
			throw new Exception("Could not instantiate player " + id);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	// TODO implement
	public void PlayerPickedUpTreasure(Player player, Treasure treasure)
	{
		numTreasuresPickedUpTotal++;
		Debug.Log("Player " + player.id + " picked up treasure " + treasure.id);

		if(numTreasuresPickedUpTotal >= numTreasuresUntilApocalypse)
		{
			Debug.Log("APOCALPYPSE INCOMING! (todo)");

			// Trigger big Cthulhu - instakill on contact
			// Trigger a few Deep Ones on islands - slow down players if close
			// Trigger apocalypse camera filter
			// Trigger apocalypse music
			
			// Activate Death trigger at left screen edge
			// Trigger Camera autoscroll
		}
		else
		{
			// Trigger some minor obstacle (at treasure position or random?)
			// Trigger music change? stinger or ramp up tension level
		}
	}
}
