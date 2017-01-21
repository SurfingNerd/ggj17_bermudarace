using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

	public string id;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Player otherPlayer = collider.GetComponent<Player>();
        Debug.Log("Player connected: " + (otherPlayer != null).ToString());
    }
}
