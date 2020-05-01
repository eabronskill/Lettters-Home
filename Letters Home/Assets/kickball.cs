using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kickball : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("Interact"))
            {
                Kick(other.gameObject);
            }
        }
    }

    public void Kick(GameObject other)
    {
        if (this.gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        this.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.one * 2 * other.transform.localScale.x, other.transform.position, ForceMode.Impulse);
    }
}


