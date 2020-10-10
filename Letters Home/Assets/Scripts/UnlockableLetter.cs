using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockableLetter : MonoBehaviour
{
    public Text LetterText;
    [Multiline]
    public string LetterString;
    public float delay = 20;
    public AudioClip Vocals;
    public AudioSource VocalEmitter;
    public string UnlockKey;
    public Button me;
    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Button>();
        if(PlayerPrefs.HasKey("C" + UnlockKey) && PlayerPrefs.GetInt("C" + UnlockKey) == 1)
        {
            me.interactable = true;
        }
        else
        {
            me.interactable = false;
        }
    }

    // Update is called once per frame
    public void SendDescriptionAndSound()
    {
        LetterText.text = LetterString;
        VocalEmitter.clip = Vocals;
        LetterText.GetComponent<WriteText>().delay = delay;
    }
}
