using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * PipeSystem class
 * Spawns Pipe objects and connects them by repositioning about the origin
 * Uses world-space and local-space tricks to achieve seamless tunnels
 * On launch, entire PipeSystem is instantiated with variable number of pipes
 * During gameplay, Pipes are reused (repositioned), rather than destroyed and instantiated. 
 * ShiftPipes() shifts all pipes along the list, moving the pipe that was most recently seen to the back of the PipeSystem
 * AlignNextPipeWithOrigin() repositions the PipeSystem after a shift happens, aligning the new current pipe seamlessly with the origin.
 * SetupFirstPipe() uses the Pipe objects that were created at runtime, repositioning and aligning them seamlessly about the origin. Returns the second Pipe object in the system.
 * SetupNextPipe() shifts PipeSystem. This is called after SetupFirstPipe so we have a few assumptions that we can make to streamline the function. Positions, aligns, and spawns items in the next Pipe object.
 */ 
public class PipeSystem : MonoBehaviour
{

    public Pipe pipePrefab;

    public int pipeCount;

    public int emptyPipeCount;

    private Pipe[] pipes;

    private void Awake()
    {
        pipes = new Pipe[pipeCount];
        for (int i = 0; i < pipes.Length; i++)
        {
            Pipe pipe = pipes[i] = Instantiate<Pipe>(pipePrefab);
            pipe.transform.SetParent(transform, false);
            
        }
    }

    private void ShiftPipes()
    {
        Pipe temp = pipes[0];
        for (int i = 1; i < pipes.Length; i++)
        {
            pipes[i - 1] = pipes[i];
        }
        pipes[pipes.Length - 1] = temp;
    }

    private void AlignNextPipeWithOrigin()
    {
        Transform transformToAlign = pipes[1].transform;
        for (int i = 0; i < pipes.Length; i++)
        {
            if (i != 1)
            {
                pipes[i].transform.SetParent(transformToAlign);
            }
        }

        transformToAlign.localPosition = Vector3.zero;
        transformToAlign.localRotation = Quaternion.identity;

        for (int i = 0; i < pipes.Length; i++)
        {
            if (i != 1)
            {
                pipes[i].transform.SetParent(transform);
            }
        }
    }

    /*
     * Public methods
     */

    public Pipe SetupFirstPipe()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            Pipe pipe = pipes[i];
            pipe.Generate(i > emptyPipeCount);
            if (i > 0)
            {
                pipe.AlignWith(pipes[i - 1]);
            }
        }
        AlignNextPipeWithOrigin();
        transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);

        return pipes[1];
    }

    public Pipe SetupNextPipe()
    {
        ShiftPipes();
        AlignNextPipeWithOrigin();
        pipes[pipes.Length - 1].Generate();
        pipes[pipes.Length - 1].AlignWith(pipes[pipes.Length - 2]);
        transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
        return pipes[1];
    }

}