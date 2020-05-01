using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerKickBall : MonoBehaviour
{
    public kickball kicker;
    public float delay = 0f;
    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        Invoke("InvokeKick", delay);
    }

   void InvokeKick()
    {
        kicker.Kick(this.gameObject);
    }
}
