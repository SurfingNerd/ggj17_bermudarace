using Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buffs
{
    public abstract class BuffBase
    {
        public double maxDuration = 45;
        public double durationLeft = 30;

        public virtual void InitBuff(Player player)
        {

        }

        public virtual void BuffEnded(Player player)
        {

        }

        public virtual void CalcTick(Player player)
        {

        }
    }
}

