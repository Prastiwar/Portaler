using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuState : MonoBehaviour
{
    StateMachineManager _stateManager;
    [SerializeField] Image musicButtonImage;
    [SerializeField] Image soundButtonImage;
    [SerializeField] AudioClip clipMusic;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _stateManager = StateMachineManager.Instance;
        SoundManager.Instance.PlayMusic(clipMusic, 0.051f);
        SoundManager.Instance.SetSpeaker(soundButtonImage);
        SoundManager.Instance.SetQuaver(musicButtonImage);
    }

    public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    public void OnChangeSceneButton(string _SceneName)
    {
        _stateManager.ChangeSceneTo(_SceneName);
    }

    public void OnMusicButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.audioClips[0], 1);
        SoundManager.Instance.ToggleMusic(musicButtonImage);
    }
    public void OnSoundButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.audioClips[0], 1);
        SoundManager.Instance.ToggleSound(soundButtonImage);
    }

}