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
    public string endLetterContent;
    public float delayTime = 2f;
    public UnityEvent e;

    // Update is called once per frame
    void Update()
    {
        if (Trigger.GetDead() && !Settee.activeInHierarchy)
            Invoke("delay", delayTime);
            //Settee.SetActive(true);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(0);  
    }

    public void delay()
    {
        letterTextBox.text = endLetterContent;
        Settee.SetActive(true);
    }

    private void OnGUI()
    {
        
    }
}
