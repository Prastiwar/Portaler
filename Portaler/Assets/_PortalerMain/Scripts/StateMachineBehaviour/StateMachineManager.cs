using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    public static StateMachineManager Instance;
    const int _MachineLayer = 9;

    public static Animator animator;
    //public static ScriptableWeapon[] GetWeapon;
    //public static ScriptableLevel[] GetLevel;
    //[SerializeField] ScriptableWeapon[] _weapons;
    //[SerializeField] ScriptableLevel[] _levels;

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
        //GetLevel = _levels;
        //GetWeapon = _weapons;
        animator = GameObjectFindWithLayer.Find(_MachineLayer).GetComponent<Animator>();
    }

    public static void ChangeStateTo(string _SceneName) // must use only on fader!
    {
        animator.SetTrigger(_SceneName);
    }

    public static void ChangeSceneTo(string _SceneName)
    {
        Initiate.Fade(_SceneName, Color.black, 1.3f);
    }

}
