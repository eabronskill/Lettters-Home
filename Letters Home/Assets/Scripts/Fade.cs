using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public GameObject panel;
    Color transp = new Color(0, 0, 0, 0);
    Color opaque = new Color(0, 0, 0, 1);

    public void fadeTo(float time)
    {
        panel.SetActive(true);
        BlackSplash(time, true);
    }

    public void fadeFrom(float time)
    {
        BlackSplash(time, false);
        panel.SetActive(false);
    }

    void BlackSplash(float time, bool fadeIn)
    {
        if (fadeIn)
        {
            LeanTween.value(gameObject, transp, opaque, time).setOnUpdate((Color val) =>
            {
                panel.GetComponent<Image>().color = val;
            });
        }
        else
        {
            LeanTween.value(gameObject, opaque, transp, time).setOnUpdate((Color val) =>
            {
                panel.GetComponent<Image>().color = val;
            });
        }
    }
}
