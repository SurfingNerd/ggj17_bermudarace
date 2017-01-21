using Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Players
{
    public abstract class InputPlayerInteraction : PlayerInteractionBase
    {
        public abstract void ExecuteAction(Player player);

        void Update()
        {
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftControl))
            {
#warning TODO: get from InputService what player pressed the buttong.
                Player triggeringPlayer = mCurrentPlayers.FirstOrDefault();

                if (triggeringPlayer != null)
                {
                    ExecuteAction(triggeringPlayer);
                }
            }
        }           
    }
}
