using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsState : MonoBehaviour
{
    [SerializeField] Transform _LevelContent;
    [SerializeField] ScriptableData data;
    [SerializeField] ScriptableItem _LevelItem;
    StateMachineManager _stateManager;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _stateManager = StateMachineManager.Instance;
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
        _stateManager.ChangeSceneTo(_SceneName);
    }

    void SpawnLeveltems()
    {
        int _Length = data.Levels.Length;
        for (int i = 0; i < _Length; i++)
        {
            int x = i;
            var level = data.Levels[i];
            _LevelItem.SpawnItem(_LevelContent);

            _LevelItem.GetIcon(0).fillAmount = level.starScoreAmount;
            _LevelItem.GetButton(0).GetComponent<Image>().sprite = level.isUnlocked ? level.sprite : level.lockedSprite;
            _LevelItem.GetButton(0).interactable = level.isUnlocked;
            _LevelItem.AddListenerOnButton(0, () => SetLevel(x), true);
        }
    }

    void SetLevel(int lvlIndex)
    {
        GameState.player.lvlIndex = lvlIndex;
        _stateManager.ChangeSceneTo("Game");
    }

}