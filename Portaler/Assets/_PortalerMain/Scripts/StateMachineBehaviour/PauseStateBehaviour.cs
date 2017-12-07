using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseStateBehaviour : StateMachineBehaviour
{
    PauseState _PauseState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _PauseState = FindObjectOfType<PauseState>();
        _PauseState.OnStateEnter(animator, animatorStateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _PauseState.OnStateUpdate(animator, animatorStateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _PauseState.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}