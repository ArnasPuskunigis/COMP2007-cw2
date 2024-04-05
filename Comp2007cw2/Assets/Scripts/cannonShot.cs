using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonShot : MonoBehaviour
{
    public float cannonBallSpeed;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * cannonBallSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
