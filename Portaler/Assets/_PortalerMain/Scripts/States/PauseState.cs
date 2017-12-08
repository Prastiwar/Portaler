﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : MonoBehaviour
{
    [SerializeField] GameObject _PausePanel;

    bool isPause = false;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        SwitchPause();
    }

    public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        SwitchPause();
    }

    public void OnChangeSceneButton(string _SceneName)
    {
        StateMachineManager.ChangeSceneTo(_SceneName);
    }

    public void OnPauseButton()
    {
        string _StateTo = null;
        _StateTo = isPause ? "Game" : "Pause";

        StateMachineManager.ChangeStateTo(_StateTo);
    }


    void SwitchPause()
    {
        Time.timeScale = isPause ? 1 : 0;
        isPause = !isPause;
        if(_PausePanel != null)
            _PausePanel.SetActive(!_PausePanel.activeSelf);
    }
}