using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        // Used for sounds to not disapear, basically ensures that it isnt destroyed between scenes
        DontDestroyOnLoad(gameObject);
    }

}
