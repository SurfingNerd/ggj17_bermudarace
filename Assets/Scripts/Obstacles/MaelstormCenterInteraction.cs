using Players;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Obstacles
{
    public class MaelstormCenterInteraction : PlayerInteractionBase
    {
        public double killPlayersAfterSeconds = 0.5;

        Dictionary<Player, double> mPlayerInMaelstormTime = new Dictionary<Player, double>();

        protected override void OnPlayerEntered(Player player)
        {
            mPlayerInMaelstormTime.Add(player, Time.deltaTime);
        }


        protected override void OnPlayerLeft(Player player)
        {
            if (mPlayerInMaelstormTime.ContainsKey(player))
            {
                mPlayerInMaelstormTime.Remove(player);
            }
        }

        void Update()
        {
            foreach (Player player in mPlayerInMaelstormTime.Keys.ToArray())
            {
                double finalTime = mPlayerInMaelstormTime[player] + Time.deltaTime;
                if ( finalTime >= killPlayersAfterSeconds)
                {
                    Debug.Log("Player got sucked in into maelstorm.");
                    player.Kill();
                }
                else
                {
                    mPlayerInMaelstormTime[player] = finalTime;
                }
            }
        }

    }
}
