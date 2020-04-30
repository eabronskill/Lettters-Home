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
        letterTextBox.text = endLetterContent;
        Settee.GetComponentInChildren<WriteText>().delay = textEatingSpeed;
        Settee.GetComponentInChildren<WriteText>().enabled = true;
        //Settee.GetComponentInChildren<Text>().enabled = true;
        Settee.SetActive(true);
        narration.gameObject.SetActive(true);
        narration.PlayOneShot(clip);
        PlayerPrefs.SetInt("C" + unlockedLetter, 1);
    }
}
