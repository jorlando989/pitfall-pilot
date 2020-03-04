using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player class
 * Stores user's data.
 * Die() is called when a player's health drops to 0 or below. Ends the game and shows the menu.
 * Damage(int) damages the player. Implicitly does a health check and calls Die()
 * StartGame() resets all local variables and starts the main game loop
 */
public class Player : MonoBehaviour {

    public PlayerMediator mediator;

    private GameObject SecondaryCamera;
    private Transform Rotater;

    public static float DistanceTraveled { get; set; }
    public static float TimeElapsed { get; set; }
    public static int Health { get; set; }
    public static float PlayerRotation { get; set; }
    public float RotationVelocity;
    public static int Score { get; set; }
    public static int MaxScore { get; set; }

    private void Start()
    {
        SecondaryCamera = mediator.secondaryCamera;
        Rotater = mediator.Rotater;
        MaxScore = 0;
    }

    /*
     * Private methods
     */

    private void Die()
    {
        mediator.EndGame();
        SecondaryCamera.SetActive(true);
        mediator.Rotater.gameObject.SetActive(false);

        if (Score > MaxScore)
        {
            MaxScore = Score;
            mediator.UpdateMaxScore(MaxScore);
        }

    }

    /*
     * Public methods
     */ 

    public void Damage(int damage)
    {
        Health -= damage;

        mediator.UpdateHealth(Health);

        if (Health <=0)
        {
            Die();
        }

    }

    public void StartGame()
    {
        Health = 3;
        Score = 0;
        DistanceTraveled = 0f;
        TimeElapsed = 0f;

        mediator.UpdateHealth(Health);
        mediator.UpdateScore(Score);
        mediator.UpdateDistance(DistanceTraveled);
        mediator.UpdateTime(TimeElapsed);

        PlayerRotation = 0f;

        mediator.SystemRotation = 0f;
        mediator.WorldRotation = 0f;
        mediator.CurrentPipe = mediator.SetupFirstPipe();
        mediator.SetupCurrentPipe();
        mediator.ShowBeam(false);

        SecondaryCamera.SetActive(false);
        Rotater.gameObject.SetActive(true);
    }

}
