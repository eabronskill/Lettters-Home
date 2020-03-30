using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    public Image img;
    public Player play;
    public Text Ammo;
    public Text Mag;

    private int prevAmmo;
    private int prevMag;

    // Start is called before the first frame update
    void Start()
    {
        play = GetComponent<DeathDetector>().Trigger;
    }

    // Update is called once per frame
    void Update()
    {
        if(prevAmmo != play.ammo)
        {
            if(prevAmmo < play.ammo)
            {
                Ammo.GetComponent<BounceOnce>().Trigger();
            }
            prevAmmo = play.ammo;
        }

        if (prevMag != play.MagAmmo)
        {
            if (prevMag < play.MagAmmo)
            {
                Mag.GetComponent<BounceOnce>().Trigger();
            }
            prevMag = play.MagAmmo;
        }

        if (play.CanShoot)
        {
            if (!play.Reloading)
                Mag.text = "" + play.MagAmmo;
            else
                Mag.text = "R";

            Mag.enabled = true;
            Ammo.enabled = true;
            img.enabled = true;
            img.gameObject.transform.position = Input.mousePosition;
        }
        else
        {
            Ammo.text = "" + play.ammo;
            Mag.text = "" + play.MagAmmo;
            //Mag.enabled = false;
            //Ammo.enabled = false;
            img.enabled = false;
        }
    }
}
