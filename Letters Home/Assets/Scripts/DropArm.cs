using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArm : MonoBehaviour
{

    public Rigidbody hand;
    public bool addForce = false;
    public Vector3 force;
    public GameObject shot;

    public void trigger()
    {
        hand.isKinematic = false;
        hand.useGravity = true;
        if(shot != null)
        {
            shot.GetComponent<AudioSource>().Play();
            shot.GetComponentInChildren<Light>().enabled = true;
        }
        if (addForce)
        {
            hand.AddForce(force, ForceMode.Impulse);
        }
        Invoke("Disable", 0.1f);
    }

    private void Disable()
    {
        shot.GetComponentInChildren<Light>().enabled = false;
    }
}
