using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] Sprite SoundOn;
    [SerializeField] Sprite SoundOff;
    [SerializeField] Sprite MusicOn;
    [SerializeField] Sprite MusicOff;

    AudioSource audioSource;
    AudioSource themeSource;
    AudioClip clip;

    bool isMusicMuted = false;
    bool isSoundMuted = false;

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
        if (isSoundMuted)
            return;

        audioSource.PlayOneShot(_clip, _volume);
    }

    public void PlayMusic(AudioClip _clip, float _volume)
    {
        if (isMusicMuted)
            return;

        themeSource.Stop();
        themeSource.clip = _clip;
        themeSource.volume = _volume;
        themeSource.Play();
    }

    public void ToggleMusic(Image image)
    {
        isMusicMuted = !isMusicMuted;
        image.sprite = isMusicMuted ? MusicOff : MusicOn;
        themeSource.volume = isMusicMuted ? 0 : 1;
    }
    public void ToggleSound(Image image)
    {
        isSoundMuted = !isSoundMuted;
        image.sprite = isSoundMuted ? SoundOff : SoundOn;
        audioSource.volume = isSoundMuted ? 0 : 1;
    }
}
