using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBehaviour : StateMachineBehaviour
{
    [SerializeField] ScriptableEnemy[] enemy;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        // Calculate range of view .. if(enemy.)
    }

    public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }
    
}
