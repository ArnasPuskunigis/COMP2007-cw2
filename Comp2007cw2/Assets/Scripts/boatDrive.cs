using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatDrive : MonoBehaviour
{

    public Transform boatForcePosition;
    public Rigidbody boatRb;
    public float speed;
    public bool isDriving;
    public float turnSpeed;

    public ParticleSystem waterParticles;

    public bool isOnWater;

    public boatInteract interactScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check that the boat is being driven and if the water is colliding with the water
        if (isDriving && isOnWater)
        {
            // Play the water particles based on if the boat is moving or not
            waterParticles.Play();
            if (boatRb.velocity.x > 0.1f || boatRb.velocity.z > 0.1f)
            {
                var emissionModule = waterParticles.emission;
                emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(50f); // Set to 50 particles per second
            }
            else
            {
                var emissionModule = waterParticles.emission;
                emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(0f); // Set to 0 particles per second
            }

            // Add forces to the boat based on the user input
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                Vector3 oldRot = boatForcePosition.localEulerAngles;
                oldRot.y = 160;
                boatForcePosition.localRotation = Quaternion.Euler(oldRot);
                boatRb.AddForceAtPosition(-boatForcePosition.transform.forward * speed * Time.deltaTime, boatForcePosition.position);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                Vector3 oldRot = boatForcePosition.localEulerAngles;
                oldRot.y = 40;
                boatForcePosition.localRotation = Quaternion.Euler(oldRot);
                boatRb.AddForceAtPosition(-boatForcePosition.transform.forward * speed * Time.deltaTime, boatForcePosition.position);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                Vector3 oldRot = boatForcePosition.localEulerAngles;
                oldRot.y = 90;
                boatForcePosition.localRotation = Quaternion.Euler(oldRot);
                boatRb.AddForceAtPosition(-boatForcePosition.transform.forward * speed * Time.deltaTime, boatForcePosition.position);
            }

            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                Vector3 oldRot = boatForcePosition.localEulerAngles;
                oldRot.y = 160;
                boatForcePosition.localRotation = Quaternion.Euler(oldRot);
                boatRb.AddForceAtPosition(boatForcePosition.transform.forward * speed * Time.deltaTime, boatForcePosition.position);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                Vector3 oldRot = boatForcePosition.localEulerAngles;
                oldRot.y = 40;
                boatForcePosition.localRotation = Quaternion.Euler(oldRot);
                boatRb.AddForceAtPosition(boatForcePosition.transform.forward * speed * Time.deltaTime, boatForcePosition.position);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Vector3 oldRot = boatForcePosition.localEulerAngles;
                oldRot.y = 90;
                boatForcePosition.localRotation = Quaternion.Euler(oldRot);
                boatRb.AddForceAtPosition(boatForcePosition.transform.forward * speed * Time.deltaTime, boatForcePosition.position);
            }
        }
        else
        {
            waterParticles.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if boat has "triggered" the water
        if (other.tag.Equals("water"))
        {
            isOnWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check if boat has "untriggered" the water
        if (other.tag.Equals("water"))
        {
            isOnWater = false;
        }
    }

}
