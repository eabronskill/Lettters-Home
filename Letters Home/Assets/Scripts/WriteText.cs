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

    void Start()
    {
        OnEnable();
    }

    private void OnEnable()
    {
            Invoke("enableScript", 0.65f);
            string origText = script.text;
            
            print("begin Tweening");
            LeanTween.value(gameObject, 0, (float)origText.Length, delay)./*setEase(LeanTweenType.easeOut)*/setOnUpdate((float val) =>
            {
                print("tween");
                script.text = origText.Substring(0, Mathf.RoundToInt(val));
            }).setDelay(.5f);

            if (sources != null && sources.Count > 0 /*&& sources.Count > 0*/)
                {
                    foreach (AudioSource source in sources)
                    {
                        if(source != null)
                            source.enabled = false;
                    }
                }
    }

    void enableScript()
    {
        script.enabled = true;
    }

    private void OnDisable()
    {
        delay = 0;
        script.enabled = false;
        if (sources != null && sources.Count > 0 /*&& sources.Count > 0*/)
        {
            foreach (AudioSource source in sources)
            {
                if(source)
                    source.enabled = true;
            }
        }
    }
}
