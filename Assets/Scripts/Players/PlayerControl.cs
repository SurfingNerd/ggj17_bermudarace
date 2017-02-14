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
        private Rigidbody2D mRigidBody2D;

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
            mRigidBody2D = GetComponent<Rigidbody2D>();

        }


        void Update()
        {
            targetHeading.x = 0;
            targetHeading.y = 0;
            targetHeading.z = 0;

            float ax = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetAxis("Horizontal" + joystickIndex);
            float ay = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetAxis("Vertical" + joystickIndex);

            // dead zone
            if (Mathf.Abs(ax) > 0.1) targetHeading.x = ax;
            if (Mathf.Abs(ay) > 0.1) targetHeading.y = ay;

            throttle = targetHeading.magnitude; //Mathf.Sqrt(ax * ax + ay * ay);
            targetHeading.Normalize();
            throttle = Mathf.Min(1, throttle);
            targetSpeed = throttle * MaxSpeed * (float)velocityModsTotal;

            

            HandleBoosters();
            
            currentSpeed = targetSpeed;
            currentHeading = targetHeading;

            // apply heading
            transform.rotation = Quaternion.FromToRotation(Vector3.up, currentHeading);

            // apply throttle
            currentForce = currentHeading * currentSpeed;
            currentVelocity = Vector3.Lerp(currentVelocity, currentForce, 0.1f); // HACK

            // Now in LateUpdate
            //transform.position = transform.position + currentVelocity * Time.deltaTime;

            //Input.            
        }

        private void HandleBoosters()
        {
            if (Input.GetButtonDown("Jump" + (joystickIndex + 1)))
            {
                Debug.Log("Booster");
            }
        }

        // After everything has updated, apply transforms
        void LateUpdate()
		{
			Vector2 compoundForce = currentVelocity;
			foreach (ForceModification forceMod in transformModifications)
			{
                compoundForce += forceMod.vector * forceMod.factor;
                // HACK this only really works for one mod at a time, otherwise effect depends on order
                //compoundForce = Vector2.Lerp(compoundForce, forceMod.vector, forceMod.factor);
            }
			transformModifications.Clear(); // only apply once

            //transform.position = transform.position + (Vector3)compoundForce * Time.deltaTime;
            mRigidBody2D.AddForce(compoundForce * Time.deltaTime * 100);
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