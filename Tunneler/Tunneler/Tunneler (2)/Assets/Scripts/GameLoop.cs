using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * GameLoop class
 * Handles tunnel generation and item spawning.
 * SetupCurrentPipe() adjusts the current pipe's position to smoothly interpolate through the tunnel
 */ 
public class GameLoop : MonoBehaviour {

    public PlayerMediator mediator;

    private float Velocity;
    private float DistanceTraveled;
    private float TimeElapsed;
    private Transform Rotater;

    public PipeSystem PipeSystem_;
    public float SystemRotation { get; private set; }
    public float DeltaToRotation { get; private set; }
    public static Pipe CurrentPipe;
    private float delta;

    private Transform world;
    public float WorldRotation { get; private set; }

    private void Start()
    {
        world = PipeSystem_.transform.parent;

        DistanceTraveled = mediator.DistanceTraveled;
        TimeElapsed = mediator.TimeElapsed;

        Velocity = mediator.Velocity;
        Rotater = mediator.Rotater;

    }

    private void Update()
    {
        if (Rotater.gameObject.activeSelf)
        {

            delta = Velocity * Time.deltaTime;
            mediator.DistanceTraveled += delta;
            mediator.TimeElapsed += Time.deltaTime;
            SystemRotation += delta * DeltaToRotation;

            mediator.UpdateDistance(mediator.DistanceTraveled);
            mediator.UpdateTime(mediator.TimeElapsed);

            if (SystemRotation >= CurrentPipe.CurveAngle)
            {
                delta = (SystemRotation - CurrentPipe.CurveAngle) / DeltaToRotation;
                CurrentPipe = PipeSystem_.SetupNextPipe();
                mediator.CurrentPipe = CurrentPipe;
                DeltaToRotation = 360f / (2f * Mathf.PI * CurrentPipe.CurveRadius);
                SetupCurrentPipe();
                SystemRotation = delta * DeltaToRotation;
            }

            mediator.UpdatePlayerRotation();
            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                mediator.Fire();
            }

            PipeSystem_.transform.localRotation = Quaternion.Euler(0f, 0f, SystemRotation);

        }
    }

    public void SetupCurrentPipe()
    {

        DeltaToRotation = 360f / (2f * Mathf.PI * CurrentPipe.CurveRadius);
        WorldRotation += CurrentPipe.RelativeRotation;
        if (WorldRotation < 0f)
        {
            WorldRotation += 360f;
        }
        else if (WorldRotation >= 360f)
        {
            WorldRotation -= 360f;
        }
        world.localRotation = Quaternion.Euler(WorldRotation, 0f, 0f);

    }
}
