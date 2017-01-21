using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using System;

namespace Treasures
{
    public class TreasureInteraction : InputPlayerInteraction
    {
        public Treasure Treasure;

        public override void ExecuteAction(Player player)
        {
            Debug.Log(player.Name + " collects " + Treasure);
             //Treasure
        }

        //protected HashSet<Player> mCurrentPlayers = new HashSet<Player>();
    }
}
