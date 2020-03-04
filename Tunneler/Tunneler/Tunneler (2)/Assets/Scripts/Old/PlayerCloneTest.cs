using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloneTest : MonoBehaviour
{

    public PipeSystem pipeSystem;
    public Transform rotater;

    public float velocity;
    public float rotationVelocity;

    public Collider shipCollider;

    public GameObject mainArea;
    public MainMenu mainMenu;
    public GameObject secondaryCamera;

    private Pipe currentPipe;

    private float distanceTraveled;
    private float timeElapsed;
    private int health;

    private float deltaToRotation;
    private float systemRotation;

    private Transform world;
    private float worldRotation;
    private float playerRotation;

    /*
     * Private methods
     */

    private void Awake()
    {
        world = pipeSystem.transform.parent;
        rotater.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (rotater.gameObject.activeSelf)
        {

            float delta = velocity * Time.deltaTime;
            distanceTraveled += delta;
            timeElapsed += Time.deltaTime;
            systemRotation += delta * deltaToRotation;

            if (systemRotation >= currentPipe.CurveAngle)
            {
                delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
                currentPipe = pipeSystem.SetupNextPipe();
                deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
                SetupCurrentPipe();
                systemRotation = delta * deltaToRotation;
            }

            UpdatePlayerRotation();

            pipeSystem.transform.localRotation = Quaternion.Euler(0f, 0f, systemRotation);

        }
    }

    private void SetupCurrentPipe()
    {

        deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        worldRotation += currentPipe.RelativeRotation;
        if (worldRotation < 0f)
        {
            worldRotation += 360f;
        }
        else if (worldRotation >= 360f)
        {
            worldRotation -= 360f;
        }
        world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);

    }

    private void UpdatePlayerRotation()
    {
        playerRotation += rotationVelocity * Time.deltaTime * Input.GetAxis("Horizontal");
        if (playerRotation < 0f)
        {
            playerRotation += 360f;
        }
        else if (playerRotation >= 360f)
        {
            playerRotation -= 360f;
        }
        rotater.localRotation = Quaternion.Euler(playerRotation, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("PowerUp"))
        {
            // Do powerup things
        }
        else
        {
            Damage(1);
        }
    }

    private void Die()
    {
        mainMenu.EndGame();
        secondaryCamera.SetActive(true);
        rotater.gameObject.SetActive(false);
    }

    /*
     * Public methods
     */

    public void Damage(int damage)
    {
        health -= damage;

        mainMenu.UpdateHealth(health);

        if (health <= 0)
        {
            Die();
        }

    }

    public void StartGame()
    {
        health = 3;
        mainMenu.UpdateHealth(health);
        distanceTraveled = 0f;
        playerRotation = 0f;
        systemRotation = 0f;
        worldRotation = 0f;
        currentPipe = pipeSystem.SetupFirstPipe();
        SetupCurrentPipe();
        secondaryCamera.SetActive(false);
        rotater.gameObject.SetActive(true);
    }

}
