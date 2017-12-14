using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : MonoBehaviour
{
    [SerializeField] GameObject _PausePanel;
    StateMachineManager _stateManager;
    bool isPause = false;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _stateManager = StateMachineManager.Instance;
        
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

    public void OnPauseButton()
    {
        string _StateTo;
        _StateTo = isPause ? "Game" : "Pause";
        SwitchPause();
        StateMachineManager.Instance.ChangeStateTo(_StateTo);
    }


    void SwitchPause()
    {
        Time.timeScale = isPause ? 1 : 0;
        isPause = !isPause;
        if(_PausePanel != null)
            _PausePanel.SetActive(!_PausePanel.activeSelf);
    }
}