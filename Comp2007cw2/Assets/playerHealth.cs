using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public bool isAlive;
    public GameObject gameOverPanel;

    public TextMeshProUGUI healthText;


    // Start is called before the first frame update
    void Start()
    {
        healthText.text = health + "/" + maxHealth;
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

        healthText.text = health + "/" + maxHealth;
    }

    public void playerDead()
    {
        print("you died");
        gameOverPanel.SetActive(true);
        Invoke("destroyPlayer()", 1);
    }

    public void destroyPlayer()
    {
        //Player dead ui
    }

}
