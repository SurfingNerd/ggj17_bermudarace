using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Treasures;


namespace Game
{
    public class GameController : MonoBehaviour
    {
        public GameController Instance;

        public HashSet<Treasure> Treasures;

	    // Use this for initialization
	    void Start()
        {
            Instance = this;

        }

        // Update is called once per frame
        void Update() {

        }
    }
}