using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathDetector : MonoBehaviour
{
    public Player Trigger;
    public GameObject Settee;
    public float delayTime = 2f;
    
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
        Settee.SetActive(true);
    }

    private void OnGUI()
    {
        
    }
}
