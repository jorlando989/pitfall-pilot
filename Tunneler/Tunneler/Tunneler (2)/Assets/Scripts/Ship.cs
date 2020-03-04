using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Ship class
 * Handles player input, ship movement, and collision detection
 * UpdatePlayerRotation() updates the player's position around the tunnel based on user input
 * Fire() fires a beam from the player's ship
 * BeamEffect() displays a short beam effect visual
 */ 
public class Ship : MonoBehaviour {

    public PlayerMediator mediator;

    private PipeSystem PipeSystem_;

    public Transform World { get; private set; }
    public Transform Rotater;
    public Transform shipCollider;
    public float Velocity;

    private LineRenderer beamEffect;

    /*
    * Private methods
    */

    private void Start()
    {
        PipeSystem_ = mediator.PipeSystem_;
        World = PipeSystem_.transform.parent;
        Rotater.gameObject.SetActive(false);
        beamEffect = shipCollider.gameObject.GetComponent<LineRenderer>();
        beamEffect.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.transform.parent.parent.CompareTag("HealthPack"))
        {
            mediator.Damage(-1);
        }
        else if (other.gameObject.transform.parent.parent.CompareTag("LineObstacle"))
        {
            mediator.Damage(1);
        }
        else if (other.gameObject.transform.parent.parent.CompareTag("SphereObstacle"))
        {
            mediator.Damage(2);
        }
    }

    public void UpdatePlayerRotation()
    {

        mediator.PlayerRotation += mediator.RotationVelocity * Time.deltaTime * Input.GetAxis("Horizontal");
        if (mediator.PlayerRotation < 0f)
        {
            mediator.PlayerRotation += 360f;
        }
        else if (mediator.PlayerRotation >= 360f)
        {
            mediator.PlayerRotation -= 360f;
        }
        Rotater.localRotation = Quaternion.Euler(mediator.PlayerRotation, 0f, 0f);
    }

    public void Fire()
    {
        if (!beamEffect.enabled)
        {

            RaycastHit hit;

            if (Physics.Raycast(shipCollider.gameObject.transform.position, shipCollider.gameObject.transform.TransformDirection(Vector3.forward), out hit, 500f))
            {
                if (hit.collider.transform.parent.parent.gameObject.tag.Contains("Obstacle")) {
                    Destroy(hit.collider.transform.parent.parent.gameObject);
                    mediator.Score += 1;
                    mediator.UpdateScore(mediator.Score);
                }
            }
            StartCoroutine("BeamEffect");
            //Debug.DrawRay(shipCollider.gameObject.transform.position, shipCollider.gameObject.transform.TransformDirection(Vector3.forward) * 1000, Color.red, 10.0f);
        }
    }

    public void ShowBeam(bool val)
    {
        beamEffect.enabled = val;
    }

    IEnumerator BeamEffect()
    {
        beamEffect.enabled = true;
        yield return new WaitForSeconds(0.05f);
        beamEffect.enabled = false;

    }
}
