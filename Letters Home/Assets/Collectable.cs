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
    public string letterContent;
    public Button exitButton;
    public Button disableButton;
    public GameObject player;
    private AudioSource narration;


    // Start is called before the first frame update
    public void Start()
    {
        if (PlayerPrefs.HasKey("C" + Prefix + Number) && PlayerPrefs.GetInt("C" + Prefix + Number) == 1)
        {
            //Destroy(this.gameObject);
        }
        narration = GetComponent<AudioSource>();
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
                if (LetterCanvas != null)
                    LetterCanvas.gameObject.SetActive(true);
                //if (player != null)
                //    player.gameObject.SetActive(false);
                if (disableButton != null)
                    disableButton.gameObject.SetActive(true);
                if (exitButton != null)
                    exitButton.gameObject.SetActive(false);
                //if (narration != null)
                {
                    narration.gameObject.SetActive(true);
                    narration.Play();
                }
                Destroy(this.gameObject);
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
