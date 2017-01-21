using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	public string NextScene = "test_scene";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown || Input.GetMouseButtonDown(0))
		{
			SceneManager.LoadScene(NextScene);
		}
	}
}
