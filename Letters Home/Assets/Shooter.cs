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
    // Start is called before the first frame update
    void Start()
    {
        play = GetComponent<DeathDetector>().Trigger;
    }

    // Update is called once per frame
    void Update()
    {
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
