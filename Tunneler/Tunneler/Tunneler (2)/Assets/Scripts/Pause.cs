using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("Tests")]

/*
 * Pause class
 * pauses and unpauses game when PauseGame() is called
 * Handles key input
 */
public class Pause : MonoBehaviour {

    private bool paused;

    private void Start()
    {
        paused = false;
    }

    public void PauseGame()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1;
        } else
        {
            paused = true;
            Time.timeScale = 0;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

}
