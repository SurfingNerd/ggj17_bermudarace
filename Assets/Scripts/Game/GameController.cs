using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

using Treasures;
using Players;
using UnityEngine.SceneManagement;

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

		private Player winningPlayer = null;

        [HideInInspector]
		public TreasureMechanics TreasureMechanics;

		public GameObject Cthulhu;

        public int Players = 1;

        public GameMode GameMode = GameMode.Coop;

        void Awake()
		{
			instance = this;
			//DontDestroyOnLoad(gameObject);

			TreasureMechanics = GetComponent<TreasureMechanics>();
		}

		// Use this for initialization
		void Start()
		{
			winningPlayer = null;

			players = new List<Player>();

            for (int i = 1; i <= Players; i++)
            {
                int numConnectedJoysticks = Input.GetJoystickNames().Length;

                if (i <= numConnectedJoysticks || numConnectedJoysticks == 0)
                {
                    CreatePlayer(i);
                }
            }
			//CreatePlayer(1);
			//CreatePlayer(2);
        }

		public void Reset()
		{
			foreach(Player player in players)
			{
				GameObject.Destroy(player.gameObject);
			}
			players.Clear();
            Start();
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
                //playerGO.transform.position = new Vector2(UnityEngine.Random.Range(-4, 4), UnityEngine.Random.Range(-3, 3));
                playerGO.transform.position = new Vector2(id * 0.5f, 0);

                PlayerControl playerControl = playerGO.GetComponent<PlayerControl>();
				SetInputs(playerControl, id);
                playerControl.player = player;
                players.Add(player);

                Debug.Log("Adding Player " + player.Name);


				return player;
			}
			else
			{
				throw new Exception("Could not instantiate player " + id);
			}
		}

		void SetInputs(PlayerControl playerControl, int id)
		{
            playerControl.joystickIndex = id;
            switch (id)
			{
				case 1:
					// use defaults
					break;
				case 2:
					playerControl.KCUp = KeyCode.W;
					playerControl.KCDown = KeyCode.S;
					playerControl.KCLeft = KeyCode.A;
					playerControl.KCRight = KeyCode.D;

					playerControl.KCThrottle = KeyCode.LeftShift;
					playerControl.KCAction1 = KeyCode.E;
					
					break;
			}
		}

		// Update is called once per frame
		void Update()
		{
			// DEBUG
			if(Input.GetKeyDown(KeyCode.T))
			{
				TriggerApocalypse();
			}

			if(Input.GetKeyDown(KeyCode.Escape))
			{
				SceneManager.LoadScene("00_titlescreen");
			}
		}

        bool mApocolypeseIsTriggered;
		public void TriggerApocalypse()
		{

            if (!mApocolypeseIsTriggered)
            {
                mApocolypeseIsTriggered = true;

                Debug.Log("APOCALPYPSE INCOMING! (todo)");

                // Trigger big Cthulhu - instakill on contact
                GameObject cthulhu = GameObject.Instantiate(Cthulhu);
                cthulhu.transform.parent = Camera.main.transform;

                // Trigger a few Deep Ones on islands - slow down players if close
                // ?

                // Trigger apocalypse camera filter
                // ?

                // Trigger apocalypse music
                SendMessage("SoundTheApocalypseNow");

                //AudioSource normalMusic = Camera.current.GetComponent<AudioSource>();
                //if (normalMusic != null)
                //{
                //    normalMusic.Stop();
                //}



                // Trigger Camera autoscroll
                Autoscroll autoscroll = Camera.main.GetComponent<Autoscroll>();
                if (autoscroll != null)
                {
                    autoscroll.enabled = true;
                }

                // Activate Death trigger at left screen edge
                Transform killbar = Camera.main.transform.FindChild("AutoscrollKillBar");
                if (killbar != null)
                {
                    killbar.gameObject.SetActive(true);
                }
            }
		}

        public void PlayerReachedGoal(Player player)
		{
			Debug.Log("Player " + player.Name + " wins!");

			winningPlayer = player;
			DontDestroyOnLoad(winningPlayer);

			SceneManager.LoadScene("WinScreen");
		}

		public Player getWinningPlayer()
		{
			return winningPlayer;
		}

		public void PlayerDied(Player player)
		{
			Debug.Log("Player " + player.Name + " died!");
			players.Remove(player);

			if(players.Count == 1 && GameMode == GameMode.Vs)
			{
				PlayerReachedGoal(players[0]);
			}

            if (players.Count == 0)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
		}
	}
}
