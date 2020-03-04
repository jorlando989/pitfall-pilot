using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * MainMenu class
 * StartGame() starts tunnel generation, movement, and obstacle spawning. Hides main menu.
 * EndGame() shows main menu.
 * UpdateHealth() changes health visual on user interface.
 */
public class MainMenu : MonoBehaviour {

    public Player player;
    public Text healthUI;
    public Text scoreUI;
    public Text timeUI;
    public Text distanceUI;
    public Text maxScoreUI;
    public GameObject mainArea;

    public void StartGame()
    {
        player.StartGame();
        mainArea.SetActive(false);
    }

    public void EndGame()
    {
        mainArea.SetActive(true);
    }

    public void UpdateHealth(int health)
    {
        healthUI.text = health.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreUI.text = ((int)score).ToString();
    }

    public void UpdateTime(float time)
    {
        timeUI.text = ((int)time).ToString();
    }

    public void UpdateDistance(float distance)
    {
        distanceUI.text = ((int)distance).ToString();
    }

    public void UpdateMaxScore(int maxScore)
    {
        maxScoreUI.text = ((int)maxScore).ToString();
    }

}
