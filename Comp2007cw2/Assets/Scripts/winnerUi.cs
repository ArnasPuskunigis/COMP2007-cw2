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
        winPanel.SetActive(false);
    }

    public void showPanel()
    {
        winPanel.SetActive(true);
    }

}
