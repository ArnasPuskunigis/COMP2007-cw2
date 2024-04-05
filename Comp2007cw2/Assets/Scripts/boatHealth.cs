using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatHealth : MonoBehaviour
{

    public int health;
    public enemyWaves waveSystem;

    // Start is called before the first frame update
    void Start()
    {
        waveSystem = GameObject.Find("waveManager").GetComponent<enemyWaves>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("bullet"))
        {
            health -= 50;
            if (health == 0)
            {
                destroyBoat();
            }
        }
    }

    public void destroyBoat()
    {
        waveSystem.removeEnemy(gameObject);
    }

}
