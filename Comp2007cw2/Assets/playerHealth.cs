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

    public Animator characterAnim;

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = health.ToString();
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
            characterAnim.SetTrigger("Shot");
        }

        if (health <= 0)
        {
            playerDead();
            characterAnim.SetTrigger("Dead");
        }

        healthText.text = health.ToString();
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
