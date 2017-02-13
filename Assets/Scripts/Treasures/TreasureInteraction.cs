using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using System;
using Game;

namespace Treasures
{


    public class TreasureInteraction : PlayerInteractionBase
    {
        public Treasure Treasure;

        public TreasureType Type = TreasureType.Speed;

        private Player currentElevatingPlayer;
        //how many % the treasure is picked up.
        private double currentPlayersSpentElevationTime;
        

        public TreasureInteraction()
        {
        }

        protected override void OnPlayerLeft(Player player)
        {
            base.OnPlayerLeft(player);
            if (currentElevatingPlayer == player)
            {
                currentElevatingPlayer = null;
                currentPlayersSpentElevationTime = 0;
                //Debug.Log("Canceling collecting " + Treasure.Name + " , because " + player.Name + " has left the Area.");
            }
        }

        protected override void OnPlayerEntered(Player player)
        {
            if (currentElevatingPlayer == null && Treasure != null)
            {
                Debug.Log(player.Name + "starts to collect" + Treasure.Name);
                currentElevatingPlayer = player;
                currentPlayersSpentElevationTime = Time.deltaTime;
                //Treasure
            }
        }

        void Start()
        {
            GameController.Instance.TreasureMechanics.InitTreasureInteraction(this);            
        }


        void Update()
        {
            if ( currentElevatingPlayer != null)
            {
                //double spentElevationTime = Time.deltaTime / GameController.Instance.TreasureMechanics.secondsToElevateATreasure;
                currentPlayersSpentElevationTime += Time.deltaTime;

                if (currentPlayersSpentElevationTime >= GameController.Instance.TreasureMechanics.secondsToElevateATreasure * (1 / currentElevatingPlayer.BuffMechanics.currentTreasurePickupSpeedModifier))
                {
                    Debug.Log(currentElevatingPlayer.Name + " got Treasure " + Treasure.Name);
                    GameController.Instance.TreasureMechanics.PlayerPickedUpTreasure(currentElevatingPlayer, Treasure);
                    currentPlayersSpentElevationTime = 0;
                    currentElevatingPlayer = null;

                    Deactivate();

                    //TODO: deactivate this spot, since treasure is in ship now.
                    //it might get reactivated and replaced, if Boat gets destroyed.
                }
            }
        }

        void Deactivate()
        {
            Treasure = null;
            gameObject.SetActive(false);

            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    Transform childTransform = transform.GetChild(i);
            //    childTransform.gameObject.SetActive(false);
            //    Debug.Log("Setting inactive.");
            //};

        }
        
        //protected HashSet<Player> mCurrentPlayers = new HashSet<Player>();
    }
}
