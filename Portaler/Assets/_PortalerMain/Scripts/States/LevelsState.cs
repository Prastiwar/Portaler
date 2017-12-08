using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsState : MonoBehaviour
{
    [SerializeField] Transform _LevelContent;
    [SerializeField] ScriptableLevel[] levels;
    [SerializeField] GameObject _LevelItemPrefab;
    List<LevelItem> _LevelItemList = new List<LevelItem>();

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        SpawnShopItems();
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

    void SpawnShopItems()
    {
        int _Length = levels.Length;
        for (int i = 0; i < _Length; i++)
        {
            GameObject _LevelItem = Instantiate(_LevelItemPrefab, _LevelContent);
            LevelItem _Item = _LevelItem.GetComponent<LevelItem>();
            _LevelItemList.Add(_Item);
            UpdateAllText(i);
        }
    }

    void UpdateAllText(int i)
    {
        _LevelItemList[i].lockedIcon.gameObject.SetActive(!levels[i].isUnlocked);
        _LevelItemList[i].icon.sprite = levels[i].sprite;
        _LevelItemList[i].starScore.fillAmount = levels[i].starScoreAmount;
        _LevelItemList[i].itemButton.interactable = levels[i].isUnlocked;
        _LevelItemList[i].itemButton.onClick.RemoveAllListeners();
        _LevelItemList[i].itemButton.onClick.AddListener(() => SetLevel(i));
    }

    void SetLevel(int lvlIndex)
    {
        GameState.lvlIndex = lvlIndex;
        StateMachineManager.ChangeSceneTo("Game");
    }

}