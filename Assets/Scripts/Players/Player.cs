using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Treasures;
using Assets.Buffs;

namespace Players
{
    public class Player : MonoBehaviour
	{
		public string Name;

		//HashSet<Treasure>
		private int numCollectedTreasures = 0;
		public int NumCollectedTreasures
		{
			get { return numCollectedTreasures; }
			private set { }
		}

		[HideInInspector]
		public PlayerControl Input;
        public BuffMechanics BuffMechanics;

        public HashSet<Treasure> boardedTreasures = new HashSet<Treasure>();
        public HashSet<Treasure> securedTreasures = new HashSet<Treasure>();

        

        

		void Awake()
		{
			Input = gameObject.GetComponent<PlayerControl>();
            BuffMechanics = gameObject.GetComponent<BuffMechanics>();
		}


		
        // Update is called once per frame
        //void Update()
        //{

        //}

		//void CraneAction()
		//{
		//	requestpickup = true;
		//}

		public void Kill()
		{
			// Trigger sinking animation?

			GameController.Instance.PlayerDied(this);
            GameObject.Destroy(gameObject);
        }
    }
}