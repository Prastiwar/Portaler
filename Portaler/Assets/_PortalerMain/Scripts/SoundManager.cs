using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    AudioSource audioSource;
    AudioSource themeSource;
    AudioClip clip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        audioSource = GetComponent<AudioSource>();
        themeSource = GetComponentInChildren<AudioSource>();
    }

    public void PlaySound(AudioClip _clip, float _volume)
    {
        audioSource.PlayOneShot(_clip, _volume);
    }

    public void ChangeTheme(AudioClip _clip, float _volume)
    {
        themeSource.Stop();
        themeSource.clip = _clip;
        themeSource.volume = _volume;
        themeSource.Play();
    }
}
