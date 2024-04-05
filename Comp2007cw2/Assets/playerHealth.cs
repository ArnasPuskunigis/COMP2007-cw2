using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int health;
    public bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("cannonBall"))
        {
            health -= 10;
        }

        if (health <= 0)
        {
            playerDead();
        }


    }

    public void playerDead()
    {
        Invoke("destroyPlayer()", 1);
    }

    public void destroyPlayer()
    {
        //Player dead ui
    }

}
