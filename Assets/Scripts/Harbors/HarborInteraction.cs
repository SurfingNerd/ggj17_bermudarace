using Game;
using Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Treasures;
using UnityEngine;

namespace Harbors
{
    public class HarborInteraction : PlayerInteractionBase
    {

        //[HideInInspector]
        //public Harbor harbor;

        public HarborInteraction()
        {

        }

        protected override void OnPlayerEntered(Player player)
        {
            if (player.boardedTreasures.Count > 0)
            {
                GameController.Instance.TreasureMechanics.PlayerReturnedTreasures(player);
            }
            
            // PlayerReturnedTreasures


            //TODO: Update UI.
        }

        protected override void OnPlayerLeft(Player player)
        {
            
        }


        //public override void ExecuteAction(Player player)
        //{
        //foreach (Treasure item in player.boardedTreasures.ToArray())
        //{ 
        //}
        //}
    }
}
