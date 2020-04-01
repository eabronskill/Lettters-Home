using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public GameObject panel;
    Color transp = new Color(0, 0, 0, 0);
    Color opaque = new Color(0, 0, 0, 1);

    public void fadeIn(float time)
    {
        panel.SetActive(true);
        BlackSplash(time, transp, opaque);
    }

    public void fadeOut(float time)
    {
        BlackSplash(time, opaque, transp);
        panel.SetActive(false);
    }

    void BlackSplash(float time, Color cFrom, Color cTo)
    {
            LeanTween.value(gameObject, cFrom, cTo, time).setOnUpdate((Color val) =>
            {
                panel.GetComponent<Image>().color = val;
            });
    }
}
