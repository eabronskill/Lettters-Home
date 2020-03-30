using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnce : MonoBehaviour
{
    public float destination = -5f;
    public float time = 0.15f;
    private bool isMoving;
    private float y;

    private void Update()
    {
        if (isMoving)
            LeanTween.moveY(gameObject, y + destination, time).setLoopOnce();
    }

    public void Trigger()
    {
        y = gameObject.transform.position.y;
        Invoke("EndTrigger", time);
        isMoving = true;
    }

    void EndTrigger()
    {
        LeanTween.moveY(gameObject, y, time).setLoopOnce();
        isMoving = false;
    }
}
