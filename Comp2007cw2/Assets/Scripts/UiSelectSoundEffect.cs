using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UiSelectSoundEffect : MonoBehaviour, IPointerEnterHandler
{
    //Button sound
    [Header("Button Hover Sound")]
    [SerializeField] public AudioSource buttonSelectAudio;

    //If the mouse hovers over the buttons, it plays a sound
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonSelectAudio.Play();
    }

}
