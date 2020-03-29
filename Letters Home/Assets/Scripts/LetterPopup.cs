using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPopup : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);  
        player = SmoothCam2D.findCam.GetComponent<SmoothCam2D>().Target;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Player>().GetDead() == true) // if player == DEAD
        {
            gameObject.SetActive(true);

        }
    }
}
