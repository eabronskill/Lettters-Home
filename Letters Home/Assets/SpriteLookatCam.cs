using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLookatCam : MonoBehaviour
{
    public bool LockRotUD = true;
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.LookAt(SmoothCam2D.findCam);
        if (LockRotUD)
        {
            this.gameObject.transform.rotation.eulerAngles.Set(0, this.gameObject.transform.rotation.eulerAngles.y, 0);
        }
    }
}
