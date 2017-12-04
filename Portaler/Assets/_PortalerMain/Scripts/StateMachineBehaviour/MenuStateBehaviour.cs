using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateBehaviour : StateMachineBehaviour
{
    MenuState _MenuState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _MenuState = FindObjectOfType<MenuState>();
        _MenuState.OnStateEnter(animator, animatorStateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _MenuState.OnStateUpdate(animator, animatorStateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _MenuState.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}
