using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UI_InvFinder.me.messageText.text = "PRESS SPACE TO STEP UP";
            UI_InvFinder.me.nearItem = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UI_InvFinder.me.messageText.text = "";
            UI_InvFinder.me.nearItem = false;

        }
    }
}
