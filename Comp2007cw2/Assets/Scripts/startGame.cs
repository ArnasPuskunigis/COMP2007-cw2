using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGame : MonoBehaviour
{
    public GameObject interactText;
    public enemyWaves waveManager;
    public bool playerInRange;

    public AudioSource talking;
    public Animator npcAnim;
    public AudioSource headShot;
    public AudioSource dyingNoise;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            interacted();
        }
    }

    public void interacted()
    {
        npcAnim.SetTrigger("interacted");
        talking.Play();
        Invoke("shootNpc", 1.76f);
    }

    public void shootNpc()
    {
        npcAnim.SetTrigger("shotdead");
        headShot.Play();
        dyingNoise.Play();
        waveManager.startBattle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            interactText.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            interactText.SetActive(false);
            playerInRange = false;
        }
    }
}
