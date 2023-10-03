using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgSound;
    public AudioClip[] bglist;

    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go, clip.length);
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop= true;
        bgSound.volume = 0.1f;
        bgSound.Play();
    }

    public void PlayDayMusic()
    {
        BgSoundPlay(bglist[0]); // dayMusic는 낮 브금 AudioClip입니다.
    }

    public void PlayNightMusic()
    {
        BgSoundPlay(bglist[1]); // nightMusic는 밤 브금 AudioClip입니다.
    }
}
