﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    //string _GameScene = "Game";
    //string _ShopScene = "Shop";
    //string _LevelsScene = "Levels";
    //float _dampTime = 1.3f;
    //static Animator animator;

    //bool isPaused = false;
    //[SerializeField] GameObject _PauseMenu;

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

        //animator = GetComponent<Animator>();
    }

    //public static void ChangeState(string state)
    //{
    //    animator.SetTrigger(state);
    //}


    /*public void OnBack(string previousScene)
    {
        Initiate.Fade(previousScene, Color.black, _dampTime);
    }

    public void OnPlay()
    {
        Initiate.Fade(_GameScene, Color.black, _dampTime);
    }

    public void OnShop()
    {
        Initiate.Fade(_ShopScene, Color.black, _dampTime);
    }

    public void OnLevels()
    {
        Initiate.Fade(_LevelsScene, Color.black, _dampTime);
    }

    public void OnPause()
    {
        Time.timeScale = isPaused ? 1 : 0;
        isPaused = !isPaused;
        _PauseMenu.SetActive(!_PauseMenu.activeSelf);
    }*/

}