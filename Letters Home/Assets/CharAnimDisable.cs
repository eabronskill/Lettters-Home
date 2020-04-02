using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimDisable : MonoBehaviour
{
    public Player play;
    public GameObject shothand;
    public GameObject noShootHand;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
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
    }
}
