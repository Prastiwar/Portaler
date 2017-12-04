using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateBehaviour : StateMachineBehaviour
{
    GameState _GameState;
    //[SerializeField] GameObject PauseCanvas;
    //Button onPauseButton;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _GameState = FindObjectOfType<GameState>();
        _GameState.OnStateEnter(animator, animatorStateInfo, layerIndex);

        //SpawnLevel(animator);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _GameState.OnStateUpdate(animator, animatorStateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _GameState.OnStateExit(animator, animatorStateInfo, layerIndex);
    }

    /*void SpawnLevel(Animator animator)
    {
        PauseCanvas = Instantiate(PauseCanvas);
        onPauseButton = PauseCanvas.GetComponentInChildren<Button>();

        onPauseButton.onClick.AddListener(delegate () {
            animator.SetTrigger("Pause");
        });
    }*/

}