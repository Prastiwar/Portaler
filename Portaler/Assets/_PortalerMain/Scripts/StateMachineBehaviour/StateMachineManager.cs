using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    public static StateMachineManager Instance;
    const int _MachineLayer = 9;

    public static Animator animator;
    public static ScriptableData data;
    [SerializeField] ScriptableData _ScriptableData;

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
        data = _ScriptableData;
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
        // higher value = faster
        float dampIn = 0.2f;
        float dampOut = 1.5f;
        // higher value = bigger break = slower load
        float breakLast = 1.35f;
        // higher value = faster dampIn after break
        float breakAcceleration = 0.8f;

        Initiate.Fade(_SceneName, Color.black, dampIn, dampOut, true, breakLast, breakAcceleration);
    }

}
