using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class boatInteract : MonoBehaviour
{

    public GameObject player;

    public CharacterController charController;
    public CapsuleCollider playerCollider;


    public Transform playerSitPos;
    public boatDrive boatDriveScript;
    public bool playerInRange = false;

    public TextMeshProUGUI getInText;
    public TextMeshProUGUI getOutText;

    public AudioSource genInSound;
    public AudioSource engineSound;

    public bool gotInBoatBool;
    public bool engineOn;

    void Update()
    {
        //if the player is in range and press F and the boat is not being driven, then put the player in the boat
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !boatDriveScript.isDriving)
        {
            boatDriveScript.isDriving = true;
            playerCollider.isTrigger = true;
            if (!gotInBoatBool)
            {
                gotInBoat();
            }
        }
        //Otherwise take the player out of the boat
        else if (playerInRange && Input.GetKeyDown(KeyCode.F) && boatDriveScript.isDriving)
        {
            boatDriveScript.isDriving = false;
            playerCollider.isTrigger = true;
            pauseEngineNoise();
        }

        //Teleport the player onto the boat every frame
        if (boatDriveScript.isDriving)
        {
            charController.enabled = false;
            player.transform.position = playerSitPos.position;
        }
        else
        {
            charController.enabled = true;
        }
    }

    //Functions for starting and pausing the boat sounds 
    public void gotInBoat()
    {
        gotInBoatBool = true;
        genInSound.Play();
        Invoke("playEngineNoise", 2f);
    }

    public void playEngineNoise()
    {
        engineOn = true;
        engineSound.Play();
    }

    public void pauseEngineNoise()
    {
        gotInBoatBool = false;
        engineSound.Pause();
    }

    // Check if the player is in range by entering the trigger around the boat
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && boatDriveScript.isDriving)
        {
            getInText.gameObject.SetActive(false);
            getOutText.gameObject.SetActive(true);
            playerInRange = true;
        }
        if (other.tag.Equals("Player") && !boatDriveScript.isDriving)
        {
            getOutText.gameObject.SetActive(false);
            getInText.gameObject.SetActive(true);
            playerInRange = true;
        }
    }

    // Check if the player has gone out range by exitting the trigger around the boat
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            getOutText.gameObject.SetActive(false);
            getInText.gameObject.SetActive(false);
            print("collision exit: " + other.tag);
            playerInRange = false;
        }
    }

}


