using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanturnCreak : MonoBehaviour
{
    public float timer;
    public Vector2 delayRange;
    public List<AudioClip> sounds;
    private int mod;
    
    // Update is called once per frame
    void Update()
    {
        if (timer < Time.time)
        {
            timer = Time.time + Random.Range(delayRange[0], delayRange[1]);
            GetComponent<AudioSource>().PlayOneShot(sounds[mod]);
            mod += 1;
            mod %= sounds.Count;
        }
    }
}
