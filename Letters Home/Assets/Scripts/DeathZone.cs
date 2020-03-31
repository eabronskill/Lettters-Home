using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("Test");
        if(other.gameObject.tag == "Player") {

            other.gameObject.GetComponent<Player>().SetDead();
            print("Got Domed.");
        }

    }
}
