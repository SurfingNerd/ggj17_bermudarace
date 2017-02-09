using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autoscroll : MonoBehaviour {

	public float Speed = 2;
    public float TotalDuration = 20;
    public AnimationCurve SpeedControl; 
    
	public GameObject AutoscrollKillBar;


    private float timeLapsed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        timeLapsed += Time.deltaTime;
        float currentSpeed = SpeedControl.Evaluate(timeLapsed / TotalDuration) * Speed;
        //Debug.Log("Speed " + currentSpeed.ToString("0.000"));
		transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
	}
}
