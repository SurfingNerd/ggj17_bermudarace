using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boosters
{
    public abstract class BoosterBase
    {
        protected abstract void Init(Players.Player player);
        protected abstract void Execute(Players.Player player);
    }
}
