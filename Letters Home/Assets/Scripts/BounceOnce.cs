using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnce : MonoBehaviour
{
    public float destination = -5f;
    public float time = 0.15f;
    private bool isMoving;
    private float y;
    private float x;

    private void Update()
    {
        if (isMoving)
        {
            LeanTween.moveY(gameObject, y + destination, time).setLoopOnce();
            //LeanTween.moveX(gameObject, x + destination / 2, time).setLoopOnce();
        }
    }

    public void Trigger()
    {
        y = gameObject.transform.position.y;
        //x = gameObject.transform.position.x;
        Invoke("EndTrigger", time);
        isMoving = true;
    }

    void EndTrigger()
    {
        LeanTween.moveY(gameObject, y, time).setLoopOnce();
        //LeanTween.moveY(gameObject, x, time).setLoopOnce();
        isMoving = false;
    }
}
