using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeLight : MonoBehaviour
{
    public GameObject light;
    float transp = 0;
    float opaque = 1;
    
    
    public void fadeIn(float time)
    {
        light.SetActive(true);
        BlackSplash(time, transp, opaque);
    }

    public void fadeOut(float time)
    {
        BlackSplash(time, opaque, transp);
        light.SetActive(false);
    }

    void BlackSplash(float time, float cFrom, float cTo)
    {
            LeanTween.value(gameObject, cFrom, cTo, time).setOnUpdate((float val) =>
            {
                light.GetComponent<Light>().intensity = val;
            });
    }
}
