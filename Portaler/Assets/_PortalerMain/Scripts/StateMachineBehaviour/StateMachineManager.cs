using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    public static StateMachineManager Instance;
    const int _MachineLayer = 9;

    public static Animator animator;

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

    // It's changing state
    public static void ChangeStateTo(string _StateName)
    {
        animator.SetTrigger(_StateName);
    }

    // It's changing scene AND scene STATE
    public static void ChangeSceneTo(string _SceneName)
    {
        Initiate.Fade(_SceneName, Color.black, 1.3f);
    }

}
