using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterPopup : MonoBehaviour
{
    private GameObject player;
    public Button disableButton;
    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);  
        player = SmoothCam2D.findCam.GetComponent<SmoothCam2D>().Target;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if(player.GetComponent<Player>().GetDead() == true) // if player == DEAD
    //    {
    //        gameObject.SetActive(true);

    //    }
    //}

    public void switchButtons()
    {
        disableButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(false);
    }
}
