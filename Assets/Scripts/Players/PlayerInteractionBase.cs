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

        protected virtual void OnPlayerEntered(Player player)
        { 
        }

        protected virtual void OnPlayerLeft(Player player)
        {

        }


        public void OnTriggerEnter2D(Collider2D collider)
        {
            Player otherPlayer = collider.gameObject.transform.parent.GetComponent<Player>();
            if (otherPlayer != null)
            {
                Debug.Log("Player " + otherPlayer.Name + " entered " + this.name);
                mCurrentPlayers.Add(otherPlayer);
                OnPlayerEntered(otherPlayer);
            }
            else
            {
                Debug.Log("Unknown Object " + collider.transform.parent.name + " entered " + this.name);
            }
        }

        public void OnTriggerExit2D(Collider2D collider)
        {
            Player otherPlayer = collider.gameObject.transform.parent.GetComponent<Player>();
            if (otherPlayer != null)
            {
                Debug.Log("Player " + otherPlayer.Name + " entered " + this.name);

                mCurrentPlayers.Remove(otherPlayer);
                OnPlayerEntered(otherPlayer);
            }
            else
            {
                Debug.Log("Unknown Object " + collider.transform.parent.name + " exited " + this.name);
            }
        }
    }
}
