using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (SceneManager.GetSceneByName("Game").isLoaded)
        {
            Button onPauseButton = GameObject.FindGameObjectWithTag("PauseButton").GetComponent<Button>();

            onPauseButton.onClick.AddListener(delegate ()
            {
                animator.SetTrigger("OnPause");
            });
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        StateMachineManager.ChangeScene(animator);
    }
}