using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public int Number = 0;
    public string Prefix = "";
    public Canvas LetterCanvas;
    public Text letterTextBox;
    [Multiline]
    public string letterContent;
    public AudioSource narration;
    public AudioClip clip;
    public float textEatingSpeed;


    // Start is called before the first frame update
    public void Start()
    {
        if (PlayerPrefs.HasKey("C" + Prefix + Number) && PlayerPrefs.GetInt("C" + Prefix + Number) == 1)
        {
            //Uncomment before final build!
            //Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            UI_InvFinder.me.nearItem = true;
            UI_InvFinder.me.messageText.text = "press 'E' to pickup Letter " + Number;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Save();
                UI_InvFinder.me.nearItem = false;
                letterTextBox.text = letterContent;
                letterTextBox.gameObject.GetComponent<WriteText>().enabled = true;
                letterTextBox.gameObject.GetComponent<WriteText>().delay = textEatingSpeed;
                LetterCanvas.gameObject.SetActive(true);
                LetterCanvas.gameObject.GetComponent<LetterPopup>().switchButtons();
                narration.gameObject.SetActive(true);
                narration.PlayOneShot(clip);
                //Destroy(this.gameObject); //coroutine 
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            UI_InvFinder.me.nearItem = false;
        }
    }
    // Update is called once per frame
    void Save()
    {
        PlayerPrefs.SetInt("C" + Prefix + Number, 1);
    }

    void UnSave()
    {
        PlayerPrefs.SetInt("C" + Prefix + Number, 0);
    }
}
