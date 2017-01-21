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

		public PlayerControl Input;

        public GameController GameController;

		void Start()
		{
			Input = gameObject.GetComponent<PlayerControl>();
		}
		
        // Update is called once per frame
        //void Update()
        //{

        //}
    }
}