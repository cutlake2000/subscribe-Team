using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource BgmAudioSource;

    public AudioClip BgmAudioClip;


    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }



    public void PlayBgmSound()
    {
        BgmAudioSource.clip = BgmAudioClip;
        BgmAudioSource.Play();
    }


}
