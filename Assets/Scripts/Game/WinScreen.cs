using Game;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameController.Instance != null)
			{
				GameObject.Destroy(GameController.Instance.gameObject);
			}
			SceneManager.LoadScene("00_titlescreen");
		}
	}
}
