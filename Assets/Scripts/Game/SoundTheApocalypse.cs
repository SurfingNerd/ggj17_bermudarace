using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTheApocalypse : MonoBehaviour {

	public AudioClip MusicOfTheApocalypse;

	public void SoundTheApocalypseNow()
	{
		AudioSource source = Camera.main.GetComponent<AudioSource>();
		source.Stop();
		source.clip = MusicOfTheApocalypse;
		source.loop = true;
		source.Play();
	}

}
