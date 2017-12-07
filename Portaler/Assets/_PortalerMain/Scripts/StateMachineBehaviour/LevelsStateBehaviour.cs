using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsStateBehaviour : StateMachineBehaviour
{
    LevelsState _LevelsState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _LevelsState = FindObjectOfType<LevelsState>();
        _LevelsState.OnStateEnter(animator, animatorStateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _LevelsState.OnStateUpdate(animator, animatorStateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _LevelsState.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}