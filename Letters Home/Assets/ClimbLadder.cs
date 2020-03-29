using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{
    public Transform ladderTop;
    public float climbSpeed = 3;

    public bool attached = false;
    void OnTriggerStay(Collider col)
    {

        if(col.gameObject.tag == "Player" && !col.gameObject.GetComponent<Player>().GetDead())
        {
            col.gameObject.GetComponent<PlayerMovement>().attached = attached;

            attached = Input.GetButton("Interact");


            if (attached)
            {
                col.gameObject.GetComponent<Rigidbody>().useGravity = false;
                col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                col.gameObject.transform.position = new Vector3(ladderTop.position.x, Mathf.Min(col.gameObject.transform.position.y, ladderTop.transform.position.y), ladderTop.position.z);
            }
            else
            {
                col.gameObject.GetComponent<Rigidbody>().useGravity = true;
                col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }

        }else if(col.gameObject.tag == "Player" && col.gameObject.GetComponent<Player>().GetDead())
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
            UI_InvFinder.me.messageText.text = "Hold E to climb";
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody>().useGravity = true;
            col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            col.gameObject.GetComponent<PlayerMovement>().attached = false;
            UI_InvFinder.me.nearItem = false;
        }
    }
}
