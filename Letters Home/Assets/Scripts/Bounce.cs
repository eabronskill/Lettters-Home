using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float destination = -2.75f;
    public float time = 0.5f;
    private void OnEnable()
    {
        LeanTween.moveY(gameObject, destination, time).setLoopPingPong();
    }
}
