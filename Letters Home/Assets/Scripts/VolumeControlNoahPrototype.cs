
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControlNoahPrototype : MonoBehaviour
{
    public AudioSource player;
    public AudioClip DeathSound;
    private AudioClip storedClip; 
    public float[] HieghtPoints = new float[2];
    public float volumeLow;
    public float volumeHigh;
    Player checker;
    bool hasfired;
    // Start is called before the first frame update
    void Start()
    {
        //HieghtPoints[0] = transform.position.y;
        checker = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!hasfired)
        {
            player.volume = volumeHigh * (1-((HieghtPoints[1] - transform.position.y)/(HieghtPoints[1] - HieghtPoints[0])));
            //GetVolumeEQ//print(volumeHigh * (HieghtPoints[1] - transform.position.y) / (HieghtPoints[1] - HieghtPoints[0]));
            if (player.volume < volumeLow)
            {
                player.volume = volumeLow;
            }

            if(player.volume > volumeHigh)
            {
                player.volume = volumeHigh;
            }

            if (checker.GetDead())
            {
                player.Stop();
                player.volume = 0.6f;
                storedClip = player.clip;
                if (DeathSound)
                {
                    player.clip = DeathSound;
                }
                player.Play();
                hasfired = true;
            }
        }
        else
        {
            player.loop = false;
        }
    }

    public void Reset()
    {
        hasfired = false;
        player.clip = storedClip;
    }
}
