using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObject : MonoBehaviour
{

    public float time;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy the object after a specified time of it spawning
        Destroy(gameObject, time);           
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
