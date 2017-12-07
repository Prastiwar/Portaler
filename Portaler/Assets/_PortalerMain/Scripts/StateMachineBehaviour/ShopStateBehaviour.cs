using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStateBehaviour : StateMachineBehaviour
{
    ShopState _ShopState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _ShopState = FindObjectOfType<ShopState>();
        _ShopState.OnStateEnter(animator, animatorStateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _ShopState.OnStateUpdate(animator, animatorStateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _ShopState.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}