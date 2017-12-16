using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsState : MonoBehaviour
{
    [SerializeField] Transform _LevelContent;
    [SerializeField] AudioClip audioClip;
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
        int _Length = _stateManager.data.Levels.Length;
        for (int i = 0; i < _Length; i++)
        {
            int x = i;
            var level = _stateManager.data.Levels[i];
            level.InitializeItem();
            level.SpawnItem(_LevelContent);
            level.GetIcon(1).fillAmount = level.starScoreAmount;
            level.SetText(0, (x + 1).ToString());
            level.GetButton(0).GetComponent<Image>().sprite = level.isUnlocked ? level.sprite : level.lockedSprite;
            level.GetButton(0).interactable = level.isUnlocked;
            level.AddListenerOnButton(0, () => SetLevel(x), true);
        }
    }

    void SetLevel(int lvlIndex)
    {
        SoundManager.Instance.PlaySingleSound(audioClip, 1);
        GameState.player.lvlIndex = lvlIndex;
        _stateManager.ChangeSceneTo("Game");
    }

}