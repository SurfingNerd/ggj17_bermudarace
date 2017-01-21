using UnityEngine;
using System.Collections;


namespace Players
{
    public class PlayerControl : MonoBehaviour
    {

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
        public Vector3 currentVelocity;

        // Use this for initialization
        void Start()
        {
            currentVelocity = new Vector3();

            targetHeading = new Vector3(0, 1); // up
            currentHeading = new Vector3(0, 1);
        }

        // Update is called once per frame
        void Update()
        {
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
				SendMessage("CraneAction");
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

        // TODO ping treasure, salvage treasure, etc.
    }
}