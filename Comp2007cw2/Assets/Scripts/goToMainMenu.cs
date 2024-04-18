using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class goToMainMenu : MonoBehaviour
{

    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        //If the exit button is pressed launch the loadMainMenu function
        exitButton.onClick.AddListener(loadMainMenu);
    }

    public void loadMainMenu()
    {
        //Load the main menu
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
