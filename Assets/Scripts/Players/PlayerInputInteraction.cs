
using UnityEngine;

namespace Players
{
    public abstract class PlayerInputInteraction : PlayerInteractionBase, IPlayerActionObserver
    {
        PlayerControl.ActionType mAction = PlayerControl.ActionType.UNDEFINED_ACTION;

        public PlayerInputInteraction(PlayerControl.ActionType action)
        {
            mAction = action;
        }

        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);

            player.Input.RegisterObserver(this, PlayerControl.ActionType.CRANE_ACTION);
        }

        protected override void OnPlayerLeft(Player player)
        {
            base.OnPlayerLeft(player);
            player.Input.UnRegisterObserver(this, PlayerControl.ActionType.CRANE_ACTION);
        }


        public void ActionWasExecuted(Player player, PlayerControl.ActionType action)
        {
            Debug.Log("Action Got executed");
            ExecuteAction(player);            
        }

        public abstract void ExecuteAction(Player player);

//        void Update()
//        {
//            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftControl))
//            {
//#warning TODO: get from InputService what player pressed the button.
//                Player triggeringPlayer = mCurrentPlayers.FirstOrDefault();

//                if (triggeringPlayer != null)
//                {
//                    ExecuteAction(triggeringPlayer);
//                }
//            }
//        }           
    }
}
