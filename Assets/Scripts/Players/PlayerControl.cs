using UnityEngine;
using System.Collections;
using System;

namespace Players
{
    public class PlayerControl : MonoBehaviour
    {
        public enum ActionType
        {
            UNDEFINED_ACTION,
            CRANE_ACTION,
            ACTION_TYPE_COUNT
        }

        public float MaxSpeed = 1; // units/second  
        public float Acceleration = 1;

        public float TurnSpeed = 1;

		public KeyCode KCDown = KeyCode.DownArrow;
		public KeyCode KCUp = KeyCode.UpArrow;
		public KeyCode KCLeft = KeyCode.LeftArrow;
		public KeyCode KCRight = KeyCode.RightArrow;
		public KeyCode KCThrottle = KeyCode.Space;
		public KeyCode KCAction1 = KeyCode.Return;

		private float throttle = 0; // player input speed command
        private float targetSpeed = 0;
        private float currentSpeed = 0;

        private Vector3 targetHeading; // direction the player wants to turn to
        private Vector3 currentHeading; // direction the ship is pointing

        [HideInInspector]
        public Player player;

		[HideInInspector]
        public Vector3 currentVelocity;

        System.Collections.Generic.HashSet<IPlayerActionObserver>[] mObservers = new System.Collections.Generic.HashSet<IPlayerActionObserver>[(int)ActionType.ACTION_TYPE_COUNT];

        BitArray mOngoingActions = new BitArray((int)ActionType.ACTION_TYPE_COUNT);
       
        // Use this for initialization
        void Start()
        {
            currentVelocity = new Vector3();

            targetHeading = new Vector3(0, 1); // up
            currentHeading = new Vector3(0, 1);
            player = GetComponent<Player>();
            for (int i = 0; i < (int)ActionType.ACTION_TYPE_COUNT; i++)
            {
                mObservers[i] = new System.Collections.Generic.HashSet<IPlayerActionObserver>();
            }
        }

        

        // Update is called once per frame
        void Update()
        {
            mOngoingActions.SetAll(false);

            //m_ongoingActions

            if (Input.GetKey(KCThrottle))
            {
                throttle = 1;
            }
            else
            {
                throttle = 0;
            }

            targetSpeed = throttle * MaxSpeed;


            // update heading
            if (Input.GetKeyDown(KCRight))
            {
                targetHeading.x = 1;

                if (!Input.GetKey(KCUp) && !Input.GetKey(KCDown))
                {
                    targetHeading.y = 0;
                }
            }
            if (Input.GetKeyDown(KCLeft))
            {
                targetHeading.x = -1;

                if (!Input.GetKey(KCUp) && !Input.GetKey(KCDown))
                {
                    targetHeading.y = 0;
                }
            }
            if (Input.GetKey(KCUp))
            {
                targetHeading.y = 1;

                if (!Input.GetKey(KCLeft) && !Input.GetKey(KCRight))
                {
                    targetHeading.x = 0;
                }
            }
            if (Input.GetKey(KCDown))
            {
                targetHeading.y = -1;

                if (!Input.GetKey(KCLeft) && !Input.GetKey(KCRight))
                {
                    targetHeading.x = 0;
                }
            }

            if ((Input.GetKeyUp(KCRight) || Input.GetKeyUp(KCLeft)) && !Input.GetKey(KCUp) && !Input.GetKey(KCDown))
            {
                targetHeading.y = 0;
            }
            if ((Input.GetKeyUp(KCUp) || Input.GetKeyUp(KCDown)) && !Input.GetKey(KCLeft) && !Input.GetKey(KCRight))
            {
                targetHeading.x = 0;
            }

			if(Input.GetKeyDown(KCAction1))
			{
                NotifyAction(ActionType.CRANE_ACTION);
                
                //SendMessage("CraneAction");
            }
			
            // TODO Gamepad

            // TODO interpolate
            currentSpeed = targetSpeed;
            currentHeading = targetHeading;

            // apply heading
            transform.rotation = Quaternion.FromToRotation(Vector3.up, currentHeading);

            // apply throttle
            Vector3 currentForce = currentHeading * currentSpeed;

            currentVelocity = Vector3.Lerp(currentVelocity, currentForce, 0.1f); // HACK
            transform.position = transform.position + currentVelocity * Time.deltaTime;
        }

        private void NotifyAction(ActionType action)
        {
            System.Collections.Generic.HashSet<IPlayerActionObserver> observers = mObservers[(int)action];

            foreach (var observer in observers)
            {
                observer.ActionWasExecuted(player, action);
            }
        }

        // TODO ping treasure, salvage treasure, etc.




        public void RegisterObserver(IPlayerActionObserver observer, ActionType actionType)
        {
            mObservers[(int)actionType].Add(observer);
        }


        public void UnRegisterObserver(IPlayerActionObserver observer, ActionType actionType)
        {
            mObservers[(int)actionType].Remove(observer);
        }
    }


    public interface IPlayerActionObserver
    {
        void ActionWasExecuted(Player player, PlayerControl.ActionType action);
    }
    
}