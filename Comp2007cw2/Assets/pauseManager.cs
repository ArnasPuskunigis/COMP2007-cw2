using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class pauseManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public bool gamePaused;
    public CinemachineFreeLook cameraRotationComponent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                pauseMenu.SetActive(false);
                gamePaused = false;
                cameraRotationComponent.enabled = true;
            }
            else
            {
                pauseMenu.SetActive(true);
                gamePaused = true;
                cameraRotationComponent.enabled = false;
            }
        }

    }
}
