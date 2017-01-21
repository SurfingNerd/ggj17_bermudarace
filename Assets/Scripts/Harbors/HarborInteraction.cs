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

            foreach (Treasure treasure in player.boardedTreasures.ToArray())
            {
                player.securedTreasures.Add(treasure);
            }

            player.boardedTreasures.Clear();

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
