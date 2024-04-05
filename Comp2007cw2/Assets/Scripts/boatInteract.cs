using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class boatInteract : MonoBehaviour
{

    public GameObject player;
    public Transform playerSitPos;
    public boatDrive boatDriveScript;
    public bool playerInRange = false;

    public TextMeshProUGUI getInText;
    public TextMeshProUGUI getOutText;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !boatDriveScript.isDriving)
        {
            boatDriveScript.isDriving = true;
        }
        else if (playerInRange && Input.GetKeyDown(KeyCode.F) && boatDriveScript.isDriving)
        {
            boatDriveScript.isDriving = false;
        }

        if (boatDriveScript.isDriving)
        {
            player.transform.position = playerSitPos.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && boatDriveScript.isDriving == true)
        {
            getInText.gameObject.SetActive(false);
            getOutText.gameObject.SetActive(true);
            playerInRange = true;
        }
        if (other.tag.Equals("Player") && boatDriveScript.isDriving == false)
        {
            getOutText.gameObject.SetActive(false);
            getInText.gameObject.SetActive(true);
            playerInRange = true;
        }
    }

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


