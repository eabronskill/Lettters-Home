using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteText : MonoBehaviour
{
    public UnityEngine.UI.Text script;
    public float delay;

    private void Start()
    {
        string origText = script.text;
        script.text = "";
        LeanTween.value(gameObject, 0, (float)origText.Length, delay).setEase(LeanTweenType.easeOutQuad).setOnUpdate((float val) =>
        {
            script.text = origText.Substring(0, Mathf.RoundToInt(val));
        }).setLoopOnce().setDelay(.5f);
    }
}
