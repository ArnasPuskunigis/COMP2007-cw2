using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGame : MonoBehaviour
{
    public GameObject interactText;
    public enemyWaves waveManager;
    public tutorialManager tutorialScript;

    public bool playerInRange;
    
    public Animator npcAnim;
    public AudioSource talking;
    public AudioSource headShot;
    public AudioSource dyingNoise;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the player is in range and if they pressed F to interact
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            interacted();
            tutorialScript.hideSecondCp();
        }
    }

    public void interacted()
    {
        //Sets the animation trigger and lauches the next animation but with a delay
        npcAnim.SetTrigger("interacted");
        //Plays the talking sound effect
        talking.Play();
        Invoke("shootNpc", 3.24f);
    }

    public void shootNpc()
    {
        //Sets the shot trigger in the animation controller and plays the respected sounds, also starts the wave system
        npcAnim.SetTrigger("shotdead");
        headShot.Play();
        dyingNoise.Play();
        waveManager.startBattle();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks if the player interacted with the collider to see if the player is in range
        if (other.transform.tag.Equals("Player"))
        {
            interactText.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Checks if the player left the collider to get rid of the message
        if (other.transform.tag.Equals("Player"))
        {
            interactText.SetActive(false);
            playerInRange = false;
        }
    }
}
