using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image panel;
    public float fadeTimer = 2f;
    public float startDelay;
    Color transp = new Color(0, 0, 0, 0);
    Color opaque = new Color(0, 0, 0, 1);

    void Start()
    {
        Invoke("fadeDelay", startDelay);
    }

    public void fadeDelay()
    {
        fadeOut(fadeTimer);
    }

    public void calledWithFadeDelay(float time)
    {
        Invoke("fadeDelay", time);
    }

    public void fadeIn(float time)
    {
        panel.enabled = true;
        BlackSplash(time, transp, opaque);
    }

    public void fadeOut(float time)
    {
        BlackSplash(time, opaque, transp);
        panel.enabled = true;
        Invoke("panelDisable", time);
    }

    void BlackSplash(float time, Color cFrom, Color cTo)
    {
        LeanTween.value(gameObject, cFrom, cTo, time).setOnUpdate((Color val) =>
        {
            panel.color = val;
        });
    }

    void panelDisable()
    {
        panel.enabled = false;
    }
}
