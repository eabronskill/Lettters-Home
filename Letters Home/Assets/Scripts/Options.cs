using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public static float prevMusVol;
    public static float prevSFXVol;
    public static float prevNarVol;

    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;
    public AudioMixer NarrationMixer;

    private Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public Slider slider1, slider2, slider3;

    // Start is called before the first frame update
    void Start()
    {
        prevMusVol = PlayerPrefs.GetFloat("MV");
        prevSFXVol = PlayerPrefs.GetFloat("SFXV");
        prevNarVol = PlayerPrefs.GetFloat("NarV");
        slider1.value = prevMusVol;
        slider2.value = prevSFXVol;
        slider3.value = prevNarVol;
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + "x" + resolutions[i].height);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setMusicVolume(float v)
    {
        prevMusVol = v;
        PlayerPrefs.SetFloat("MV", v);
        MusicMixer.SetFloat("MusicVolume", Mathf.Log10(v) * 20);
    }

    public void setSFXVolume(float v)
    {
        prevSFXVol = v;
        PlayerPrefs.SetFloat("SFXV", v);
        SFXMixer.SetFloat("SFXVolume", Mathf.Log10(v) * 20);
    }

    public void setNarrationVolume(float v)
    {
        prevNarVol = v;
        PlayerPrefs.SetFloat("NarV", v);
        NarrationMixer.SetFloat("NarrationVolume", Mathf.Log10(v) * 20);
    }

    public float getMusicVolume()
    {
        float v;
        MusicMixer.GetFloat("MusicVolume", out v);
        return v;

    }

    public float getSFXVolume()
    {
        float v;
        SFXMixer.GetFloat("SFXVolume", out v);
        return v;

    }

    public float getNarrationVolume()
    {
        float v;
        NarrationMixer.GetFloat("NarrationVolume", out v);
        return v;

    }
    public void setQuality(int q)
    {
        QualitySettings.SetQualityLevel(q);
        Debug.Log(QualitySettings.GetQualityLevel().ToString());
        //TODO: Fix this
    }

    public void setScreenSize(int r)
    {
        Resolution res = resolutions[r];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void toggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    public void toggleMuteMusic()
    {
        float vol = getMusicVolume();
        if (vol != -80 && ! float.IsNaN(vol))
        {
            prevMusVol = vol;
            setMusicVolume(-80);
        }
        else
        {
            setMusicVolume(prevMusVol);
        }
    }
    public void toggleMuteSFX()
    {
        float vol = getSFXVolume();
        if (vol != -80 && !float.IsNaN(vol))
        {
            prevSFXVol = vol;
            setSFXVolume(-80);
        }
        else
        {
            setSFXVolume(prevSFXVol);
        }
    }
    public void toggleMuteNar()
    {
        float vol = getNarrationVolume();
        if (vol != -80 && !float.IsNaN(vol))
        {
            prevNarVol = vol;
            setNarrationVolume(-80);
        }
        else
        {
            setNarrationVolume(prevNarVol);
        }
    }
}
