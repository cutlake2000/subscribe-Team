using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource BgmAudioSource;

    public AudioClip[] BgmAudioClips;

    enum BgmScene
    {
        StartScene,
        MainScene
    }

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 이름을 기반으로 해당 씬에 맞는 배경 음악을 선택하여 재생
        string sceneName = scene.name;
        AudioClip selectedClip = null;

        // 씬 이름에 따라 다른 배경 음악을 선택
        if (sceneName == "StartScene")
        {
            selectedClip = BgmAudioClips[(int)BgmScene.StartScene];
        }
        else if (sceneName == "MainScene")
        {
            selectedClip = BgmAudioClips[(int)BgmScene.MainScene];
        }
        // 다른 씬에 대한 처리도 추가

        // 선택된 배경 음악을 재생
        if (selectedClip != null)
        {
            BgmAudioSource.clip = selectedClip;
            BgmAudioSource.Play();
        }
    }
}
