using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateBehaviour : StateMachineBehaviour
{
    GameState _GameState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _GameState = FindObjectOfType<GameState>();
        _GameState.OnStateEnter(animator, animatorStateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _GameState.OnStateUpdate(animator, animatorStateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _GameState.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}