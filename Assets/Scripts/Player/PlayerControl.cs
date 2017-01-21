using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float MaxSpeed = 1; // units/second  
	public float Acceleration = 1;

	public float TurnSpeed = 1;

	private float throttle = 0; // player input speed command
	private float targetSpeed = 0;
	private float currentSpeed = 0;

	private Vector3 targetHeading; // direction the player wants to turn to
	private Vector3 currentHeading; // direction the ship is pointing

	private Vector3 currentVelocity;

	// Use this for initialization
	void Start () {
		currentVelocity = new Vector3();

		targetHeading = new Vector3(0, 1); // up
		currentHeading = new Vector3(0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space))
		{
			throttle = 1;
		}
		else
		{
			throttle = 0;
		}

		targetSpeed = throttle * MaxSpeed;


		// update heading
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			targetHeading.x = 1;

			if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
			{
				targetHeading.y = 0;
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			targetHeading.x = -1;

			if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
			{
				targetHeading.y = 0;
			}
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			targetHeading.y = 1;

			if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
			{
				targetHeading.x = 0;
			}
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			targetHeading.y = -1;

			if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
			{
				targetHeading.x = 0;
			}
		}

		if ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
		{
			targetHeading.y = 0;
		}
		if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
		{
			targetHeading.x = 0;
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
