using Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Harbors
{
    public class HarborInteraction : PlayerInputInteraction
    {

        public HarborInteraction()
            : base( PlayerControl.ActionType.CRANE_ACTION)
        {

        }

        [HideInInspector]
        public Harbor harbor;

        public override void ExecuteAction(Player player)
        {
            Debug.Log("HaborInteraction");
        }
    }
}
