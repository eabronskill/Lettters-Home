using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTrigger : MonoBehaviour
{
    public List<DropArm> link = new List<DropArm>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (DropArm arm in link)
            {
                arm.trigger();
            }
            Destroy(this.gameObject);
        }
    }
}
