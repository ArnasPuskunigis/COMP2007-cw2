using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winnerUi : MonoBehaviour
{

    public Button resumeButton;
    public GameObject winPanel;


    // Start is called before the first frame update
    void Start()
    {

        resumeButton.onClick.AddListener(hidePanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hidePanel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        winPanel.SetActive(false);
    }

    public void showPanel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        winPanel.SetActive(true);
    }

}
