using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class DeathDetector : MonoBehaviour
{
    public Player Trigger;
    public GameObject Settee;
    public Text letterTextBox;
    public Button disableButton;
    public Button exitButton;
    [Multiline]
    public string endLetterContent;
    public float delayTime = 2f;
    public UnityEvent e;
    public float textEatingSpeed;
    private bool hasDied = false;
    public AudioSource narration;
    public AudioClip clip;
    public int unlockedLetter;

    // Update is called once per frame
    void Update()
    {
        if (Trigger.GetDead() && !Settee.activeInHierarchy && !hasDied)
        {
            Invoke("delay", delayTime);
            hasDied = true;
        }

            //Settee.SetActive(true);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(0);  
    }

    public void delay()
    {
        exitButton.gameObject.SetActive(true);
        disableButton.gameObject.SetActive(false);
        letterTextBox.text = endLetterContent;
        Settee.GetComponentInChildren<WriteText>().delay = textEatingSpeed;
        Settee.GetComponentInChildren<WriteText>().enabled = true;
        Settee.SetActive(true);
        narration.gameObject.SetActive(true);
        narration.PlayOneShot(clip);
        PlayerPrefs.SetInt("C" + unlockedLetter, 1);
    }
}
