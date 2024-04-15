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

    void Awake()
    {
        bulletRb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.tag.Equals(ignoreTag))
        {
            bulletCollider.enabled = false;
            Instantiate(explosionParticles, transform.position, transform.rotation);
            bulletMesh.enabled = false;
            bulletRb.velocity = new Vector3(0, 0, 0);
            Invoke("DestoryObject", 1f);
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

}
