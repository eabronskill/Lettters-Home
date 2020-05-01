using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerKickBall : MonoBehaviour
{
    public kickball kicker;
    public float delay = 0f;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Invoke("InvokeKick", delay);
            Destroy(this.gameObject, delay + 0.1f);
        }
    }

   void InvokeKick()
    {
        kicker.Kick(this.gameObject);
    }
}
