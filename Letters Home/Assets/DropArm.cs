using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArm : MonoBehaviour
{

    public Rigidbody hand;

    public GameObject shot;

    public void trigger()
    {
        hand.isKinematic = false;
        hand.useGravity = true;
        shot.GetComponent<AudioSource>().Play();
        shot.GetComponentInChildren<Light>().enabled = true;
        Invoke("Disable", 0.1f);
    }

    private void Disable()
    {
        shot.GetComponentInChildren<Light>().enabled = false;
    }
}
