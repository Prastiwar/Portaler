using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager
{
    public static void ChangeScene(Animator animator)
    {
        string _SceneName = null;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Game State"))
            _SceneName = "Game";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Level State"))
            _SceneName = "Levels";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Shop State"))
            _SceneName = "Shop";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Menu State"))
            _SceneName = "Menu";
        else
            return;

        Initiate.Fade(_SceneName, Color.black, 1.3f);
    }
}
