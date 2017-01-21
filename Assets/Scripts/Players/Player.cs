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

		//void CraneAction()
		//{
		//	requestpickup = true;
		//}
    }
}