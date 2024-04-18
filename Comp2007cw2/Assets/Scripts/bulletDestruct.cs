using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDestruct : MonoBehaviour
{

    public GameObject explosionParticles;
    public MeshRenderer bulletMesh;
    public SphereCollider bulletCollider;
    public Rigidbody bulletRb;
    public string ignoreTag;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Ensure that the rigid body is added to the script before the first frame
    void Awake()
    {
        bulletRb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Check if the bullet has collided with anything other than the ignored tag
    private void OnCollisionEnter(Collision collision)
    {
        //Once the bullet has collided, spawn the bullet explosion particles and disable any further movement of the bullet
        if (!collision.transform.tag.Equals(ignoreTag))
        {
            bulletCollider.enabled = false;
            Instantiate(explosionParticles, transform.position, transform.rotation);
            bulletMesh.enabled = false;
            bulletRb.velocity = new Vector3(0, 0, 0);
            Invoke("DestoryObject", 1f);
        }
    }

    //Destroy the bullet object
    public void DestroyObject()
    {
        Destroy(gameObject);
    }

}
