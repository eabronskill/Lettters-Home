using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTrigger : MonoBehaviour
{
    public DropArm link;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            link.trigger();
            Destroy(this.gameObject);
        }
    }
}
