using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEditor.SceneManagement;

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
        Cursor.lockState = CursorLockMode.Locked;
        resumeButton.onClick.AddListener(onResumeButtonPressed);
        exitButton.onClick.AddListener(onExitButtonPressed);
    }

    public void onResumeButtonPressed()
    {
        pauseMenu.SetActive(false);
        gamePaused = false;
        cameraRotationComponent.enabled = true;
    }

    public void onExitButtonPressed()
    {
        EditorSceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {

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
