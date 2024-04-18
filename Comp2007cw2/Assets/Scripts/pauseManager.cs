using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class pauseManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public bool gamePaused;
    public CinemachineFreeLook cameraRotationComponent;

    public Button resumeButton;
    public Button exitButton;


    // Start is called before the first frame update
    void Start()
    {
        //Locks the cursor so the player can see their crosshair
        Cursor.lockState = CursorLockMode.Locked;
        resumeButton.onClick.AddListener(onResumeButtonPressed);
        exitButton.onClick.AddListener(onExitButtonPressed);
    }

    public void onResumeButtonPressed()
    {
        //Resets the pause setting
        pauseMenu.SetActive(false);

        gamePaused = false;
        cameraRotationComponent.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void onExitButtonPressed()
    {
        //Loads the main menu
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Accordingly activates the pause menu and unlocks/locks the mouse cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                pauseMenu.SetActive(false);
                gamePaused = false;
                cameraRotationComponent.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                pauseMenu.SetActive(true);
                gamePaused = true;
                Cursor.visible = true;
                cameraRotationComponent.enabled = false;
            }
        }

    }
}
