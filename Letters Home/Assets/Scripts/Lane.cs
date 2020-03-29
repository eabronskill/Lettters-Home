using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public int myLane;
    public Collider2D[] myCols;
    public Collider2D[] myChildren;
    // Start is called before the first frame update
    void Start()
    {
        myCols = GetComponents<Collider2D>();
        myChildren = GetComponentsInChildren<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!myCols[0].enabled && PlayerMovement.Lane == myLane)
        {
            foreach(Collider2D c in myCols)
            {
                c.enabled = true;
            }
            foreach (Collider2D c in myChildren)
            {
                c.enabled = true;
            }
        }
        else if (myCols[0].enabled && PlayerMovement.Lane != myLane)
        {
            foreach (Collider2D c in myCols)
            {
                c.enabled = false;
            }
            foreach (Collider2D c in myChildren)
            {
                c.enabled = false;
            }
        }
    }
}
