using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatHealth : MonoBehaviour
{

    public int health;
    public enemyWaves waveSystem;

    public GameObject boatExplosion;

    public GameObject explosionParent;

    // Start is called before the first frame update
    void Start()
    {
        waveSystem = GameObject.Find("waveManager").GetComponent<enemyWaves>();
        explosionParent = GameObject.Find("BoatExplosions");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Check if collided with a player bullet
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("bullet"))
        {
            //Take health off
            health -= 50;
            if (health == 0)
            {
                //destroy the boat
                destroyBoat();
            }
        }

        if (collision.gameObject.tag.Equals("playerCannon"))
        {
            destroyBoat();
        }

    }

    public void destroyBoat()
    {
        //Spawn a explosion particle and remove the boat from the wave system list
        Instantiate(boatExplosion, gameObject.transform.position, gameObject.transform.rotation);
        waveSystem.removeEnemy(gameObject);
    }

}
