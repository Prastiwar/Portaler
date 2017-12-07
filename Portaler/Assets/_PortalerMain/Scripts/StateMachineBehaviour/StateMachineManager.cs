using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    public static StateMachineManager Instance;
    public static Animator animator;
    const int _MachineLayer = 9;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);

        animator = GameObjectFindWithLayer.Find(_MachineLayer).GetComponent<Animator>();
    }

    public static void ChangeStateTo(string sceneName)
    {
        animator.SetTrigger(sceneName);
    }

    public static void ChangeSceneTo(string _SceneName)
    {
        Initiate.Fade(_SceneName, Color.black, 1.3f);
    }

}
