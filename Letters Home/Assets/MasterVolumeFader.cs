using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MasterVolumeFader : MonoBehaviour
{
    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;
    public AudioMixer NarrationMixer;

    public List<AudioSource> sfxs;

    public float fadeTime = 0f;
    private float fadeTimer = 0f;
    public bool fade = true;
    private bool t = false;

    // Start is called before the first frame update
    void Start()
    {
        fadeTimer = Time.time + fadeTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (fadeTimer > Time.time && !fade)
        {
            float f = Mathf.Lerp(-1, PlayerPrefs.GetFloat("MV"), (fadeTimer - Time.time) / fadeTime);
            //print(f);
            float f2 = Mathf.Lerp(-1, PlayerPrefs.GetFloat("SFXV"), (fadeTimer - Time.time) / fadeTime);
            setMusicVolume(f);
            setSFXVolume(f2);
            setNarrationVolume(Mathf.Lerp(-1, PlayerPrefs.GetFloat("NarV"), (fadeTimer - Time.time) / fadeTime));
        }
        if (fadeTimer > Time.time && fade)
        {
            float f = Mathf.Lerp(PlayerPrefs.GetFloat("MV"), -1, (fadeTimer - Time.time) / fadeTime);
            //print(f);
            float f2 = Mathf.Lerp(PlayerPrefs.GetFloat("SFXV"), -1, (fadeTimer - Time.time) / fadeTime);
            setMusicVolume(f);
            setSFXVolume(f2);
            setNarrationVolume(Mathf.Lerp(PlayerPrefs.GetFloat("NarV"), -1, (fadeTimer - Time.time) / fadeTime));
        }
        if(fadeTimer < Time.time && !t)
        {
            t = !t;
            if(sfxs.Count > 0)
            {
                foreach(AudioSource s in sfxs)
                {
                    s.Play();
                }
            }
        }
    }

    public void setMusicVolume(float v)
    {

        MusicMixer.SetFloat("MusicVolume", Mathf.Log10(-v) * 20);

    }

    public void setSFXVolume(float v)
    {
        SFXMixer.SetFloat("SFXVolume", Mathf.Log10(-v) * 20);
    }

    public void setNarrationVolume(float v)
    {
        NarrationMixer.SetFloat("NarrationVolume", Mathf.Log10(-v) * 20);
    }

    private void invokeFadeIn()
    {
        fadeTimer = Time.time + fadeTime;
        fade = true;
    }

    private void invokeFadeOut()
    {
        fadeTimer = Time.time + fadeTime;
        fade = false;
    }
}
