using Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Abilities
{
    public class ModifyShipSpeedInteraction : PlayerInteractionBase
    {
        public double modifyFactor = 1;

        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);
            player.Input.AddVelocityMod(this, modifyFactor);
        }

        protected override void OnPlayerLeft(Player player)
        {
            base.OnPlayerLeft(player);
            player.Input.RemoveVelocityMod(this);
        }
    }
}
