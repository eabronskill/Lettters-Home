using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunActivate : MonoBehaviour
{
    // Start is called before the first frame update
    public int MagAmount;
    public int AmmoOnPickup;
    public int MagCap;

    public void Activate()
    {
        Player current = SmoothCam2D.findCam.GetComponent<SmoothCam2D>().Target.GetComponent<Player>();
        current.ammo += AmmoOnPickup;
        current.MagAmmo = MagAmount;
        current.MagSize = MagCap;
    }
    // Update is called once per frame
    public void Trigger()
    {
        Player current = SmoothCam2D.findCam.GetComponent<SmoothCam2D>().Target.GetComponent<Player>();
        current.CanShoot = !current.CanShoot;
    }

    public void Deactivate()
    {
        Player current = SmoothCam2D.findCam.GetComponent<SmoothCam2D>().Target.GetComponent<Player>();
        current.CanShoot = false;
    }
}