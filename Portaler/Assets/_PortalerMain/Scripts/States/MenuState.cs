using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : MonoBehaviour
{

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }

    public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    public void ChangeScene(string _SceneName)
    {
        Initiate.Fade(_SceneName, Color.black, 1.3f);
    }

}
