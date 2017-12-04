using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMachineManager : MonoBehaviour
{
    public static StateMachineManager Instance;
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public static void ChangeState(Scene scene)
    {
        Animator animator = GameObject.FindGameObjectWithTag("PauseButton").GetComponent<Animator>();
        animator.SetTrigger(scene.name);
    }


}
