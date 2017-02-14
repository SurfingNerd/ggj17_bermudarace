using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Players
{
    public abstract class PlayerInteractionBase : MonoBehaviour
    {
        //protected abstract String GetObjectName();
        protected HashSet<Player> mCurrentPlayers = new HashSet<Player>();

        protected abstract void OnPlayerEntered(Player player);



        protected abstract void OnPlayerLeft(Player player);
        

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.transform.parent != null)
            {
                Player otherPlayer = collider.transform.parent.GetComponent<Player>();
                if (otherPlayer != null)
                {
                    //Debug.Log("Player " + otherPlayer.Name + " entered " + this.name);
                    mCurrentPlayers.Add(otherPlayer);
                    OnPlayerEntered(otherPlayer);
                }
                else
                {
                    Debug.Log("Unknown Object " + collider.transform.parent.name + " entered " + this.name);
                }
            }
            else
            {
                Debug.Log("Unknown Object " + collider.name + " entered " + this.name);
            }
        }

        public void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.transform.parent != null)
            {
                Player otherPlayer = collider.transform.parent.GetComponent<Player>();
                if (otherPlayer != null)
                {
                    Debug.Log("Player " + otherPlayer.Name + " entered " + this.name);
                    mCurrentPlayers.Remove(otherPlayer);
                    OnPlayerLeft(otherPlayer);
                }
                else
                {
                    Debug.Log("Unknown Object " + collider.transform.parent.name + " exited " + this.name);
                }
            }
        }
    }
}
