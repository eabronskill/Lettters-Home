using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimDisable : MonoBehaviour
{
    public bool ShootDisable = true;
    public bool LanternDisable = false;
    public bool GasMaskDisable = false;
    public Player play;
    public GameObject shothand;
    public GameObject noShootHand;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(ShootDisable)
            if(play.CanShoot && noShootHand.activeInHierarchy)
            {
                noShootHand.SetActive(false);
                shothand.SetActive(true);
            }
            else if(!play.CanShoot && shothand.activeInHierarchy)
            {
                noShootHand.SetActive(true);
                shothand.SetActive(false);
            }
        if (LanternDisable)
            if (play.Lantern && noShootHand.activeInHierarchy)
            {
                noShootHand.SetActive(false);
                shothand.SetActive(true);
            }
            else if (!play.Lantern && shothand.activeInHierarchy)
            {
                noShootHand.SetActive(true);
                shothand.SetActive(false);
            }
    }
}
