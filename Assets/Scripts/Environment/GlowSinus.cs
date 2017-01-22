using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowSinus : MonoBehaviour {

	private float t;

	public float Speed = 2;
	public float minValue = 0.5f;

	private Renderer rend;
	private Color col;

	// Use this for initialization
	void Start () {
		//t = 0;

		//rend = GetComponent<Renderer>();
		//col = rend.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		//t += Time.deltaTime;
		//col.a = minValue + (1 - minValue * Mathf.Sin(t * Speed));
		//rend.material.color = col;
	}
}
