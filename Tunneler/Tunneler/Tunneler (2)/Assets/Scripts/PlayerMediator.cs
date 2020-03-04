using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * PlayerMediator class
 * Handles communication dependencies between multiple classes, including Player, Ship, MainMenu, and GameLoop.
 */ 
public class PlayerMediator : MonoBehaviour
{

    public Player player;
    public GameLoop gameLoop;
    public Ship ship;

    public GameObject mainArea;
    public MainMenu mainMenu;
    public GameObject secondaryCamera;

    /*
     * Intercommunication between modules
     */

    public void UpdateHealth(int health)
    {
        mainMenu.UpdateHealth(health);
    }

    public void UpdateScore(int score)
    {
        mainMenu.UpdateScore(score);
    }

    public void UpdateDistance(float distance)
    {
        mainMenu.UpdateDistance(distance);
    }

    public void UpdateTime(float time)
    {
        mainMenu.UpdateTime(time);
    }

    public void UpdateMaxScore(int maxScore)
    {
        mainMenu.UpdateMaxScore(maxScore);
    }

    public void EndGame()
    {
        mainMenu.EndGame();
    }

    public float DistanceTraveled
    {
        get
        {
            return Player.DistanceTraveled;
        }

        set { Player.DistanceTraveled = value; }
    }

    public float TimeElapsed
    {
        get
        {
            return Player.TimeElapsed;
        }

        set { Player.TimeElapsed = value; }
    }

    public float PlayerRotation
    {
        get
        {
            return Player.PlayerRotation;
        }

        set { Player.PlayerRotation = value; }
    }

    public int Health
    {
        get
        {
            return Player.Health;
        }

        set { Player.Health = value; }
    }

    public int Score
    {
        get
        {
            return Player.Score;
        }

        set { Player.Score = value; }
    }

    public float RotationVelocity
    {
        get
        {
            return player.RotationVelocity;
        }

        set { }
    }

    public void Damage(int damage)
    {
        player.Damage(damage);
    }

    public float SystemRotation
    {
        get
        {
            return gameLoop.SystemRotation;
        }

        set { }
    }

    public float DeltaToRotation
    {
        get
        {
            return gameLoop.DeltaToRotation;
        }
    }

    public Pipe CurrentPipe
    {
        get
        {
            return GameLoop.CurrentPipe;
        }

        set { GameLoop.CurrentPipe = value; }
    }

    public PipeSystem PipeSystem_
    {
        get
        {
            return gameLoop.PipeSystem_;
        }
    }

    public float WorldRotation
    {
        get
        {
            return gameLoop.WorldRotation;
        }

        set { }
    }

    public void SetupCurrentPipe()
    {
        gameLoop.SetupCurrentPipe();
    }

    public Pipe SetupFirstPipe()
    { 
        return gameLoop.PipeSystem_.SetupFirstPipe();
    }

    public float Velocity
    {
        get
        {
            return ship.Velocity;
        }
    }

    public Transform Rotater
    {
        get
        {
            return ship.Rotater;
        }
    }

    public void UpdatePlayerRotation()
    {
        ship.UpdatePlayerRotation();
    }

    public void Fire()
    {
        ship.Fire();
    }

    public void ShowBeam(bool val)
    {
        ship.ShowBeam(val);
    }
}
