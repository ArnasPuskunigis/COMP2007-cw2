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

    public bool isOnWater;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDriving && isOnWater)
        {
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("water"))
        {
            isOnWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("water"))
        {
            isOnWater = false;
        }
    }

}
