using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoGiver : MonoBehaviour
{
    GameObject player;
    public int amount;

    // Start is called before the first frame update
    void Start()
    {
        player = SmoothCam2D.findCam.GetComponent<SmoothCam2D>().Target;
        
    }

    public void giveAmmo()
    {
        player.GetComponent<Player>().ammo += amount;
        player.GetComponent<Player>().myItem = null;
        print("Picked up");
        Destroy(this.gameObject);
    }


}
