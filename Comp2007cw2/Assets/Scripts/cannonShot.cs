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
        //Set cannon ball speed to randomly be between half and double of the inspector value
        cannonBallSpeed = Random.Range(cannonBallSpeed / 2, cannonBallSpeed * 2);
        //Get the rigid body component
        rb = GetComponent<Rigidbody>();
        //Add the initial force of the cannon ball
        rb.AddForce(transform.forward * cannonBallSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
