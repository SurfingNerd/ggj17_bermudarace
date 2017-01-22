using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Players
{
    public class PlayerControl : MonoBehaviour
    {
        public PlayerControl()
        {
            for (int i = 0; i < (int)ActionType.ACTION_TYPE_COUNT; i++)
            {
                mObservers[i] = new System.Collections.Generic.HashSet<IPlayerActionObserver>();
            }
        }

        public enum ActionType
        {
            UNDEFINED_ACTION,
            CRANE_ACTION,
            ACTION_TYPE_COUNT
        }

        public float MaxSpeed = 1; // units/second  
        public float TurnSpeed = 1;

        private double velocityModsTotal = 1;

		public KeyCode KCDown = KeyCode.DownArrow;
		public KeyCode KCUp = KeyCode.UpArrow;
		public KeyCode KCLeft = KeyCode.LeftArrow;
		public KeyCode KCRight = KeyCode.RightArrow;
		public KeyCode KCThrottle = KeyCode.Space;
		public KeyCode KCAction1 = KeyCode.Return;

		public int joystickIndex = 0;

		private float throttle = 0; // player input speed command
        private float targetSpeed = 0;
        private float currentSpeed = 0;

        private Vector3 targetHeading; // direction the player wants to turn to
        private Vector3 currentHeading; // direction the ship is pointing

        private VelocityModCollection velocityMods = new VelocityModCollection();

        [HideInInspector]
        public Player player;

		[HideInInspector]
		public Vector3 currentVelocity;

		private Vector3 currentForce;

		System.Collections.Generic.HashSet<IPlayerActionObserver>[] mObservers = new System.Collections.Generic.HashSet<IPlayerActionObserver>[(int)ActionType.ACTION_TYPE_COUNT];

        BitArray mOngoingActions = new BitArray((int)ActionType.ACTION_TYPE_COUNT);

        List<ForceModification> transformModifications = new List<ForceModification>();

        // Use this for initialization
        void Start()
        {
            currentVelocity = new Vector3();

            targetHeading = new Vector3(0, 1); // up
            currentHeading = new Vector3(0, 1);
            player = GetComponent<Player>();
			transformModifications = new List<ForceModification>();
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

			float throttleAxis = Input.GetAxis("Fire" + (joystickIndex + 1));
			throttle = throttleAxis;

			targetSpeed = throttle * MaxSpeed * (float)velocityModsTotal;


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

			//Debug.Log(Input.GetJoystickNames().Length);
			if (Input.GetJoystickNames().Length >= joystickIndex)
			{
				float ax = Input.GetAxis("Horizontal" + (joystickIndex + 1));
				float ay = Input.GetAxis("Vertical" + (joystickIndex + 1));

				// dead zone
				if (Mathf.Abs(ax) > 0.1) targetHeading.x = ax;
				if (Mathf.Abs(ay) > 0.1) targetHeading.y = ay;
			}

			targetHeading.Normalize();

			if (Input.GetKeyDown(KCAction1))
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
			currentForce = currentHeading * currentSpeed;
			currentVelocity = Vector3.Lerp(currentVelocity, currentForce, 0.1f); // HACK

			// Now in LateUpdate
			//transform.position = transform.position + currentVelocity * Time.deltaTime;
		}

		// After everything has updated, apply transforms
		void LateUpdate()
		{
			Vector2 compoundForce = currentVelocity;
			foreach (ForceModification forceMod in transformModifications)
			{
				// HACK this only really works for one mod at a time, otherwise effect depends on order
				compoundForce = Vector2.Lerp(compoundForce, forceMod.vector, forceMod.factor);
			}
			transformModifications.Clear(); // only apply once

			transform.position = transform.position + (Vector3)compoundForce * Time.deltaTime;
		}

		public void AddForceModification(Vector2 force, float factor)
		{
			transformModifications.Add(new ForceModification(force, factor));
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

        public void AddVelocityMod(object source, double factor)
        {
            velocityMods.Add(new VelocityMod(source, factor));
            ReclalcVelocityMods();
        }

        public void RemoveVelocityMod(object source)
        {
            velocityMods.Remove(source);
            ReclalcVelocityMods();
        }

        private void ReclalcVelocityMods()
        {
            velocityModsTotal = 1;

            foreach (VelocityMod mod in velocityMods)
            {
                velocityModsTotal *= mod.VelocityMultiplier;
            }
        }

    }

    public class VelocityMod
    {
        public VelocityMod(object sourceObject, double velocityMultiplier)
        {
            SourceObject = sourceObject;
            VelocityMultiplier = velocityMultiplier;
        }

        public object SourceObject;
        public double VelocityMultiplier;
    }

    public class VelocityModCollection : System.Collections.ObjectModel.KeyedCollection<object, VelocityMod>
    {
        protected override object GetKeyForItem(VelocityMod item)
        {
            return item.SourceObject;
        }
    }



    public interface IPlayerActionObserver
    {
        void ActionWasExecuted(Player player, PlayerControl.ActionType action);
    }
    
}