using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public int Number = 0;
    public string Prefix = "";
    public Canvas LetterCanvas;
    public Canvas DialogueCanvas;
    public Text letterTextBox;
    [Multiline]
    public string letterContent;
    public AudioSource narration;
    public AudioClip clip;
    public float textEatingSpeed;
    private Player player;
    private updateCollectUI collectedUI;
    private bool hasPickedUp = false;


    // Start is called before the first frame update
    public void Start()
    {
        if (PlayerPrefs.HasKey("C" + Prefix + Number) && PlayerPrefs.GetInt("C" + Prefix + Number) == 1)
        {
            //Uncomment before final build!
            SmoothCam2D.findCam.GetComponent<SmoothCam2D>().Target.GetComponent<Player>().collectedLetters++;
            Destroy(this.gameObject, 0.5f);
           
        }
        collectedUI = DialogueCanvas.GetComponentInChildren<updateCollectUI>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.gameObject.GetComponent<Player>();
            UI_InvFinder.me.nearItem = true;
            UI_InvFinder.me.messageText.text = "press 'E' to pickup Letter " + Number;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E) && !hasPickedUp)
            {
                Save();
                hasPickedUp = true;
                UI_InvFinder.me.nearItem = false;
                letterTextBox.text = letterContent;
                letterTextBox.gameObject.GetComponent<WriteText>().enabled = true;
                letterTextBox.gameObject.GetComponent<WriteText>().delay = textEatingSpeed;
                LetterCanvas.gameObject.SetActive(true);
                LetterCanvas.gameObject.GetComponent<LetterPopup>().switchButtons();
                narration.gameObject.SetActive(true);
                narration.PlayOneShot(clip);
                player.collectedLetters += 1;
                Destroy(this.gameObject, 0.5f); //coroutine 
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = null;
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
