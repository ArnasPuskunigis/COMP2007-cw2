using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("cannonBall"))
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(2);
    }

    public void destroyPlayer()
    {
        //Player dead ui
    }

}
