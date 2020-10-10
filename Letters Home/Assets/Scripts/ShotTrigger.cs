using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTrigger : MonoBehaviour
{
    public List<DropArm> link = new List<DropArm>();
    public bool t = false;

    void Update()
    {
        if (t)
        {
            foreach (DropArm arm in link)
            {
                arm.trigger();
            }
            t = !t;
        }

    }
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
