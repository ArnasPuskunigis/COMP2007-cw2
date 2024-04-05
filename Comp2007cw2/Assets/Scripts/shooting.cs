using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bullet;
    public GameObject bulletParent;

    public float shootingIntervalTimer;

    public float timeBetweenShots;

    public bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootingIntervalTimer += Time.deltaTime;

        if(shootingIntervalTimer >= timeBetweenShots)
        {
            canShoot = true;
        }

        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            print("shot");
            Instantiate(bullet, firePoint.position, firePoint.rotation, bulletParent.transform);
            canShoot = false;
            shootingIntervalTimer = 0f;
        }
    }
}
