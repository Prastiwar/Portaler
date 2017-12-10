using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsState : MonoBehaviour
{
    [SerializeField] Transform _LevelContent;
    [SerializeField] ScriptableData data;
    [SerializeField] ScriptableItem _LevelItem;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        SpawnLeveltems();
    }

    public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }

    public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    public void OnChangeSceneButton(string _SceneName)
    {
        StateMachineManager.ChangeSceneTo(_SceneName);
    }

    void SpawnLeveltems()
    {
        int _Length = data.Levels.Length;
        for (int i = 0; i < _Length; i++)
        {
            var level = data.Levels[i];
            _LevelItem.SpawnItem(_LevelContent);

            _LevelItem.SetIcon(0, level.sprite);
            _LevelItem.GetIcon(1).gameObject.SetActive(!level.isUnlocked);
            _LevelItem.GetIcon(2).fillAmount = level.starScoreAmount;
            _LevelItem.GetButton(0).interactable = level.isUnlocked;
            _LevelItem.AddListenerOnButton(0, () => SetLevel(i), true);
        }
    }

    void SetLevel(int lvlIndex)
    {
        GameState.lvlIndex = lvlIndex;
        StateMachineManager.ChangeSceneTo("Game");
    }

}