using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goForward : MonoBehaviour
{

    public float bulletSpeed;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //Add force to push the bullet forward as soon as it is spawned and add speed based on inspector input
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
