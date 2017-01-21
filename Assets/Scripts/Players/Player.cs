using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

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

		public bool requestpickup = false;

		void Start()
		{
			Input = gameObject.GetComponent<PlayerControl>();
		}
		
        // Update is called once per frame
        //void Update()
        //{

        //}

		void CraneAction()
		{
			requestpickup = true;
		}

		// pass param if relevant what the treasure is
		void CollectedTreasure()
		{
			numCollectedTreasures++;
		}
    }
}