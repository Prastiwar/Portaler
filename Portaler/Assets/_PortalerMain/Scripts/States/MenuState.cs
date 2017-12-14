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
    [SerializeField] AudioClip clipSound;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _stateManager = StateMachineManager.Instance;
        SoundManager.Instance.PlayMusic(clipMusic, 1);
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
        SoundManager.Instance.PlayMusic(clipSound, 1);
        SoundManager.Instance.ToggleMusic(musicButtonImage);
    }
    public void OnSoundButton()
    {
        SoundManager.Instance.PlayMusic(clipSound, 1);
        SoundManager.Instance.ToggleSound(soundButtonImage);
    }

}
