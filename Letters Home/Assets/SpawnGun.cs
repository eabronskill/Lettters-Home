using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGun : MonoBehaviour
{
    public GameObject gun;
    private Dialog_Basic dialog;
    private bool spawned;
    private void Start()
    {
        dialog = GetComponent<Dialog_Basic>();
    }

    private void FixedUpdate()
    {
        print("69");
        if (!dialog.social && !spawned) 
        { 
            gun.SetActive(true);
            spawned = true;
            Destroy(this);
        }
    }
}
