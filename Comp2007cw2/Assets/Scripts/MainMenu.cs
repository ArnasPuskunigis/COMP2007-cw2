using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    // ===== SOUND EFFECTS ===== 
    // UI
    // https://pixabay.com/sound-effects/button-124476/
    // SEA
    // https://pixabay.com/sound-effects/sandy-beach-calm-waves-water-nature-sounds-8052/
    // ===== MUSIC =====
    // https://pixabay.com/users/kamhunt-27612606/

    [Header("Buttons")]
    [SerializeField] public Button PlayButton;
    [SerializeField] public Button ExitButton;

    [Header("Audio Source")]
    [SerializeField] public AudioSource buttonPressAudio;

    // Start is called before the first frame update
    void Start()
    {
        //Add listeners to the buttons when theyre pressed, this is like an event and it will run the functions
        PlayButton.onClick.AddListener(PlayPressed);
        ExitButton.onClick.AddListener(ExitPressed);
    }

    public void PlayPressed()
    {
        // Play sound
        buttonPressAudio.Play();
        // Load the play scene
        SceneManager.LoadScene(1);
    }

    public void ExitPressed()
    {
        // Play sound
        buttonPressAudio.Play();
        // Close the application
        Application.Quit();
    }


}

