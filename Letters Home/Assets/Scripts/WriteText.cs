using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WriteText : MonoBehaviour
{
    public UnityEngine.UI.Text script;
    public float delay;
    public AudioMixer mixer;
    public List<AudioSource> sources = new List<AudioSource>();
    private bool firstTrigger = true;

    void Start()
    {
        OnEnable();
        firstTrigger = true;
    }

    private void OnEnable()
    {
        if (!firstTrigger)
        {
            if (sources.Count > 0 && sources.Count > 0)
            {
                foreach (AudioSource source in sources)
                {
                    source.enabled = false;
                }
            }

            string origText = script.text;
            script.text = "";
            print("begin Tweening");
            LeanTween.value(gameObject, 0, (float)origText.Length, delay)./*setEase(LeanTweenType.easeOut)*/setOnUpdate((float val) =>
            {
                print("tween");
                script.text = origText.Substring(0, Mathf.RoundToInt(val));
            }).setDelay(.5f);
        }
        else
        {
            firstTrigger = false;
        }
    }

    private void OnDisable()
    {
        if (sources.Count > 0 && sources.Count > 0)
        {
            foreach (AudioSource source in sources)
            {
                source.enabled = true;
            }
        }
    }
}
