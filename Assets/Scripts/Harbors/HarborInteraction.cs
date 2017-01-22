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
            base.OnPlayerEntered(player);

            if (player.boardedTreasures.Count > 0)
            {
                GameController.Instance.TreasureMechanics.PlayerReturnedTreasures(player, player.boardedTreasures);

                foreach (Treasure treasure in player.boardedTreasures.ToArray())
                {
                    player.Input.RemoveVelocityMod(treasure);
                    player.securedTreasures.Add(treasure);
                    player.BuffMechanics.AddSpeedupBuff();
                }

                player.boardedTreasures.Clear();
            }
            
            // PlayerReturnedTreasures


            //TODO: Update UI.
        }


        //public override void ExecuteAction(Player player)
        //{
        //foreach (Treasure item in player.boardedTreasures.ToArray())
        //{ 
        //}
        //}
    }
}
