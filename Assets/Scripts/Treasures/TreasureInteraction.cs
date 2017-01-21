using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using System;
using Game;

namespace Treasures
{
    public class TreasureInteraction : InputPlayerInteraction
    {
        public Treasure Treasure;

        public override void ExecuteAction(Player player)
        {
            Debug.Log(player.Name + " collects " + Treasure);
			//Treasure

			GameController.Instance.PlayerPickedUpTreasure(player, Treasure);
        }

        //protected HashSet<Player> mCurrentPlayers = new HashSet<Player>();
    }
}
