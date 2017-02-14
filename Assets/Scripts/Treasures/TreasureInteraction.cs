using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using System;
using Game;
using UI;

namespace Treasures
{
   
    public class TreasureInteraction : PlayerInteractionBase
    {
        public Treasure Treasure;

        public TreasureType Type = TreasureType.Speed;

        private Player currentElevatingPlayer;

        private Queue<Player> mWaitingPlayers = new Queue<Player>();

        //how many % the treasure is picked up.
        private float currentPlayersSpentElevationTime;

        GameObject mCurrentProgressBar;
        ProgressBar mCurrentProgressBarLogic;

        public TreasureInteraction()
        {
        }

        protected override void OnPlayerLeft(Player player)
        {
            if (currentElevatingPlayer == player)
            {
                currentElevatingPlayer = null;
                currentPlayersSpentElevationTime = 0;
                //Debug.Log("Canceling collecting " + Treasure.Name + " , because " + player.Name + " has left the Area.");
            }

            if (mCurrentProgressBar != null)
            {
                mCurrentProgressBarLogic = null;
                Destroy(mCurrentProgressBar);
            }

            if (mWaitingPlayers.Count > 0)
            {
                Player nextPlayer = mWaitingPlayers.Dequeue();
                if (nextPlayer != null)
                {
                    OnPlayerEntered(nextPlayer);
                }
            }
            
        }

        protected override void OnPlayerEntered(Player player)
        {
            Log(player.Name + "Player " + player.Name + " Entered Treasure " + Treasure != null ? Treasure.Name : " NULL ");
            if (Treasure != null)
            {
                if (currentElevatingPlayer == null)
                {
                    Log(player.Name + "starts to collect" + Treasure.Name);
                    currentElevatingPlayer = player;
                    currentPlayersSpentElevationTime = Time.deltaTime;
                    //Treasure
                    if (GameController.Instance.TreasureMechanics.ProgressBarPrefab != null)
                    {
                        mCurrentProgressBar = Instantiate(GameController.Instance.TreasureMechanics.ProgressBarPrefab);
                        mCurrentProgressBarLogic = mCurrentProgressBar.GetComponent<ProgressBar>();
                    }
                }
                else
                {
                    Log(player.Name + " gets queued in " + Treasure.Name);
                    mWaitingPlayers.Enqueue(player);
                }
            }
            
        }

        private void Log(string message)
        {
            if (GameController.Instance.TreasureMechanics.DebugLog)
            {
                Debug.Log(message);
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

                float progress = currentPlayersSpentElevationTime / (GameController.Instance.TreasureMechanics.secondsToElevateATreasure * (1 / currentElevatingPlayer.BuffMechanics.currentTreasurePickupSpeedModifier));
                if (mCurrentProgressBarLogic != null)
                {
                    Debug.Log("Setting Progress to " + progress.ToString("#.###"));
                    mCurrentProgressBarLogic.SetProgress(progress);
                    mCurrentProgressBar.transform.position = new Vector3(currentElevatingPlayer.transform.position.x - 0.5f, currentElevatingPlayer.transform.position.y, currentElevatingPlayer.transform.position.z);
                }
                if (progress >= 1)
                {
                    Debug.Log(currentElevatingPlayer.Name + " got Treasure " + Treasure.Name);
                    GameController.Instance.TreasureMechanics.PlayerPickedUpTreasure(currentElevatingPlayer, Treasure);
                    currentPlayersSpentElevationTime = 0;
                    currentElevatingPlayer = null;
                    Destroy(mCurrentProgressBar);
                    mCurrentProgressBar = null;
                    mCurrentProgressBarLogic = null;
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
