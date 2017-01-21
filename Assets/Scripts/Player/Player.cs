using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //HashSet<Treasure>

    [HideInInspector]
    public PlayerControl control;

	// Use this for initialization
	void Start () {
        control = GetComponent<PlayerControl>(); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
