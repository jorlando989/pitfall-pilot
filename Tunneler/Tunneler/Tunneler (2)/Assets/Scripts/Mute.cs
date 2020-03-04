using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Mute class
 * Mutes and unmutes audio when MuteGame() is called
 */ 
public class Mute : MonoBehaviour {

    private bool muted;
    public AudioSource audio;

	void Start()
    {
        muted = false;
	}
	
    public void MuteGame()
    {
        if (muted)
        {
            muted = false;
            audio.volume = 1f;
        } else
        {
            muted = true;
            audio.volume = 0f;
        }
    }
}
