using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;


namespace Treasures
{
    public class TreasureInteraction : MonoBehaviour
    {
        public Treasure Treasure;

        protected HashSet<Player> mCurrentPlayers = new HashSet<Player>();

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            Player otherPlayer = collider.gameObject.transform.parent.GetComponent<Player>();
            if (otherPlayer != null)
            {
                Debug.Log("Player " + otherPlayer.Name + " entered Treasure Area " + Treasure.Name);
                mCurrentPlayers.Add(otherPlayer);
            }
            else
            {
                Debug.Log("Unknown Object " + collider.transform.parent.name + " entered Treasure Area " + Treasure.Name);
            }
        }

        public void OnTriggerExit2D(Collider2D collider)
        {
            Player otherPlayer = collider.gameObject.transform.parent.GetComponent<Player>();
            if (otherPlayer != null)
            {
                Debug.Log("Player " + otherPlayer.Name + " exited  Treasure Area " + Treasure.Name);
                mCurrentPlayers.Remove(otherPlayer);
            }
            else
            {
                Debug.Log("Unknown Object " + collider.transform.parent.name + " exited Treasure Area " + Treasure.Name);
            }
        }
    }
}
