using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("MV"))
        {
            PlayerPrefs.SetFloat("MV", 5);
            PlayerPrefs.SetFloat("SFXV", 5);
            PlayerPrefs.SetFloat("NarV", 5);
        }
    }
    
}
