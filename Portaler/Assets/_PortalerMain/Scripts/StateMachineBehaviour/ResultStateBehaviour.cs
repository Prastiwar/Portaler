using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultStateBehaviour : StateMachineBehaviour
{
    ResultState _ResultState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _ResultState = FindObjectOfType<ResultState>();
        _ResultState.OnStateEnter(animator, animatorStateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _ResultState.OnStateUpdate(animator, animatorStateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _ResultState.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}