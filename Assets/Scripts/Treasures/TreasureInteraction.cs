using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using System;
using Game;

namespace Treasures
{
    public class TreasureInteraction : PlayerInputInteraction
    {
        public Treasure Treasure;

        public TreasureInteraction() : base( PlayerControl.ActionType.CRANE_ACTION)
        {
        }

        public override void ExecuteAction(Player player)
        {
            Debug.Log(player.Name + " collects " + Treasure);
			//Treasure

			//GameController.Instance.PlayerPickedUpTreasure(player, Treasure);
        }

        //protected HashSet<Player> mCurrentPlayers = new HashSet<Player>();
    }
}
