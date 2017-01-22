using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Players;

namespace Assets.Buffs
{
    public class SpeedUpBuff : BuffBase
    {
        public double SpeedUpFactor = 1.3;

        public override void InitBuff(Player player)
        {
            base.InitBuff(player);

            player.Input.AddVelocityMod(this, SpeedUpFactor);
        }

        public override void BuffEnded(Player player)
        {
            player.Input.RemoveVelocityMod(this);
            base.BuffEnded(player);
        }
    }
}
