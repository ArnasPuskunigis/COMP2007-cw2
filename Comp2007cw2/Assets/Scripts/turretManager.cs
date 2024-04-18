using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretManager : MonoBehaviour
{

    public Transform playerCamera;
    public Transform turretTrans;
    public boatDrive boatDriveScript;
    public GameObject debugMarker;

    public Transform shootTrans;
    public GameObject boatBullet;
    public float shootingIntervalTimer;
    public float timeBetweenShots;
    public bool canShoot;

    public AudioSource cannonShoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boatDriveScript.isDriving)
        {
            Ray camForwardRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            // IF the raycast hits something rotate the turret to the hit point 
            if (Physics.Raycast(camForwardRay, out hit, 1000f))
            {
                Vector3 relativePosition = hit.point - turretTrans.position;
                relativePosition.y = 0;

                float angleToPlayer = Mathf.Atan2(relativePosition.x, relativePosition.z) * Mathf.Rad2Deg;

                angleToPlayer = (angleToPlayer < 0) ? angleToPlayer + 360 : angleToPlayer;

                Quaternion targetRotation = Quaternion.Euler(0f, angleToPlayer + 90, 0f);
                turretTrans.rotation = targetRotation;

            }


            //If player can shoot and shoots then spawn the bullet and play the cannon shoot sound
            shootingIntervalTimer += Time.deltaTime;

            if (shootingIntervalTimer >= timeBetweenShots)
            {
                canShoot = true;
            }

            if (Input.GetButtonDown("Fire1") && canShoot)
            {
                shootingIntervalTimer = 0f;
                canShoot = false;
                Instantiate(boatBullet, shootTrans.position, shootTrans.rotation);
                cannonShoot.Play();
            }

        }
    }
}
