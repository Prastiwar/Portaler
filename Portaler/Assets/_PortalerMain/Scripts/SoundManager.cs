using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct SoundOptions
{
    public bool isMusicMuted;
    public bool isSoundMuted;
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public SoundOptions Sound;

    [SerializeField] Sprite[] OnOff;
    public AudioClip[] audioClips;

    AudioSource audioSource;
    AudioSource themeSource;

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

        themeSource = transform.GetChild(0).GetComponentInChildren<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        isMusicMuted = Sound.isMusicMuted;
        isSoundMuted = Sound.isSoundMuted;
    }

    public void PlaySound(AudioClip _clip, float _volume)
    {
        if (isSoundMuted)
            return;

        audioSource.PlayOneShot(_clip, _volume);
    }

    public void PlaySound(AudioClip _clip, float _volume, bool _loop)
    {
        if (isSoundMuted)
            return;

        audioSource.Stop();
        audioSource.clip = _clip;
        audioSource.volume = _volume;
        audioSource.loop = _loop;
        audioSource.Play();
    }

    public void PlaySingleSound(AudioClip _clip, float _volume)
    {
        if (isSoundMuted)
            return;

        audioSource.Stop();
        audioSource.clip = _clip;
        audioSource.volume = _volume;
        audioSource.Play();
    }

    public void MusicStereo(float stereoValue)
    {
        themeSource.panStereo = stereoValue;
    }

    public void PlayMusic(AudioClip _clip, float _volume)
    {
        if (isMusicMuted)
            return;

        MusicStereo(0);
        themeSource.Stop();
        themeSource.clip = _clip;
        themeSource.volume = _volume;
        themeSource.playOnAwake = true;
        themeSource.loop = true;
        themeSource.Play();
    }

    public void ToggleMusic(Image _image)
    {
        isMusicMuted = !isMusicMuted;
        SetQuaver(_image);
        themeSource.mute = isMusicMuted ? true : false;
    }
    public void ToggleSound(Image _image)
    {
        isSoundMuted = !isSoundMuted;
        SetSpeaker(_image);
        audioSource.mute = isSoundMuted ? true : false;
    }

    public void SetSpeaker(Image _image)
    {
        _image.sprite = isSoundMuted ? OnOff[1] : OnOff[0];
    }
    public void SetQuaver(Image _image)
    {
        _image.sprite = isMusicMuted ? OnOff[3] : OnOff[2];
    }
}
