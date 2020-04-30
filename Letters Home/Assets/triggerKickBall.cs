using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerKickBall : MonoBehaviour
{
    public kickball kicker;
    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        kicker.Kick(this.gameObject);
    }

}
