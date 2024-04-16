using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManager : MonoBehaviour
{

    public GameObject checkpoint;
    public GameObject checkpoint1;
    public GameObject wasdControls;
    public GameObject mouseControls;
    public GameObject missionText;
    public GameObject boatBlocker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && gameObject.transform.name.Equals("Cylinder"))
        {
            nextCheckpoint();
        }
    }

    public void hideSecondCp()
    {
        checkpoint1.SetActive(false);
    }

    public void nextCheckpoint()
    {
        boatBlocker.SetActive(false);
        missionText.SetActive(false);
        checkpoint.SetActive(false);
        checkpoint1.SetActive(true);
        wasdControls.SetActive(false);
        mouseControls.SetActive(false);
    }

}
