using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Treasures;
using Buffs;
using Boosters;

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

        [HideInInspector]
        public BuffMechanics BuffMechanics;

        public HashSet<Treasure> boardedTreasures = new HashSet<Treasure>();
        public HashSet<Treasure> securedTreasures = new HashSet<Treasure>();


        public List<BoosterBase> Boosters { get; set; }

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



        private bool isKilled = false;
		public void Kill()
		{
            // Trigger sinking animation?
            if (!isKilled)
            {
                GameController.Instance.PlayerDied(this);
                GameObject.Destroy(gameObject);
                isKilled = true;
            }
        }
    }
}