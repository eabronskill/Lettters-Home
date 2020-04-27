using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{
    public Transform ladderTop;
    public float climbSpeed = 3;

    public bool attached = false;
    private bool clb = false;
    void OnTriggerStay(Collider col)
    {

        if(col.gameObject.tag == "Player" && !col.gameObject.GetComponent<Player>().GetDead())
        {

            if (Input.GetButton("Interact") && !clb)
            {
                attached = !attached;
                col.gameObject.GetComponent<PlayerMovement>().attached = attached;
                clb = true;
            }
            else if (clb && !Input.GetButton("Interact"))
            {
                clb = false;
            }

            if (!attached)
            {
                UI_InvFinder.me.nearItem = true;
                UI_InvFinder.me.messageText.text = "Press E to toggle climb";
            }
            else
                UI_InvFinder.me.nearItem = false;

            if (attached)
            {
                col.gameObject.GetComponent<Rigidbody>().useGravity = false;
                col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                col.gameObject.transform.position = new Vector3(ladderTop.position.x, Mathf.Min(col.gameObject.transform.position.y, ladderTop.transform.position.y), ladderTop.position.z);
                if(Mathf.Abs(col.gameObject.transform.position.y - ladderTop.transform.position.y) < 0.1f)
                {
                    col.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    col.gameObject.GetComponent<PlayerMovement>().attached = false;
                    attached = false;
                }
            }
            else
            {
                col.gameObject.GetComponent<Rigidbody>().useGravity = true;
                col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        else if(col.gameObject.tag == "Player" && col.gameObject.GetComponent<Player>().GetDead())
        {
            attached = false;
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !other.gameObject.GetComponent<Player>().GetDead())
        {
            other.gameObject.GetComponent<PlayerMovement>().climbSpeed = climbSpeed;
            UI_InvFinder.me.nearItem = true;
            UI_InvFinder.me.messageText.text = "Press E to toggle climb";
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody>().useGravity = true;
            col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            col.gameObject.GetComponent<PlayerMovement>().attached = false;
            attached = false;
            UI_InvFinder.me.nearItem = false;
        }
    }
}
