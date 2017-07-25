using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarm : MonoBehaviour {

	public AudioClip sound;
	AudioSource alarm;
	public float delayTime = 3;

	// Use this for initialization
	void Start () {
		alarm = GetComponent<AudioSource> ();
	}
	public void PlaySound()
	{
		//yield return new WaitForSeconds (delayTime);
		alarm.PlayOneShot (sound);
	}
}
